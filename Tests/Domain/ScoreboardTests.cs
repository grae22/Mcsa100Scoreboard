using System.Linq;

using Mcsa100Scoreboard.Domain;
using Mcsa100Scoreboard.Models;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ScoreboardTests
  {
    [Test]
    public void Constructor_GivenInputModel_ShouldRankClimbersByRouteCountDescending()
    {
      // Arrange.
      var model = new InputModel
      {
        Values = new[]
        {
          new[] { "Climb number", "Climber1", "Climber2", "Climber3" },
          new[] { "1", "C1R1", "C2R1", "C3R1" },
          new[] { "2", "", "C2R2", "C3R2" },
          new[] { "3", "", "", "C3R3" },
        }
      };

      // Act.
      var testObject = new Scoreboard(model);

      // Assert.
      Assert.AreEqual("Climber3", testObject.RankedClimbers.First().Name);
      Assert.AreEqual("Climber1", testObject.RankedClimbers.Last().Name);
      Assert.AreEqual(3, testObject.RankedClimbers.First().RouteCount);
      Assert.AreEqual(1, testObject.RankedClimbers.Last().RouteCount);
    }
  }
}
