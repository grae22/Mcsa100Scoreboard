using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class Scoreboard
  {
    public IEnumerable<ICompetitor> RankedCompetitors { get; }

    public Scoreboard(in IEnumerable<ICompetitor> competitors)
    {
      if (competitors == null)
      {
        throw new ArgumentNullException(nameof(competitors));
      }

      RankedCompetitors = competitors
        .OrderByDescending(c => c.Scoreables.Count())
        .ThenBy(c => c.Name);
    }
  }
}
