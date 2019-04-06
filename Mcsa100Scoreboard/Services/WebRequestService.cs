using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services
{
  internal class WebRequestService : IWebRequestService
  {
    public async Task<T> RetrieveInput<T>(Uri address)
    {
      const int maxAttempts = 5;

      for (int i = 0; i < maxAttempts; i++)
      {
        try
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
        catch (Exception)
        {
          if (i == maxAttempts - 1)
          {
            throw;
          }

          await Task.Delay(1000);
        }
      }

      throw new Exception("Data retrieval failed");
    }

    public async Task<bool> WriteJson(Uri address, string content)
    {
      var httpContent = new StringContent(content);

      httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

      const int maxAttempts = 5;

      for (int i = 0; i < maxAttempts; i++)
      {
        try
        {
          using (var client = new HttpClient())
          {
            using (var response = await client.PutAsync(address, httpContent))
            {
              return response.IsSuccessStatusCode;
            }
          }
        }
        catch (Exception)
        {
          if (i == maxAttempts - 1)
          {
            throw;
          }

          await Task.Delay(1000);
        }
      }

      throw new Exception("Data retrieval failed");
    }
  }
}
