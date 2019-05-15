using System.Linq;

using Mcsa100Scoreboard.Domain.Hikes;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Domain.Hikes
{
  [TestFixture]
  public class ScoreboardTests
  {
    [Test]
    public void RankedHikers_GivenHikers_ShouldRankCorrectly()
    {
      // Arrange.
      var hiker1 = Substitute.For<ICompetitor>();
      var hiker2 = Substitute.For<ICompetitor>();
      var hiker3 = Substitute.For<ICompetitor>();
      var hiker3a = Substitute.For<ICompetitor>();
      var hiker3b = Substitute.For<ICompetitor>();
      var hiker4 = Substitute.For<ICompetitor>();

      hiker1
        .Name
        .Returns("1");

      hiker2
        .Name
        .Returns("2");

      hiker3
        .Name
        .Returns("3");

      hiker3a
        .Name
        .Returns("3a");

      hiker3b
        .Name
        .Returns("3b");

      hiker4
        .Name
        .Returns("4");

      hiker1
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) A"),
          Scoreable.Create("(S) B")
        });

      hiker2
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) A"),
          Scoreable.Create("(S) B"),
          Scoreable.Create("(S) C"),
          Scoreable.Create("(S) D")
        });

      hiker3
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) B"),
          Scoreable.Create("(S) C"),
          Scoreable.Create("(S) D")
        });

      hiker3a
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) D"),
          Scoreable.Create("(S) E"),
          Scoreable.Create("(S) F")
        });

      hiker3b
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) F"),
          Scoreable.Create("(S) G"),
          Scoreable.Create("(S) H")
        });

      hiker4
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) X")
        });

      var hikers = new[]
      {
        hiker3a,
        hiker3b,
        hiker1,
        hiker4,
        hiker2,
        hiker3
      };

      // Act.
      var testObject = new Scoreboard(hikers);

      // Assert.
      Assert.NotNull(testObject.RankedCompetitors);

      Assert.AreEqual(6, testObject.RankedCompetitors.Count());

      Assert.AreEqual("2", testObject.RankedCompetitors.First().Competitor.Name);
      Assert.AreEqual("3", testObject.RankedCompetitors.ElementAt(1).Competitor.Name);
      Assert.AreEqual("3a", testObject.RankedCompetitors.ElementAt(2).Competitor.Name);
      Assert.AreEqual("3b", testObject.RankedCompetitors.ElementAt(3).Competitor.Name);
      Assert.AreEqual("1", testObject.RankedCompetitors.ElementAt(4).Competitor.Name);
      Assert.AreEqual("4", testObject.RankedCompetitors.Last().Competitor.Name);

      Assert.AreEqual(1, testObject.RankedCompetitors.First().Rank);
      Assert.AreEqual(2, testObject.RankedCompetitors.ElementAt(1).Rank);
      Assert.AreEqual(2, testObject.RankedCompetitors.ElementAt(2).Rank);
      Assert.AreEqual(2, testObject.RankedCompetitors.ElementAt(3).Rank);
      Assert.AreEqual(5, testObject.RankedCompetitors.ElementAt(4).Rank);
      Assert.AreEqual(6, testObject.RankedCompetitors.Last().Rank);
    }
  }
}
