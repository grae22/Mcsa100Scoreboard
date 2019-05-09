using Mcsa100Scoreboard.Domain.Hikes;

using NUnit.Framework;

namespace Tests.Domain.Hikes
{
  [TestFixture]
  public class ScoreableTests
  {
    [TestCase("(S)")]
    [TestCase("(P)")]
    [TestCase("(C)")]
    public void Create_GivenCorrectTypeId_ShouldReturnScoreable(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual(name[1], result.TypeId);
    }

    [TestCase("(X)")]
    [TestCase("(X")]
    [TestCase("X)")]
    [TestCase("X")]
    [TestCase("S")]
    [TestCase("C")]
    [TestCase("P")]
    [TestCase(null)]
    public void Create_GivenInvalidTypeId_ShouldReturnNull(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.Null(result);
    }
  }
}
