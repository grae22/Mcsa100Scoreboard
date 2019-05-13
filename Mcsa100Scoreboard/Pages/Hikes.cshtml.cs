using System;
using System.Threading.Tasks;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Domain.Hikes;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcsa100Scoreboard.Pages
{
  public class HikesModel : PageModel
  {
    public Scoreboard Scoreboard { get; private set; } = new Scoreboard(null);
    public string SheetId { get; }

    private const string GoogleSheetsBaseUrl = "https://sheets.googleapis.com/v4/spreadsheets/";

    private readonly IWebRestService _liveDataSource;

    public HikesModel()
    {
      string googleApiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.GoogleApiKeyVarName) ?? throw new Exception("Api Key env-var not found.");

      SheetId = Environment.GetEnvironmentVariable(EnvironmentVariables.HikesGoogleSheetIdKeyVarName) ?? throw new Exception("Sheet Id env-var not found.");

      _liveDataSource = new WebRestService(new Uri($"{GoogleSheetsBaseUrl}{SheetId}/values/Sheet1!A1:Z500?key={googleApiKey}"));
    }

    public async Task OnGet()
    {
      InputModel input = null;

      try
      {
        input = await _liveDataSource
          .Get<InputModel>()
          .ConfigureAwait(false);
      }
      catch (Exception)
      {
        // Yep.
      }

      if (input == null)
      {
        return;
      }

      var parsedInput = new InputParser(input);

      Scoreboard = new Scoreboard(parsedInput.Competitors);
    }
  }
}
