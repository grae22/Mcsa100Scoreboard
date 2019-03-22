using System;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class ClimberAnalysis
  {
    public int? HighestGradeClimbed { get; }
    public int? LowestGradeClimbed { get; }
    public int? AverageGradeClimbed { get; }

    public ClimberAnalysis(IClimber climber)
    {
      if (climber == null)
      {
        throw new ArgumentNullException(nameof(climber));
      }

      if (climber.Routes.Any())
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
