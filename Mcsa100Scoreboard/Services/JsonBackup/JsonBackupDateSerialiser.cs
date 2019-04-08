using Newtonsoft.Json.Converters;

namespace Mcsa100Scoreboard.Services.JsonBackup
{
  internal class JsonBackupDateSerialiser : IsoDateTimeConverter
  {
    public JsonBackupDateSerialiser()
    {
      DateTimeFormat = "yyyyMMdd";
    }
  }
}