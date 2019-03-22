using System;

using Mcsa100Scoreboard.Domain;

using NUnit.Framework;

namespace Tests.Domain
{
  [TestFixture]
  public class CountdownBuilderTests
  {
    [Test]
    [TestCase("2020/1/1", "2019/1/1", "12 months", null)]
    [TestCase("2020/1/1", "2019/12/1", "1 month", "months")]
    [TestCase("2020/1/1", "2019/12/3", null, "month")]
    public void Build_GivenDate_ShouldReturnCorrectMonthsRemainingValue(
      in string end,
      in string now,
      in string expected,
      in string notExpected)
    {
      // Arrange.
      // Act.
      var result = CountdownBuilder.Build(DateTime.Parse(end), DateTime.Parse(now));

      // Assert.
      if (expected != null)
      {
        StringAssert.Contains(expected, result);
      }

      if (notExpected != null)
      {
        StringAssert.DoesNotContain(notExpected, result);
      }
    }

    [Test]
    [TestCase("2020/1/1", "2019/1/16", "2 weeks", null)]
    [TestCase("2020/1/1", "2019/1/14", "3 weeks", null)]
    [TestCase("2020/1/1", "2019/1/30", null, "week")]
    public void Build_GivenDate_ShouldReturnCorrectWeeksRemainingValue(
      in string end,
      in string now,
      in string expected,
      in string notExpected)
    {
      // Arrange.
      // Act.
      var result = CountdownBuilder.Build(DateTime.Parse(end), DateTime.Parse(now));

      // Assert.
      if (expected != null)
      {
        StringAssert.Contains(expected, result);
      }

      if (notExpected != null)
      {
        StringAssert.DoesNotContain(notExpected, result);
      }
    }

    [Test]
    [TestCase("2020/1/1 00:00:00", "2019/12/30 12:00:00", "1 day", null)]
    public void Build_GivenDate_ShouldReturnCorrectDaysRemainingValue(
      in string end,
      in string now,
      in string expected,
      in string notExpected)
    {
      // Arrange.
      // Act.
      var result = CountdownBuilder.Build(DateTime.Parse(end), DateTime.Parse(now));

      // Assert.
      if (expected != null)
      {
        StringAssert.Contains(expected, result);
      }

      if (notExpected != null)
      {
        StringAssert.DoesNotContain(notExpected, result);
      }
    }

    [Test]
    [TestCase("2020/1/1 00:00:00", "2019/11/23 12:00:00", "1 month, 1 week, 1 day", null)]
    public void Build_GivenDate_ShouldReturnCorrectRemainingValue(
      in string end,
      in string now,
      in string expected,
      in string notExpected)
    {
      // Arrange.
      // Act.
      var result = CountdownBuilder.Build(DateTime.Parse(end), DateTime.Parse(now));

      // Assert.
      if (expected != null)
      {
        StringAssert.Contains(expected, result);
      }

      if (notExpected != null)
      {
        StringAssert.DoesNotContain(notExpected, result);
      }
    }
  }
}
