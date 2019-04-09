using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services.JsonBackup
{
  internal class JsonBackupService
  {
    private const string KeyDateFormat = "yyyy-MM-dd";

    private readonly ITimeService _timeService;
    private readonly IWebRestService _webRestService;
    private readonly int _maxBackupAgeInDays;

    public JsonBackupService(
      in ITimeService timeService,
      in IWebRestService webRestService,
      in int maxBackupAgeInDays)
    {
      _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
      _webRestService = webRestService ?? throw new ArgumentNullException(nameof(webRestService));
      _maxBackupAgeInDays = maxBackupAgeInDays;
    }

    public async Task Add(string data)
    {
      JsonBackupData backup = await RetrieveData();

      string key = _timeService.Now.ToString(KeyDateFormat);

      if (backup.DataByTimestamp.ContainsKey(key))
      {
        if (backup.DataByTimestamp[key].Equals(data))
        {
          return;
        }

        backup.DataByTimestamp[key] = data;
      }
      else
      {
        backup.DataByTimestamp.Add(key, data);
      }

      RemoveExpiredBackups(backup);

      string serialisedBackup = JsonConvert.SerializeObject(backup);

      await _webRestService.Put(serialisedBackup);
    }

    public async Task<string> GetOldest()
    {
      var data = await RetrieveData();

      if (data.DataByTimestamp.Count == 0)
      {
        return null;
      }

      return data
        .DataByTimestamp
        .OrderBy(d => d.Key)
        .First()
        .Value;
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

    private void RemoveExpiredBackups(in JsonBackupData data)
    {
      if (_maxBackupAgeInDays < 1)
      {
        return;
      }

      DateTime thresholdDate = _timeService.Now.AddDays(-_maxBackupAgeInDays);

      var keysToRemove = new List<string>();

      foreach (string key in data.DataByTimestamp.Keys)
      {
        if (key.Length != KeyDateFormat.Length)
        {
          keysToRemove.Add(key);
          continue;
        }

        if (!DateTime.TryParse(key, out DateTime date))
        {
          keysToRemove.Add(key);
          continue;
        }

        if (date > thresholdDate)
        {
          continue;
        }

        keysToRemove.Add(key);
      }

      foreach (string key in keysToRemove)
      {
        data.DataByTimestamp.Remove(key);
      }
    }
  }
}
