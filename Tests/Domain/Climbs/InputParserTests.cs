﻿using System.Collections.Generic;
using System.Linq;

using Mcsa100Scoreboard.Domain.Climbs;
using Mcsa100Scoreboard.Models;

using NUnit.Framework;

namespace Tests.Domain.Climbs
{
  [TestFixture]
  public class InputParserTests
  {
    [Test]
    public void Climbers_GivenValidInput_ShouldReturnClimbersAndClimbs()
    {
      // Arrange.
      InputModel input = TestData.TestDataUtils.LoadClimbsTestInput(@"TestData\InputParserTests_TestData.json");

      var testObject = new InputParser(input);

      // Act.
      IEnumerable<Climber> climbers = testObject.Climbers;

      // Assert.
      Assert.NotNull(climbers);
      Assert.True(climbers.Any(c => c.Name == "Neil"));
      Assert.AreEqual(17, climbers.First(c => c.Name == "Neil").RouteCount);

      Assert.True(climbers.Any(c => c.Name == "Bruce"));
      Assert.AreEqual(14, climbers.First(c => c.Name == "Bruce").RouteCount);

      Assert.True(climbers.Any(c => c.Name == "Marco"));
      Assert.AreEqual(24, climbers.First(c => c.Name == "Marco").RouteCount);

      Assert.True(climbers.Any(c => c.Name == "Gavin"));
      Assert.AreEqual(1, climbers.First(c => c.Name == "Gavin").OverrideScoreboardPosition);

      Assert.False(climbers.Any(c => c.Name == "Climb number"));
    }
  }
}
