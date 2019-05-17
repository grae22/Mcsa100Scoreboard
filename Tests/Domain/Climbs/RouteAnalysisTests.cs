using System.Collections.Generic;
using System.Linq;

using Mcsa100Scoreboard.Domain.Climbs;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain.Climbs
{
  [TestFixture]
  public class RouteAnalysisTests
  {
    [Test]
    public void PopularRoutes_GivenLimit_ShouldReturnNoMoreRoutesThanLimit()
    {
      // Arrange.
      var climbers = new List<IClimber>
      {
        Substitute.For<IClimber>(),
        Substitute.For<IClimber>()
      };

      var climber1 = climbers[0];
      var climber2 = climbers[1];

      climber1
        .Routes
        .Returns(
          new[]
          {
            Route.Create("Route1"),
            Route.Create("Route2")
          });

      climber2
        .Routes
        .Returns(
          new[]
          {
            Route.Create("Route1"),
            Route.Create("Route2"),
            Route.Create("Route3")
          });

      // Act.
      var testObject = new RouteAnalysis(climbers, 2);

      // Assert.
      Assert.AreEqual(2, testObject.PopularRoutes.Count());
    }

    [Test]
    public void PopularRoutes_GivenRoutes_ShouldReturnCorrectAscentCounts()
    {
      // Arrange.
      var climbers = new List<IClimber>
      {
        Substitute.For<IClimber>(),
        Substitute.For<IClimber>()
      };

      var climber1 = climbers[0];
      var climber2 = climbers[1];

      climber1
        .Routes
        .Returns(
          new[]
          {
            Route.Create("Route1"),
            Route.Create("Route2")
          });

      climber2
        .Routes
        .Returns(
          new[]
          {
            Route.Create("Route1"),
            Route.Create("Route2"),
            Route.Create("Route3")
          });

      // Act.
      var testObject = new RouteAnalysis(climbers, 3);

      // Assert.
      Assert.AreEqual(2, testObject.PopularRoutes.ElementAt(0).AscentCount);
      Assert.AreEqual(2, testObject.PopularRoutes.ElementAt(1).AscentCount);
      Assert.AreEqual(1, testObject.PopularRoutes.ElementAt(2).AscentCount);
    }
  }
}
