using System;
using System.Threading.Tasks;

namespace Mcsa100Scoreboard.Services
{
  public interface IWebRequestService
  {
    Task<T> RetrieveInput<T>(Uri address);
    Task<bool> WriteJson(Uri address, string content);
  }
}
