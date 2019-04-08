using System;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services.JsonBackup
{
  internal class JsonBackupData
  {
    [JsonConverter(typeof(JsonBackupDateSerialiser))]
    public DateTime Timestamp { get; set; }

    public string Data { get; set; }
  }
}
