using System.IO;

using Mcsa100Scoreboard.Models;

using Newtonsoft.Json;

using NUnit.Framework;

namespace Tests.Models
{
  [TestFixture]
  public class InputModelTests
  {
    [Test]
    public void GivenInputJson_ShouldDerserialiseSuccessfully()
    {
      // Arrange.
      string rawData = File.ReadAllText(@"TestData\ScoreboardController_TestData.json");
      
      // Act.
      var model = JsonConvert.DeserializeObject<InputModel>(rawData);

      // Assert.
      Assert.AreEqual("Sheet1!A1:Z500", model.Range);
      Assert.AreEqual("ROWS", model.MajorDimension);
      Assert.AreEqual("Climb number", model.Values[0][0]);
      Assert.AreEqual("1", model.Values[1][0]);
      Assert.AreEqual("100", model.Values[100][0]);
      Assert.AreEqual("Travel Desk 18", model.Values[24][2]);
    }
  }
}
