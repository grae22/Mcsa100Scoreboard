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

    private readonly IWebRequestService _webRequestService;
    private readonly string _googleApiKey;
    private readonly string _googleSheetId;
    private readonly string _delayedBackupUrl;

    public IndexModel(IWebRequestService webRequestService)
    {
      _webRequestService = webRequestService ?? throw new ArgumentNullException(nameof(webRequestService));

      _googleApiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleApiKeyVarName) ?? throw new Exception("Api Key env-var not found.");
      _googleSheetId = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleSheetIdKeyVarName) ?? throw new Exception("Sheet Id env-var not found.");
      _delayedBackupUrl = Environment.GetEnvironmentVariable(EnvironmentVariables.DelayedBackupUrlVarName) ?? throw new Exception("Delayed Backup URL env-var not found.");
    }

    public async Task OnGet()
    {
      var address = new Uri($"{GoogleSheetsBaseUrl}{_googleSheetId}/values/Sheet1!A1:Z500?key={_googleApiKey}");
      var delayedBackupAddress = new Uri(_delayedBackupUrl);

      InputModel input = null;
      InputModel delayedBackupInput = null;

      try
      {
        input = await _webRequestService.RetrieveInput<InputModel>(address);
        delayedBackupInput = await _webRequestService.RetrieveInput<InputModel>(delayedBackupAddress);
      }
      catch (Exception)
      {
        // Yep.
      }

      if (input == null)
      {
        Scoreboard = new Scoreboard(null);
        Narrator = new ScoreboardNarrator(null, null);
        return;
      }

      var parsedInput = new InputParser(input);

      Scoreboard = new Scoreboard(parsedInput.Climbers);

      if (delayedBackupInput == null)
      {
        Narrator = new ScoreboardNarrator(null, null);
        await UpdateDelayedBackup(input, null);
        return;
      }

      var delayedBackupParsedInput = new InputParser(delayedBackupInput);
      var oldScoreboard = new Scoreboard(delayedBackupParsedInput.Climbers);
      Narrator = new ScoreboardNarrator(oldScoreboard, Scoreboard);

      // TODO
      //await UpdateDelayedBackup(input, delayedBackupInput);
    }

    private async Task UpdateDelayedBackup(
      InputModel input,
      InputModel delayedBackupInput)
    {
      var delayedBackupAddress = new Uri(_delayedBackupUrl);

      if (!DateTime.TryParse(delayedBackupInput?.values[0][0], out DateTime currentBackupTimestamp))
      {
        currentBackupTimestamp = DateTime.MinValue;
      }

      TimeSpan delta = DateTime.Now - currentBackupTimestamp;

      if (delta.TotalDays <= 7)
      {
        return;
      }

      input.values[0][0] = $"{DateTime.Now:yyyy/MM/dd}";

      var result = await _webRequestService.WriteJson(
        delayedBackupAddress,
        JsonConvert.SerializeObject(input));
    }
  }
}
