using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class Scoreboard
  {
    public IEnumerable<RankedCompetitor> RankedCompetitors
    {
      get
      {
        return _rankedCompetitors
          .OrderBy(c => c.Rank)
          .ThenBy(c => c.Competitor.Name);
      }
    }

    private readonly List<RankedCompetitor> _rankedCompetitors = new List<RankedCompetitor>();

    public Scoreboard(in IEnumerable<ICompetitor> competitors)
    {
      if (competitors == null)
      {
        return;
      }

      BuildRankings(competitors);
    }

    private void BuildRankings(in IEnumerable<ICompetitor> competitors)
    {
      var competitorsGroupedByScoreableCounts = competitors.GroupBy(c => c.Scoreables.Count());

      int rank = 1;

      foreach (var grouping in competitorsGroupedByScoreableCounts.OrderByDescending(g => g.Key))
      {
        foreach (var competitor in grouping)
        {
          _rankedCompetitors.Add(
            new RankedCompetitor(competitor, rank));
        }

        rank += grouping.Count();
      }
    }
  }
}
