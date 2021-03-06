﻿using Mcsa100Scoreboard.Domain.Hikes;

using NUnit.Framework;

namespace Tests.Domain.Hikes
{
  [TestFixture]
  public class ScoreableTests
  {
    [TestCase("(S) Name")]
    [TestCase("(P) Name")]
    [TestCase("(C) Name")]
    [TestCase("(s) Name")]
    [TestCase("(p) Name")]
    [TestCase("(c) Name")]
    public void Create_GivenCorrectTypeId_ShouldReturnScoreable(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual(name[1].ToString().ToUpper()[0], result.TypeId);
    }

    [TestCase("(X) Name")]
    [TestCase("(X Name")]
    [TestCase("X) Name")]
    [TestCase("X Name")]
    [TestCase("S Name")]
    [TestCase("C Name")]
    [TestCase("P Name")]
    public void Create_GivenInvalidTypeId_ShouldReturnWithUnknownTypeId(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual(Scoreable.UnknownTypeId, result.TypeId);
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase(null)]
    public void Create_GivenNullOrEmptyRawName_ShouldReturnNull(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.Null(result);
    }

    [TestCase("(S) Some Scoreable")]
    [TestCase("(P) Some Scoreable")]
    [TestCase("(C) Some Scoreable")]
    [TestCase("(C)Some Scoreable")]
    [TestCase("(C)Some Scoreable ")]
    [TestCase("(C) Some Scoreable ")]
    public void Create_GivenValidName_ShouldReturnScoreable(in string name)
    {
      // Arrange.
      // Act.
      Scoreable result = Scoreable.Create(name);

      // Assert.
      Assert.NotNull(result);
      Assert.AreEqual("Some Scoreable", result.Name);
      Assert.AreEqual($"{name.Substring(0, 3)} Some Scoreable", result.FullName);
    }
  }
}
