using System.Text.RegularExpressions;

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
      var route1 = Route.Create("Route1 (19)");
      var route2 = Route.Create("Route2 (22)");

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

      climberInstance2
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
      StringAssert.Contains("ClimberName added 'Route2 (22)'", StripHtmlTags(testObject.Narrative));
    }

    [Test]
    public void Narrative_GivenClimberAddedSeveralClimbs_ShouldListFirstClimbAndCountForOthers()
    {
      // Arrange.
      var route1 = Route.Create("Route1");
      var route2 = Route.Create("Route2 (F1)");
      var route3 = Route.Create("Route3");

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
        .Returns(new[] { route1, route2, route3 });

      climberInstance1
        .Climber
        .RouteCount
        .Returns(1);

      climberInstance2
        .Climber
        .RouteCount
        .Returns(3);

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
      StringAssert.Contains("ClimberName added 'Route2 (F1)' and 1 other climb(s)", StripHtmlTags(testObject.Narrative));
    }

    [Test]
    public void Narrative_GivenNewClimber_ShouldReportClimberJoined()
    {
      // Arrange.
      var route1 = Route.Create("Route1");
      var route2 = Route.Create("Route2");
      var route3 = Route.Create("Route3");

      var climber = Substitute.For<IClimberAnalysis>();

      climber
        .Climber
        .Name
        .Returns("ClimberName");

      climber
        .Climber
        .Routes
        .Returns(new[] { route1, route2, route3 });

      climber
        .Climber
        .RouteCount
        .Returns(3);

      var oldScoreboard = Substitute.For<IScoreboard>();
      var newScoreboard = Substitute.For<IScoreboard>();

      newScoreboard
        .AnalysedClimbersInRankOrder
        .Returns(new[] { climber });

      // Act.
      var testObject = new ScoreboardNarrator(oldScoreboard, newScoreboard);

      // Assert.
      StringAssert.Contains("ClimberName joined and added 3 climb(s)", StripHtmlTags(testObject.Narrative));
    }

    [Test]
    public void Narrative_GivenNewClimberWithNoClimbs_ShouldReportClimberJoined()
    {
      // Arrange.
      var climber = Substitute.For<IClimberAnalysis>();

      climber
        .Climber
        .Name
        .Returns("ClimberName");

      var oldScoreboard = Substitute.For<IScoreboard>();
      var newScoreboard = Substitute.For<IScoreboard>();

      newScoreboard
        .AnalysedClimbersInRankOrder
        .Returns(new[] { climber });

      // Act.
      var testObject = new ScoreboardNarrator(oldScoreboard, newScoreboard);

      // Assert.
      StringAssert.Contains("ClimberName joined", StripHtmlTags(testObject.Narrative));
    }

    private static string StripHtmlTags(in string html)
    {
      return Regex.Replace(html, "<.*?>", string.Empty);
    }
  }
}
