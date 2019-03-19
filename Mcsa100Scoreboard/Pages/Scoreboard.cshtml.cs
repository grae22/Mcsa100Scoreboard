﻿using System;
using System.Threading.Tasks;
using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Models;
using Mcsa100Scoreboard.Services;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mcsa100Scoreboard.Pages
{
  public class ScoreboardModel : PageModel
  {
    // TODO: Remove.
    private const string ApiKey = "AIzaSyAYhi3At01IDR5rbEIM8XSXruvgk-5NDMU";

    private readonly IGoogleSheetService _googleSheetService;

    public ScoreboardModel(IGoogleSheetService googleSheetService)
    {
      _googleSheetService = googleSheetService ?? throw new ArgumentNullException(nameof(googleSheetService));
    }

    public async Task OnGet()
    {
      var address = new Uri($"https://sheets.googleapis.com/v4/spreadsheets/1qYBulO5nLpFs574h49En8POnvQjXcdVB6VpU1gMBIQQ/values/Sheet1!A1:Z500?key={ApiKey}");
      var input = await _googleSheetService.RetrieveInput<InputModel>(address);
      var scoreboard = new Scoreboard(input);
    }
  }
}