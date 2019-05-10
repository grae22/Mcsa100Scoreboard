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

      hiker1
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) A")
        });

      hiker2
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) A"),
          Scoreable.Create("(S) B"),
          Scoreable.Create("(S) C")
        });

      hiker3
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) B"),
          Scoreable.Create("(S) C")
        });

      hiker3a
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) D"),
          Scoreable.Create("(S) E")
        });

      hiker3b
        .Scoreables
        .Returns(new[]
        {
          Scoreable.Create("(S) F"),
          Scoreable.Create("(S) G")
        });

      var hikers = new[]
      {
        hiker3a,
        hiker3b,
        hiker1,
        hiker2,
        hiker3
      };

      // Act.
      var testObject = new Scoreboard(hikers);

      // Assert.
      Assert.NotNull(testObject.RankedCompetitors);
      Assert.AreEqual(5, testObject.RankedCompetitors.Count());
      Assert.AreSame(hiker2, testObject.RankedCompetitors.First());
      Assert.AreSame(hiker1, testObject.RankedCompetitors.Last());
      Assert.AreSame(hiker3, testObject.RankedCompetitors.ElementAt(1));
      Assert.AreSame(hiker3a, testObject.RankedCompetitors.ElementAt(2));
      Assert.AreSame(hiker3b, testObject.RankedCompetitors.ElementAt(3));
    }
  }
}
