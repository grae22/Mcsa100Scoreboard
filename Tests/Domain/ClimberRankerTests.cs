using System.Collections.Generic;

using Mcsa100Scoreboard.Domain;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ClimberRankerTests
  {
    [Test]
    public void RankingByClimber_GivenClimbers_ShouldRankByRouteCount()
    {
      // Arrange.
      var climberWith1Route = Climber.Create("Name1", new string[] { "Route1" });
      var climberWith2Routes = Climber.Create("Name2", new string[] { "Route1", "Route2" });
      var climberWith3Routes = Climber.Create("Name3", new string[] { "Route1", "Route2", "Route3" });

      var climbers = new List<Climber>
      {
        climberWith1Route,
        climberWith2Routes,
        climberWith3Routes
      };

      var testObject = new ClimberRanker(climbers);

      // Act.
      var result = testObject.RankingByClimber;

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual(1, result[climberWith3Routes]);
      Assert.AreEqual(2, result[climberWith2Routes]);
      Assert.AreEqual(3, result[climberWith1Route]);
    }

    [Test]
    public void RankingByClimber_GivenClimbersWithSameRouteCount_ShouldRankByRouteCount()
    {
      // Arrange.
      var climberWith1Route = Climber.Create("Name1", new string[] { "Route1" });
      var climberWith2Routes1 = Climber.Create("Name2", new string[] { "Route1", "Route2" });
      var climberWith2Routes2 = Climber.Create("Name3", new string[] { "Route1", "Route2" });

      var climbers = new List<Climber>
      {
        climberWith1Route,
        climberWith2Routes1,
        climberWith2Routes2
      };

      var testObject = new ClimberRanker(climbers);

      // Act.
      var result = testObject.RankingByClimber;

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual(1, result[climberWith2Routes1]);
      Assert.AreEqual(1, result[climberWith2Routes2]);
      Assert.AreEqual(2, result[climberWith1Route]);
    }
  }
}
