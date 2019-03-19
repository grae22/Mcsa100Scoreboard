using System;
using System.Threading.Tasks;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;

using Microsoft.AspNetCore.Mvc;

namespace Mcsa100Scoreboard.Controllers
{
  internal class ScoreboardController : Controller
  {
    // TODO: Remove.
    private const string ApiKey = "AIzaSyAYhi3At01IDR5rbEIM8XSXruvgk-5NDMU";

    private readonly IGoogleSheetService _googleSheetService;

    public ScoreboardController(IGoogleSheetService googleSheetService)
    {
      _googleSheetService = googleSheetService ?? throw new ArgumentNullException(nameof(googleSheetService));
    }

    public async Task<IActionResult> Index()
    {
      var address = new Uri($"https://sheets.googleapis.com/v4/spreadsheets/1qYBulO5nLpFs574h49En8POnvQjXcdVB6VpU1gMBIQQ?ranges=A1:z500?key={ApiKey}");
      var input = await _googleSheetService.RetrieveInput<InputModel>(address);
      var scoreboard = new Scoreboard(input);

      return View();
    }
  }
}