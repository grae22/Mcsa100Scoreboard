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
      var testObject = new JsonBackupService(timeService, webRequestService, 0);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      // Act.
      testObject
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new JsonBackupData
      {
        DataByTimestamp = new Dictionary<string, string>
        {
          { "2019-04-08", "{}" }
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
      var testObject = new JsonBackupService(timeService, webRequestService, 0);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      webRequestService
        .Get<JsonBackupData>()
        .Returns(
          new JsonBackupData
          {
            DataByTimestamp = new Dictionary<string, string>
            {
              { "2019-04-07", "{}" }
            }
          });

      // Act.
      testObject
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new JsonBackupData
      {
        DataByTimestamp = new Dictionary<string, string>
        {
          { "2019-04-07", "{}" },
          { "2019-04-08", "{}" }
        }
      };

      webRequestService
        .Received(1)
        .Put(JsonConvert.SerializeObject(expected));
    }

    [Test]
    public void Add_GivenExistingPriorBackupForSameDate_ShouldUpdateExisting()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService, 0);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      webRequestService
        .Get<JsonBackupData>()
        .Returns(
          new JsonBackupData
          {
            DataByTimestamp = new Dictionary<string, string>
            {
              { "2019-04-08", "{}" }
            }
          });

      // Act.
      testObject
        .Add("{\"key\":\"value\"}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new JsonBackupData
      {
        DataByTimestamp = new Dictionary<string, string>
        {
          { "2019-04-08", "{\"key\":\"value\"}" }
        }
      };

      webRequestService
        .Received(1)
        .Put(JsonConvert.SerializeObject(expected));
    }

    [Test]
    public void Add_GivenOldPriorBackup_ShouldRemoveTheOldBackup()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService, 7);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      webRequestService
        .Get<JsonBackupData>()
        .Returns(
          new JsonBackupData
          {
            DataByTimestamp = new Dictionary<string, string>
            {
              { "2019-03-31", "{}" }
            }
          });

      // Act.
      testObject
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      var expected = new JsonBackupData
      {
        DataByTimestamp = new Dictionary<string, string>
        {
          { "2019-04-08", "{}" }
        }
      };

      webRequestService
        .Received(1)
        .Put(JsonConvert.SerializeObject(expected));
    }
  }
}
