using System.Collections.Generic;

using Mcsa100Scoreboard.Domain;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ClimberAnalysisTests
  {
    [Test]
    public void HighestGradeClimbed_GivenClimberWithClimbs_ShouldReturnGradeOfHighestClimb()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (15)"),
          Route.Create("Route3 (20)"),
          Route.Create("RouteX")
        });

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.HighestGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(20, result);
    }

    [Test]
    public void HighestGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfHighestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.HighestGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
      Assert.IsNull(result);
    }

    [Test]
    public void LowestGradeClimbed_GivenClimberWithClimbs_ShouldReturnGradeOfLowestClimb()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (15)"),
          Route.Create("Route3 (20)"),
          Route.Create("RouteX")
        });

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.LowestGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(10, result);
    }

    [Test]
    public void LowestGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfLowestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.LowestGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
      Assert.IsNull(result);
    }

    [Test]
    public void AverageGradeClimbed_GivenClimberWithClimbs_ShouldReturnGradeOfAverageClimb()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (20)"),
          Route.Create("RouteX")
        });

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.AverageGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(15, result);
    }

    [Test]
    public void AverageGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfAverageClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber);

      // Act.
      int? result = testObject.AverageGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
      Assert.IsNull(result);
    }
  }
}
