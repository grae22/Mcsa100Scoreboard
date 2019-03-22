using System.Linq;

using Mcsa100Scoreboard.Domain;

using NUnit.Framework;

namespace Tests.Domain
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
      Climber climber = Climber.Create(name, routes);

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
      Climber climber = Climber.Create(name, routes);

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
      Climber climber = Climber.Create(name, routes);

      // Assert.
      Assert.AreEqual(routes.Length, climber.RouteCount);
    }
  }
}
