﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Mcsa100Scoreboard.Services
{
  internal class GoogleSheetsService : IGoogleSheetService
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
  }
}
