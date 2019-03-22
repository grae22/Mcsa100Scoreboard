using Mcsa100Scoreboard.Domain;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class RouteTests
  {
    [Test]
    public void Create_GivenName_ShouldReturnRouteWithName()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("RouteName", route.Name);
      Assert.False(route.HasGrade);
    }

    [Test]
    public void Create_GivenNameAndGrade_ShouldReturnRouteWithNameAndGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName (17)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("RouteName", route.Name);
      Assert.AreEqual(17, route.Grade);
      Assert.True(route.HasGrade);
    }

    [Test]
    public void Create_GivenNameAndGradeNoSpace_ShouldReturnRouteWithNameAndGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName(17)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("RouteName", route.Name);
      Assert.AreEqual(17, route.Grade);
      Assert.True(route.HasGrade);
    }

    [Test]
    public void Create_GivenGradeOnly_ShouldReturnGradeAsNameAndGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("(17)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("(17)", route.Name);
      Assert.AreEqual(17, route.Grade);
      Assert.True(route.HasGrade);
    }
  }
}
