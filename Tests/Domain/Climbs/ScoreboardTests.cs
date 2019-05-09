using System.Linq;

using Mcsa100Scoreboard.Domain.Climbs;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain.Climbs
{
  [TestFixture]
  public class ScoreboardTests
  {
    [Test]
    public void AnalysedClimbersInRankOrder_GivenInputModel_ShouldRankClimbersByRouteCountDescending()
    {
      // Arrange.
      Climber[] climbers =
      {
        Climber.Create("Climber1", new[] { Route.Create("C1R1") }),
        Climber.Create("Climber2", new[] { Route.Create("C2R1"), Route.Create("C2R2") }),
        Climber.Create("Climber3", new[] { Route.Create("C3R1"), Route.Create("C3R2"), Route.Create("C3R3") })
      };

      // Act.
      var testObject = new Scoreboard(climbers, null);

      // Assert.
      IClimberAnalysis firstClimberAnalysis = testObject.AnalysedClimbersInRankOrder.First();
      IClimberAnalysis lastClimberAnalysis = testObject.AnalysedClimbersInRankOrder.Last();

      Assert.AreEqual("Climber3", firstClimberAnalysis.Climber.Name);
      Assert.AreEqual("Climber1", lastClimberAnalysis.Climber.Name);
      Assert.AreEqual(3, firstClimberAnalysis.Climber.RouteCount);
      Assert.AreEqual(1, lastClimberAnalysis.Climber.RouteCount);
    }

    [Test]
    public void AnalysedClimbersInRankOrder_GivenPriorRankings_ShouldReturnCorrectDelta()
    {
      // Arrange.
      Climber[] climbers =
      {
        Climber.Create("Climber1", new[] { Route.Create("C1R1") }),
        Climber.Create("Climber2", new[] { Route.Create("C2R1"), Route.Create("C2R2") })
      };

      var priorAnalysisForClimber = Substitute.For<IClimberAnalysis>();
      priorAnalysisForClimber.Climber.Name.Returns("Climber2");
      priorAnalysisForClimber.Rank.Returns(2);

      var priorAnalyses = new[]
      {
        priorAnalysisForClimber
      };

      // Act.
      var testObject = new Scoreboard(climbers, priorAnalyses);

      // Assert.
      Assert.AreEqual(1, testObject.AnalysedClimbersInRankOrder[0].RankDelta);
    }

    [Test]
    public void AnalysedClimbersInRankOrder_GivenNoPriorRankings_ShouldReturnCorrectDelta()
    {
      // Arrange.
      Climber[] climbers =
      {
        Climber.Create("Climber1", new[] { Route.Create("C1R1") }),
        Climber.Create("Climber2", new[] { Route.Create("C2R1"), Route.Create("C2R2") })
      };

      var priorAnalyses = new ClimberAnalysis[0];

      // Act.
      var testObject1 = new Scoreboard(climbers, priorAnalyses);
      var testObject2 = new Scoreboard(climbers, null);

      // Assert.
      Assert.AreEqual(0, testObject1.AnalysedClimbersInRankOrder[0].RankDelta);
      Assert.AreEqual(0, testObject2.AnalysedClimbersInRankOrder[0].RankDelta);
    }
  }
}
