using System;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class RankedCompetitor
  {
    public ICompetitor Competitor { get; }
    public int Rank { get; }

    public RankedCompetitor(
      in ICompetitor competitor,
      in int rank)
    {
      Competitor = competitor ?? throw new ArgumentNullException(nameof(competitor));
      Rank = rank;
    }
  }
}
