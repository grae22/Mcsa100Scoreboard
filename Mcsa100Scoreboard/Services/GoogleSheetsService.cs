using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services
{
  internal class GoogleSheetsService : IGoogleSheetService
  {
    public async Task<T> RetrieveInput<T>(Uri address)
    {
      using (var client = new HttpClient())
      {
        using (var response = await client.GetAsync(address))
        {
          using (var content = response.Content)
          {
            string data = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
          }
        }
      }
    }
  }
}
