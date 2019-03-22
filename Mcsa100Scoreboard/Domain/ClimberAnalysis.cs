using System;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class ClimberAnalysis
  {
    public IClimber Climber { get; }
    public int Rank { get; }
    public int? HighestGradeClimbed { get; }
    public int? LowestGradeClimbed { get; }
    public int? AverageGradeClimbed { get; }

    public ClimberAnalysis(IClimber climber, int rank)
    {
      if (climber == null)
      {
        throw new ArgumentNullException(nameof(climber));
      }

      if (rank < 1)
      {
        throw new ArgumentException($"Invalid rank value '{rank}' for climber '{climber.Name}'.");
      }

      Climber = climber;
      Rank = rank;

      if (climber.Routes.Any(r => r.HasGrade))
      {
        HighestGradeClimbed =
          climber
            .Routes
            .Where(r => r.HasGrade)
            .Max(r => r.Grade);

        LowestGradeClimbed =
          climber
            .Routes
            .Where(r => r.HasGrade)
            .Min(r => r.Grade);

        AverageGradeClimbed =
          (int?)climber
            .Routes
            .Where(r => r.HasGrade)
            .Average(r => (decimal?)r.Grade);
      }
    }
  }
}
