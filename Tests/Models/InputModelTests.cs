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
      string rawData = File.ReadAllText(@"TestData\InputModelTests_TestData.json");
      
      // Act.
      var model = JsonConvert.DeserializeObject<InputModel>(rawData);

      // Assert.
      Assert.AreEqual("Sheet1!A1:Z500", model.range);
      Assert.AreEqual("ROWS", model.majorDimension);
      Assert.AreEqual("Climb number", model.values[0][0]);
      Assert.AreEqual("1", model.values[1][0]);
      Assert.AreEqual("100", model.values[100][0]);
      Assert.AreEqual("Travel Desk 18", model.values[24][2]);
    }
  }
}
