using Mcsa100Scoreboard.Domain;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ClimberAnalyserTests
  {
    [Test]
    public void HighestGradeClimb_GivenClimberWithClimbs_ShouldReturnGradeOfHighestClimb()
    {
      // Arrange.
      var climber = Substitute.For<IClimber>();

      // Act.

      // Assert.
    }
  }
}
