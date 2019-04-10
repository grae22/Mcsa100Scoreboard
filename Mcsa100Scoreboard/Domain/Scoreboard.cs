using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class Scoreboard : IScoreboard
  {
    public IClimberAnalysis[] AnalysedClimbersInRankOrder { get; }

    public Scoreboard(
      in IEnumerable<IClimber> climbers,
      in IEnumerable<IClimberAnalysis> priorAnalyses)
    {
      if (climbers == null)
      {
        AnalysedClimbersInRankOrder = new ClimberAnalysis[0];
        return;
      }

      var ranker = new ClimberRanker(climbers);
      var analysedClimbers = new List<ClimberAnalysis>();

      foreach (var pair in ranker.RankingByClimber.OrderBy(p => p.Value))
      {
        IClimber climber = pair.Key;
        int rank = pair.Value;

        int? priorRank =
          priorAnalyses
            ?.FirstOrDefault(a => a.Climber.Name == climber.Name)
            ?.Rank;

        analysedClimbers.Add(
          new ClimberAnalysis(
            climber,
            rank,
            priorRank,
            climbers));
      }

      var sortedAnalysedClimbers =
        analysedClimbers
          .OrderBy(c => c.Rank)
          .ThenByDescending(c => c.HighestGradeClimbed)
          .ThenByDescending(c => c.AverageGradeClimbed)
          .ThenByDescending(c => c.LowestGradeClimbed);

      AnalysedClimbersInRankOrder = sortedAnalysedClimbers.ToArray();
    }
  }
}
