using System;
using System.Threading.Tasks;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Pages
{
  public class IndexModel : PageModel
  {
    public Scoreboard Scoreboard { get; private set; }
    public ScoreboardNarrator Narrator { get; private set; }

    private const string GoogleSheetsBaseUrl = "https://sheets.googleapis.com/v4/spreadsheets/";

    private readonly IGoogleSheetService _googleSheetService;
    private readonly string _googleSheetId;
    private readonly string _delayedBackupGoogleSheetId;
    private readonly string _googleApiKey;

    public IndexModel(IGoogleSheetService googleSheetService)
    {
      _googleSheetService = googleSheetService ?? throw new ArgumentNullException(nameof(googleSheetService));

      _googleSheetId = Environment.GetEnvironmentVariable("GoogleSheetId") ?? throw new Exception("Sheet Id not found.");
      _delayedBackupGoogleSheetId = Environment.GetEnvironmentVariable("DelayedBackupGoogleSheetId") ?? throw new Exception("Delayed Backup Sheet Id not found.");
      _googleApiKey = Environment.GetEnvironmentVariable("GoogleApiKey") ?? throw new Exception("Api Key not found.");
    }

    public async Task OnGet()
    {
      var address = new Uri($"{GoogleSheetsBaseUrl}{_googleSheetId}/values/Sheet1!A1:Z500?key={_googleApiKey}");
      var delayedBackupAddress = new Uri($"{GoogleSheetsBaseUrl}{_delayedBackupGoogleSheetId}/values/Sheet1!A1:Z500?key={_googleApiKey}");

      InputModel input = null;
      InputModel delayedBackupInput = null;

      try
      {
        input = await _googleSheetService.RetrieveInput<InputModel>(address);
        delayedBackupInput = await _googleSheetService.RetrieveInput<InputModel>(delayedBackupAddress);
      }
      catch (Exception)
      {
        // Yep.
      }

      if (input == null ||
          delayedBackupInput == null)
      {
        Scoreboard = new Scoreboard(null);
        Narrator = new ScoreboardNarrator(null, null);
        return;
      }

      var parsedInput = new InputParser(input);
      var delayedBackupParsedInput = new InputParser(delayedBackupInput);

      Scoreboard = new Scoreboard(parsedInput.Climbers);

      var oldScoreboard = new Scoreboard(delayedBackupParsedInput.Climbers);
      Narrator = new ScoreboardNarrator(oldScoreboard, Scoreboard);

      // TODO
      //await UpdateDelayedBackup(input, delayedBackupInput);
    }

    private async Task UpdateDelayedBackup(
      InputModel input,
      InputModel delayedBackupInput)
    {
      var delayedBackupAddress = new Uri($"{GoogleSheetsBaseUrl}{_delayedBackupGoogleSheetId}/values/Sheet2!A1:Z500?valueInputOption=USER_ENTERED?key={_googleApiKey}");

      if (!DateTime.TryParse(delayedBackupInput.values[0][0], out DateTime currentBackupTimestamp))
      {
        currentBackupTimestamp = DateTime.MinValue;
      }

      TimeSpan delta = DateTime.Now - currentBackupTimestamp;

      if (delta.TotalDays <= 7)
      {
        return;
      }

      // TODO: Remove.
      input.range = "Sheet2!A1:Z500";

      input.values[0][0] = $"{DateTime.Now:yyyy/MM/dd}";

      var result = await _googleSheetService.Write(
        delayedBackupAddress,
        JsonConvert.SerializeObject(input));
    }
  }
}
