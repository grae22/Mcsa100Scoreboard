using System.IO;

using Mcsa100Scoreboard.Models;

using Newtonsoft.Json;

namespace Tests.TestData
{
  internal static class TestDataUtils
  {
    private static string _climbsRawData;
    private static string _hikesRawData;

    public static InputModel LoadClimbsTestInput(in string path)
    {
      if (_climbsRawData == null)
      {
        _climbsRawData = File.ReadAllText(path);
      }

      return JsonConvert.DeserializeObject<InputModel>(_climbsRawData);
    }

    public static InputModel LoadHikesTestInput(in string path)
    {
      if (_hikesRawData == null)
      {
        _hikesRawData = File.ReadAllText(path);
      }

      return JsonConvert.DeserializeObject<InputModel>(_hikesRawData);
    }
  }
}
