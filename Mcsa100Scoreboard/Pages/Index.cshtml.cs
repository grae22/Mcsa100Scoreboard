using System;
using System.Threading.Tasks;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcsa100Scoreboard.Pages
{
  public class IndexModel : PageModel
  {
    public Scoreboard Scoreboard { get; private set; }
    public ScoreboardNarrator Narrator { get; private set; }

    private const string GoogleSheetsBaseUrl = "https://sheets.googleapis.com/v4/spreadsheets/";

    private readonly IWebRestService _liveDataSource;
    private readonly IWebRestService _backupDataSource;

    public IndexModel()
    {
      string googleApiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleApiKeyVarName) ?? throw new Exception("Api Key env-var not found.");
      string googleSheetId = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleSheetIdKeyVarName) ?? throw new Exception("Sheet Id env-var not found.");
      string backupUrl = Environment.GetEnvironmentVariable(EnvironmentVariables.DataBackupUrlVarName) ?? throw new Exception("Data Backup URL env-var not found.");

      _liveDataSource = new WebRestService(new Uri($"{GoogleSheetsBaseUrl}{googleSheetId}/values/Sheet1!A1:Z500?key={googleApiKey}"));
      _backupDataSource = new WebRestService(new Uri(backupUrl));
    }

    public async Task OnGet()
    {
      InputModel input = null;
      InputModel delayedBackupInput = null;

      try
      {
        input = await _liveDataSource.Get<InputModel>();
        delayedBackupInput = await _backupDataSource.Get<InputModel>();
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
        //await UpdateDelayedBackup(input, null);
        return;
      }

      var delayedBackupParsedInput = new InputParser(delayedBackupInput);
      var oldScoreboard = new Scoreboard(delayedBackupParsedInput.Climbers);
      Narrator = new ScoreboardNarrator(oldScoreboard, Scoreboard);

      // TODO
      //await UpdateDelayedBackup(input, delayedBackupInput);
    }

    //private async Task UpdateDelayedBackup(
    //  InputModel input,
    //  InputModel delayedBackupInput)
    //{
    //  var delayedBackupAddress = new Uri(_delayedBackupUrl);

    //  if (!DateTime.TryParse(delayedBackupInput?.values[0][0], out DateTime currentBackupTimestamp))
    //  {
    //    currentBackupTimestamp = DateTime.MinValue;
    //  }

    //  TimeSpan delta = DateTime.Now - currentBackupTimestamp;

    //  if (delta.TotalDays <= 7)
    //  {
    //    return;
    //  }

    //  input.values[0][0] = $"{DateTime.Now:yyyy/MM/dd}";

    //  var result = await _webRequestService.WriteJson(
    //    delayedBackupAddress,
    //    JsonConvert.SerializeObject(input));
    //}
  }
}
