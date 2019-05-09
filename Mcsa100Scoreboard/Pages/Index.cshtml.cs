using System;
using System.Threading.Tasks;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Domain.Climbs;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;
using Mcsa100Scoreboard.Services.JsonBackup;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Pages
{
  public class IndexModel : PageModel
  {
    public Scoreboard Scoreboard { get; private set; } = new Scoreboard(null, null);
    public ScoreboardNarrator Narrator { get; private set; } = new ScoreboardNarrator(null, null);

    private const string GoogleSheetsBaseUrl = "https://sheets.googleapis.com/v4/spreadsheets/";

    private readonly IWebRestService _liveDataSource;
    private readonly JsonBackupService _backupService;

    public IndexModel()
    {
      string googleApiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleApiKeyVarName) ?? throw new Exception("Api Key env-var not found.");
      string googleSheetId = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleSheetIdKeyVarName) ?? throw new Exception("Sheet Id env-var not found.");
      string backupUrl = Environment.GetEnvironmentVariable(EnvironmentVariables.DataBackupUrlVarName) ?? throw new Exception("Data Backup URL env-var not found.");

      _liveDataSource = new WebRestService(new Uri($"{GoogleSheetsBaseUrl}{googleSheetId}/values/Sheet1!A1:Z500?key={googleApiKey}"));

      var backupWebService = new WebRestService(new Uri(backupUrl));

      _backupService = new JsonBackupService(
        new TimeService(),
        backupWebService,
        7);
    }

    public async Task OnGet()
    {
      InputModel input = null;
      InputModel backupInput = null;

      try
      {
        input = await _liveDataSource
          .Get<InputModel>()
          .ConfigureAwait(false);
#if !DEBUG
        await _backupService
          .Add(JsonConvert.SerializeObject(input))
          .ConfigureAwait(false);
#endif
        string backupDataSerialised = await _backupService.GetOldest();

        backupInput = JsonConvert.DeserializeObject<InputModel>(backupDataSerialised);
      }
      catch (Exception)
      {
        // Yep.
      }

      if (input == null)
      {
        return;
      }

      Scoreboard oldScoreboard = null;

      if (backupInput != null)
      {
        var backupParsedInput = new InputParser(backupInput);

        oldScoreboard = new Scoreboard(backupParsedInput.Climbers, null);
      }

      var parsedInput = new InputParser(input);

      Scoreboard = new Scoreboard(parsedInput.Climbers, oldScoreboard?.AnalysedClimbersInRankOrder);
      Narrator = new ScoreboardNarrator(oldScoreboard, Scoreboard);
    }
  }
}
