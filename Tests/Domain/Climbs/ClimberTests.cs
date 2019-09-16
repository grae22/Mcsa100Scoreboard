using System.Linq;

using Mcsa100Scoreboard.Domain.Climbs;

using NUnit.Framework;

namespace Tests.Domain.Climbs
{
  [TestFixture]
  public class ClimberTests
  {
    [Test]
    public void Create_GivenValidData_ShouldReturnNewClimber()
    {
      // Arrange.
      const string name = "Name123";

      Route[] routes =
      {
        Route.Create("Route1"),
        Route.Create("Route2")
      };

      // Act.
      Climber climber = Climber.Create(name, routes, null);

      // Assert.
      Assert.NotNull(climber);
      Assert.AreEqual(name, climber.Name);
      Assert.AreEqual(routes[0], climber.Routes.First());
      Assert.AreEqual(routes[1], climber.Routes.Last());
    }

    [Test]
    public void Create_GivenNoName_ShouldReturnNull()
    {
      // Arrange.
      const string name = null;

      Route[] routes =
      {
        Route.Create("Route1"),
        Route.Create("Route2")
      };

      // Act.
      Climber climber = Climber.Create(name, routes, null);

      // Assert.
      Assert.Null(climber);
    }

    [Test]
    public void RouteCount_GivenClimberWith3Routes_ShouldReturnReturn3()
    {
      // Arrange.
      const string name = "Name123";

      Route[] routes =
      {
        Route.Create("Route1"),
        Route.Create("Route2"),
        Route.Create("Route3")
      };

      // Act.
      Climber climber = Climber.Create(name, routes, null);

      // Assert.
      Assert.AreEqual(routes.Length, climber.RouteCount);
    }

    [Test]
    public void GradedRoutes_GivenRoutes_ShouldReturnOnlyGradedRoutes()
    {
      // Arrange.
      const string name = "Name123";

      Route[] routes =
      {
        Route.Create("Route1 (10)"),
        Route.Create("Route2"),
        Route.Create("Route3 (F3)")
      };

      // Act.
      Climber climber = Climber.Create(name, routes, null);

      // Assert.
      Assert.True(climber.GradedRoutes.Any(r => r.Name == "Route1"));
      Assert.True(climber.GradedRoutes.Any(r => r.Name == "Route3"));
      Assert.False(climber.GradedRoutes.Any(r => r.Name == "Route2"));
    }
  }
}
