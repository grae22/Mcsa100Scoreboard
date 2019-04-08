using System;
using System.Threading.Tasks;

namespace Mcsa100Scoreboard.Services
{
  public class JsonBackupService
  {
    private IWebRestService _webRestService;

    public JsonBackupService(IWebRestService webRestService)
    {
      _webRestService = webRestService ?? throw new ArgumentNullException(nameof(webRestService));
    }

    public async Task Add(string json)
    {
      
    }
  }
}
