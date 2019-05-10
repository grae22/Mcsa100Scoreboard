using System.Linq;

using Mcsa100Scoreboard.Domain.Hikes;

using NUnit.Framework;

namespace Tests.Domain.Hikes
{
  [TestFixture]
  public class CompetitorTests
  {
    [Test]
    public void AddScoreable_GivenValidScoreable_ShouldBePresentInScoreablesProperty()
    {
      // Arrange.
      var testObject = new Competitor("SomeName");

      // Act.
      testObject.AddScoreable("(S) Some Summit");

      // Assert.
      Assert.AreEqual(1, testObject.Scoreables.Count(s => s.Name == "Some Summit"));
    }
  }
}
