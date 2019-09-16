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
      ApplyOverridePositions();
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

    private void ApplyOverridePositions()
    {
      var tmpRankedClimbers = new Dictionary<IClimber, int>();

      _rankedClimbers
        .Where(c => c.Key.OverrideScoreboardPosition.HasValue)
        .OrderBy(c => c.Key.OverrideScoreboardPosition)
        .ToList()
        .ForEach(c => tmpRankedClimbers.Add(c.Key, c.Key.OverrideScoreboardPosition.Value));

      int rankOffset = _rankedClimbers
        .Keys
        .Where(c => c.OverrideScoreboardPosition.HasValue)
        .Max(c => c.OverrideScoreboardPosition) ?? 0;

      _rankedClimbers
        .ToList()
        .ForEach(
          c =>
          {
            if (!tmpRankedClimbers.ContainsKey(c.Key))
            {
              int rank = c.Value;

              if (rank > rankOffset)
              {
                tmpRankedClimbers.Add(c.Key, rank);
              }
              else
              {
                tmpRankedClimbers.Add(c.Key, rank + rankOffset);
              }
            }
          });

      _rankedClimbers.Clear();

      tmpRankedClimbers
        .ToList()
        .ForEach(c => _rankedClimbers.Add(c.Key, c.Value));
    }
  }
}
