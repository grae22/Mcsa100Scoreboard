using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services.JsonBackup
{
  internal class JsonBackupService
  {
    private readonly ITimeService _timeService;
    private readonly IWebRestService _webRestService;

    public JsonBackupService(
      in ITimeService timeService,
      in IWebRestService webRestService)
    {
      _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
      _webRestService = webRestService ?? throw new ArgumentNullException(nameof(webRestService));
    }

    public async Task Add(string json)
    {
      var model = new JsonBackupData
      {
        Timestamp = _timeService.Now,
        Data = json
      };

      var allModels = new[]
      {
        model
      };

      string serialisedData = JsonConvert.SerializeObject(allModels);

      await _webRestService.Put(serialisedData);
    }
  }
}
