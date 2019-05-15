using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Climbs
{
  public class ClimberRanker
  {
    public IReadOnlyDictionary<IClimber, int> RankingByClimber => _rankedClimbers;

    private readonly Dictionary<IClimber, int> _rankedClimbers = new Dictionary<IClimber, int>();

    public ClimberRanker(in IEnumerable<IClimber> climbers)
    {
      if (climbers == null)
      {
        throw new ArgumentNullException(nameof(climbers));
      }

      BuildRankings(climbers);
    }

    private void BuildRankings(in IEnumerable<IClimber> climbers)
    {
      var climbersGroupedByRouteCounts = climbers.GroupBy(c => c.RouteCount);

      int rank = 1;

      foreach (var grouping in climbersGroupedByRouteCounts.OrderByDescending(g => g.Key))
      {
        foreach (var climber in grouping)
        {
          _rankedClimbers.Add(climber, rank);
        }

        rank += grouping.Count();
      }
    }
  }
}
