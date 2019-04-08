using System.Threading.Tasks;

namespace Mcsa100Scoreboard.Services
{
  public interface IWebRestService
  {
    Task<T> Get<T>();
    Task<bool> Put(string content);
  }
}
