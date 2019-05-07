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
    public void Add_GivenSameData_ShouldNotMakeExternalCall()
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
        .Add("{}")
        .GetAwaiter()
        .GetResult();

      // Assert.
      webRequestService
        .Received(0)
        .Put(Arg.Any<string>());
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

    [Test]
    public void GetOldest_GivenExistingBackups_ShouldReturnTheOldest()
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
              { "2019-04-06", "{\"key1\": 123}" },
              { "2019-04-02", "{\"key2\": 456}" },
              { "2019-04-07", "{\"key3\": 789}" }
            }
          });

      // Act.
      string result = testObject
        .GetOldest()
        .GetAwaiter()
        .GetResult();

      // Assert.
      Assert.AreEqual("{\"key2\": 456}", result);
    }

    [Test]
    public void GetOldest_GivenNoBackups_ShouldReturnNull()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService, 0);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      // Act.
      string result = testObject
        .GetOldest()
        .GetAwaiter()
        .GetResult();

      // Assert.
      Assert.IsNull(result);
    }

    [Test]
    public void GetNewest_GivenExistingBackups_ShouldReturnTheNewest()
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
              { "2019-04-06", "{\"key1\": 123}" },
              { "2019-04-02", "{\"key2\": 456}" },
              { "2019-04-08", "{\"key3\": 789}" }
            }
          });

      // Act.
      string result = testObject
        .GetNewest(true)
        .GetAwaiter()
        .GetResult();

      // Assert.
      Assert.AreEqual("{\"key1\": 123}", result);
    }

    [Test]
    public void GetNewest_GivenNoBackups_ShouldReturnNull()
    {
      // Arrange.
      var timeService = Substitute.For<ITimeService>();
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(timeService, webRequestService, 0);

      timeService.Now.Returns(new DateTime(2019, 4, 8));

      // Act.
      string result = testObject
        .GetNewest(true)
        .GetAwaiter()
        .GetResult();

      // Assert.
      Assert.IsNull(result);
    }

    [Test]
    public void GetNewest_GivenGetOldestPreviouslyCalled_ShouldNotMakeExternalCallForData()
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
              { "2019-04-06", "{\"key1\": 123}" },
              { "2019-04-02", "{\"key2\": 456}" },
              { "2019-04-08", "{\"key3\": 789}" }
            }
          });

      // Act.
      testObject
        .GetOldest()
        .GetAwaiter()
        .GetResult();

      testObject
        .GetNewest(true)
        .GetAwaiter()
        .GetResult();

      // Assert.
      webRequestService
        .Received(1)
        .Get<JsonBackupData>();
    }
  }
}
