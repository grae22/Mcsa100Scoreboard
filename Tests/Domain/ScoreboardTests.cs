using System.Linq;

using Mcsa100Scoreboard.Domain;

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
      Climber[] climbers =
      {
        Climber.Create("Climber1", new[] { Route.Create("C1R1") }),
        Climber.Create("Climber2", new[] { Route.Create("C2R1"), Route.Create("C2R2") }),
        Climber.Create("Climber3", new[] { Route.Create("C3R1"), Route.Create("C3R2"), Route.Create("C3R3") })
      };

      // Act.
      var testObject = new Scoreboard(climbers);

      // Assert.
      IClimberAnalysis firstClimberAnalysis = testObject.AnalysedClimbersInRankOrder.First();
      IClimberAnalysis lastClimberAnalysis = testObject.AnalysedClimbersInRankOrder.Last();

      Assert.AreEqual("Climber3", firstClimberAnalysis.Climber.Name);
      Assert.AreEqual("Climber1", lastClimberAnalysis.Climber.Name);
      Assert.AreEqual(3, firstClimberAnalysis.Climber.RouteCount);
      Assert.AreEqual(1, lastClimberAnalysis.Climber.RouteCount);
    }
  }
}
