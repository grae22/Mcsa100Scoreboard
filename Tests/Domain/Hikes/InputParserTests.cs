using System.Collections.Generic;
using System.Linq;

using Mcsa100Scoreboard.Domain.Hikes;
using Mcsa100Scoreboard.Models;

using NUnit.Framework;

namespace Tests.Domain.Hikes
{
  [TestFixture]
  public class InputParserTests
  {
    [Test]
    public void Competitors_GivenValidInput_ShouldReturnCompetitorsAndScoreables()
    {
      // Arrange.
      InputModel input = TestData.TestDataUtils.LoadHikesTestInput(@"TestData\HikesInputParserTests_TestData.json");

      var testObject = new InputParser(input);

      // Act.
      IEnumerable<Competitor> competitors = testObject.Competitors;

      // Assert.
      Assert.NotNull(competitors);
      Assert.True(competitors.Any(c => c.Name == "Graeme"));
      Assert.AreEqual(9, competitors.First(c => c.Name == "Graeme").Scoreables.Count());

      Assert.True(competitors.Any(c => c.Name == "Person1"));
      Assert.AreEqual(3, competitors.First(c => c.Name == "Person1").Scoreables.Count());

      Assert.True(competitors.Any(c => c.Name == "Person2"));
      Assert.AreEqual(1, competitors.First(c => c.Name == "Person2").Scoreables.Count());
    }
  }
}
