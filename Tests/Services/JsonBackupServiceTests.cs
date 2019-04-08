using Mcsa100Scoreboard.Services;

using NSubstitute;

using NUnit.Framework;

namespace Tests.Services
{
  [TestFixture]
  public class JsonBackupServiceTests
  {
    [Test]
    public void Add_GivenJson_ShouldMakeRequestWithTimestampedJson()
    {
      // Arrange.
      var webRequestService = Substitute.For<IWebRestService>();
      var testObject = new JsonBackupService(webRequestService);

      // Act.
      testObject.Add("{}");

      // Assert.
    }
  }
}
