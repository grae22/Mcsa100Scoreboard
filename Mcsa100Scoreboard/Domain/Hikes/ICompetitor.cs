using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public interface ICompetitor
  {
    string Name { get; }
    IEnumerable<Scoreable> Scoreables { get; }
  }
}
