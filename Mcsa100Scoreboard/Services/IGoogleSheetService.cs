using System;
using System.Threading.Tasks;

namespace Mcsa100Scoreboard.Services
{
  public interface IGoogleSheetService
  {
    Task<T> RetrieveInput<T>(Uri address);
  }
}
