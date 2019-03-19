using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class ClimberRanker
  {
    public IReadOnlyDictionary<Climber, int> RankingByClimber => _rankedClimbers;

    private Dictionary<Climber, int> _rankedClimbers = new Dictionary<Climber, int>();

    public ClimberRanker(in IEnumerable<Climber> climbers)
    {
      if (climbers == null)
      {
        throw new ArgumentNullException(nameof(climbers));
      }

      BuildRankings(climbers);
    }

    private void BuildRankings(in IEnumerable<Climber> climbers)
    {
      var climbersGroupedByRouteCounts = climbers.GroupBy(c => c.RouteCount);

      int rank = 1;

      foreach (var grouping in climbersGroupedByRouteCounts.OrderByDescending(g => g.Key))
      {
        foreach (var climber in grouping)
        {
          _rankedClimbers.Add(climber, rank);
        }

        rank++;
      }
    }
  }
}
