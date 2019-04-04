using System.IO;

using Mcsa100Scoreboard.Models;

using Newtonsoft.Json;

namespace Tests.TestData
{
  internal static class TestDataUtils
  {
    private static string _rawData;

    public static InputModel LoadTestInput(in string path)
    {
      if (_rawData == null)
      {
        _rawData = File.ReadAllText(path);
      }

      return JsonConvert.DeserializeObject<InputModel>(_rawData);
    }
  }
}
