using System.IO;

using Mcsa100Scoreboard.Models;

using Newtonsoft.Json;

namespace Tests.TestData
{
  internal static class TestDataUtils
  {
    private static string _rawData;

    public static InputModel LoadTestInput()
    {
      if (_rawData == null)
      {
        _rawData = File.ReadAllText(@"TestData\ScoreboardController_TestData.json");
      }

      return JsonConvert.DeserializeObject<InputModel>(_rawData);
    }
  }
}
