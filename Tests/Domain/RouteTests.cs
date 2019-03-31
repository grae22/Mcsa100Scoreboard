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
    public void Create_GivenGradeWithinName_ShouldReturnRouteWithNameAndGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("Route (17) Name");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("Route Name", route.Name);
      Assert.AreEqual(17, route.Grade);
      Assert.True(route.HasGrade);
    }

    [Test]
    public void Create_GivenGradeBeforeName_ShouldReturnRouteWithNameAndGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("(17) Route Name");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("Route Name", route.Name);
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

    [Test]
    public void Create_GivenNameAndLetterGrade_ShouldReturnRouteWithNameAndSportGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName (F1)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("RouteName", route.Name);
      Assert.AreEqual(13, route.Grade);
      Assert.True(route.HasGrade);
    }

    [Test]
    public void Create_GivenNameAndUnknownLetterGrade_ShouldReturnRouteWithNameAndNoGrade()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName (X)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("RouteName", route.Name);
      Assert.False(route.HasGrade);
    }

    [Test]
    public void GradeFriendly_GivenSportGrade_ShouldReturnInSportFormat()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName (19)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("(19)", route.GradeFriendly);
    }

    [Test]
    public void GradeFriendly_GivenOldSaGrade_ShouldReturnInOldSaFormat()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName (F1)");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("(F1)", route.GradeFriendly);
    }

    [Test]
    public void GradeFriendly_GivenNoGrade_ShouldReturnQuestionMark()
    {
      // Arrange.
      // Act.
      Route route = Route.Create("RouteName");

      // Assert.
      Assert.NotNull(route);
      Assert.AreEqual("(?)", route.GradeFriendly);
    }
  }
}
