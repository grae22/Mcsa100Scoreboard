using System;
using System.Collections.Generic;

using Mcsa100Scoreboard.Services;
using Mcsa100Scoreboard.Services.JsonBackup;

using Newtonsoft.Json;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Services.JsonBackup
{
  [TestFixture]
  public class JsonBackupServiceTests
  {
    [Test]
    public void Add_GivenJson_ShouldMakeRequestWithTimestampedJson()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      // Act.
      testObject
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new[]
      {
        new JsonBackupData
        {
          DataByTimestamp = new Dictionary<string, string>
          {
            { "20190408", "{}" }
          }
        }
      };

      webRequestService
        .Received(1)
        .Put(JsonConvert.SerializeObject(expected));
    }

    [Test]
    public void Add_GivenExistingPriorBackup_ShouldPreserveExistingAndAddNew()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      // Act.
      testObject
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new[]
      {
        new JsonBackupData
        {
          DataByTimestamp = new Dictionary<string, string>
          {
            { "20190408", "{}" }
          }
        }
      };

      webRequestService
        .Received(1)
        .Put(JsonConvert.SerializeObject(expected));

      // Test is WIP.
      Assert.Fail();
    }
  }
}
