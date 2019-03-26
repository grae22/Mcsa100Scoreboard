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

      climber.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (15)"),
          Route.Create("Route3 (20)")
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.HighestGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(20, result);
    }

    [Test]
    public void HighestGradeClimbed_GivenClimberWithNoGradedClimbs_ShouldReturnGradeOfHighestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1"),
          Route.Create("Route2"),
          Route.Create("Route3")
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.HighestGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
    }

    [Test]
    public void HighestGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfHighestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

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

      climber.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (15)"),
          Route.Create("Route3 (20)")
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.LowestGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(10, result);
    }

    [Test]
    public void LowestGradeClimbed_GivenClimberWithNoGradedClimbs_ShouldReturnGradeOfHighestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1"),
          Route.Create("Route2"),
          Route.Create("Route3")
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.LowestGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
    }

    [Test]
    public void LowestGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfLowestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

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

      climber.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (20)"),
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.AverageGradeClimbed;

      // Assert.
      Assert.True(result.HasValue);
      Assert.AreEqual(15, result);
    }

    [Test]
    public void AverageGradeClimbed_GivenClimberWithNoGradedClimbs_ShouldReturnGradeOfHighestClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(
        new[]
        {
          Route.Create("Route1"),
          Route.Create("Route2"),
          Route.Create("Route3")
        });

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.AverageGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
    }

    [Test]
    public void AverageGradeClimbed_GivenClimberWithNoClimbs_ShouldReturnGradeOfAverageClimbAsNull()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      climber.Routes.Returns(new List<Route>());

      var testObject = new ClimberAnalysis(climber, 1, new[] { climber });

      // Act.
      int? result = testObject.AverageGradeClimbed;

      // Assert.
      Assert.False(result.HasValue);
      Assert.IsNull(result);
    }

    [Test]
    public void HasHighestGradedClimb_GivenClimbersWithHighestGrade_ShouldReturnTrue()
    {
      // Arrange.
      var climber1 = Substitute.For<IClimber>();
      var climber2 = Substitute.For<IClimber>();
      var climber3 = Substitute.For<IClimber>();
      var climber4 = Substitute.For<IClimber>();

      climber1.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (30)"),
          Route.Create("Route2 (15)"),
          Route.Create("Route3 (20)")
        });

      climber2.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (15)"),
          Route.Create("Route2 (20)"),
          Route.Create("Route3 (25)")
        });

      climber3.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (20)"),
          Route.Create("Route2 (25)"),
          Route.Create("Route3 (30)")
        });

      var climbers = new[]
      {
        climber1,
        climber2,
        climber3,
        climber4
      };

      // Act.
      var climber1Analysis = new ClimberAnalysis(climber1, 1, climbers);
      var climber2Analysis = new ClimberAnalysis(climber2, 1, climbers);
      var climber3Analysis = new ClimberAnalysis(climber3, 1, climbers);
      var climber4Analysis = new ClimberAnalysis(climber4, 1, climbers);

      // Assert.
      Assert.True(climber1Analysis.HasHighestGradedClimb);
      Assert.False(climber2Analysis.HasHighestGradedClimb);
      Assert.True(climber3Analysis.HasHighestGradedClimb);
      Assert.False(climber4Analysis.HasHighestGradedClimb);
    }

    [Test]
    public void HasHighestAverageGrade_GivenClimbersWithHighestAverageGrade_ShouldReturnTrue()
    {
      // Arrange.
      var climber1 = Substitute.For<IClimber>();
      var climber2 = Substitute.For<IClimber>();
      var climber3 = Substitute.For<IClimber>();
      var climber4 = Substitute.For<IClimber>();

      climber1.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (15)")
        });

      climber2.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (20)")
        });

      climber3.GradedRoutes.Returns(
        new[]
        {
          Route.Create("Route1 (10)"),
          Route.Create("Route2 (12)"),
          Route.Create("Route3 (20)")
        });

      var climbers = new[]
      {
        climber1,
        climber2,
        climber3,
        climber4
      };

      // Act.
      var climber1Analysis = new ClimberAnalysis(climber1, 1, climbers);
      var climber2Analysis = new ClimberAnalysis(climber2, 1, climbers);
      var climber3Analysis = new ClimberAnalysis(climber3, 1, climbers);
      var climber4Analysis = new ClimberAnalysis(climber4, 1, climbers);

      // Assert.
      Assert.True(climber1Analysis.HasHighestAverageGrade);
      Assert.True(climber2Analysis.HasHighestAverageGrade);
      Assert.False(climber3Analysis.HasHighestAverageGrade);
      Assert.False(climber4Analysis.HasHighestAverageGrade);
    }
  }
}
