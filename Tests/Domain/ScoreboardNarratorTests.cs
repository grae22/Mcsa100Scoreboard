using Mcsa100Scoreboard.Domain;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class ScoreboardNarratorTests
  {
    [Test]
    public void Narrative_GivenClimberAddedClimb_ShouldListNewClimb()
    {
      // Arrange.
      var oldScoreboard = Substitute.For<IScoreboard>();
      var newScoreboard = Substitute.For<IScoreboard>();

      //oldScoreboard
      //  .AnalysedClimbersInRankOrder
      //  .Returns()

      // Act.

      // Assert.
    }
  }
}
