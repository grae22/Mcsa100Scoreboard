using Mcsa100Scoreboard.Domain;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ScoreboardNarratorTests
  {
    [Test]
    public void Narrative_GivenClimberAddedClimb_ShouldListNewClimb()
    {
      // Arrange.
      var route1 = Route.Create("Route1");
      var route2 = Route.Create("Route2");

      var climberInstance1 = Substitute.For<IClimberAnalysis>();
      var climberInstance2 = Substitute.For<IClimberAnalysis>();

      climberInstance1
        .Climber
        .Name
        .Returns("ClimberName");

      climberInstance2
        .Climber
        .Name
        .Returns("ClimberName");

      climberInstance1
        .Climber
        .Routes
        .Returns(new[] { route1 });

      climberInstance2
        .Climber
        .Routes
        .Returns(new[] { route1, route2 });

      climberInstance1
        .Climber
        .RouteCount
        .Returns(1);

      climberInstance1
        .Climber
        .RouteCount
        .Returns(2);

      var oldScoreboard = Substitute.For<IScoreboard>();
      var newScoreboard = Substitute.For<IScoreboard>();

      oldScoreboard
        .AnalysedClimbersInRankOrder
        .Returns(new[] { climberInstance1 });

      newScoreboard
        .AnalysedClimbersInRankOrder
        .Returns(new[] { climberInstance2 });

      // Act.
      var testObject = new ScoreboardNarrator(oldScoreboard, newScoreboard);

      // Assert.
      StringAssert.Contains("ClimberName added 'Route2'.", testObject.Narrative);
    }

    [Test]
    public void Narrative_GivenNewClimber_ShouldReportClimberJoined()
    {
      // Arrange.

      // Act.

      // Assert.
    }
  }
}
