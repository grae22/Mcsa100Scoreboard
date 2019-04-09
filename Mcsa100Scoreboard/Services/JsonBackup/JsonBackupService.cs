using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services.JsonBackup
{
  internal class JsonBackupService
  {
    private const string KeyDateFormat = "yyyyMMdd";

    private readonly ITimeService _timeService;
    private readonly IWebRestService _webRestService;

    public JsonBackupService(
      in ITimeService timeService,
      in IWebRestService webRestService)
    {
      _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
      _webRestService = webRestService ?? throw new ArgumentNullException(nameof(webRestService));
    }

    public async Task Add(string data)
    {
      JsonBackupData backup = await RetrieveData();

      string key = _timeService.Now.ToString(KeyDateFormat);

      if (backup.DataByTimestamp.ContainsKey(key))
      {
        backup.DataByTimestamp[key] = data;
      }
      else
      {
        backup.DataByTimestamp.Add(key, data);
      }

      string serialisedBackup = JsonConvert.SerializeObject(backup);

      await _webRestService.Put(serialisedBackup);
    }

    private async Task<JsonBackupData> RetrieveData()
    {
      var data = await _webRestService.Get<JsonBackupData>();

      if (data == null)
      {
        data = new JsonBackupData();
      }

      return data;
    }
  }
}
