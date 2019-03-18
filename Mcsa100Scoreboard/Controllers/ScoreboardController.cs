using Microsoft.AspNetCore.Mvc;

namespace Mcsa100Scoreboard.Controllers
{
  internal class ScoreboardController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}