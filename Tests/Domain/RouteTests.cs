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
      Assert.AreEqual("RouteName (?)", route.NameAndGrade);
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
      Assert.AreEqual("RouteName (17)", route.NameAndGrade);
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
      Assert.AreEqual("RouteName (17)", route.NameAndGrade);
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
      Assert.AreEqual("RouteName (F1)", route.NameAndGrade);
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
      Assert.AreEqual("RouteName (X)", route.NameAndGrade);
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

    [Test]
    public void Equals_GivenSameNameAndGrade_ShouldReturnTrue()
    {
      // Arrange.
      Route route1 = Route.Create("RouteName (19)");
      Route route2 = Route.Create("RouteName (19)");

      // Act.
      bool result1 = route1.Equals(route2);
      bool result2 = route1 == route2;
      bool result3 = route1 != route2;

      // Assert.
      Assert.True(result1);
      Assert.True(result2);
      Assert.False(result3);
    }

    [Test]
    public void Equals_GivenSameNameAndDifferentGrade_ShouldReturnFalse()
    {
      // Arrange.
      Route route1 = Route.Create("RouteName (19)");
      Route route2 = Route.Create("RouteName (10)");

      // Act.
      bool result1 = route1.Equals(route2);
      bool result2 = route1 == route2;
      bool result3 = route1 != route2;

      // Assert.
      Assert.False(result1);
      Assert.False(result2);
      Assert.True(result3);
    }
  }
}
