using System;
using System.Collections.Generic;
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
    public bool HasHighestGradedClimb { get; }
    public bool HasHighestAverageGrade { get; }

    public ClimberAnalysis(
      in IClimber climber,
      in int rank,
      in IEnumerable<IClimber> climbers)
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

      if (climber.GradedRoutes.Any())
      {
        HighestGradeClimbed =
          climber
            .GradedRoutes
            .Max(r => r.Grade);

        LowestGradeClimbed =
          climber
            .GradedRoutes
            .Min(r => r.Grade);

        AverageGradeClimbed =
          (int?)climber
            .GradedRoutes
            .Average(r => (decimal?)r.Grade);

        HasHighestGradedClimb = DetermineIfClimberHasHighestGradedClimb(Climber, climbers);
        HasHighestAverageGrade = DetermineIfClimberHasHighestAverageGrade(Climber, climbers);
      }
    }

    private static bool DetermineIfClimberHasHighestGradedClimb(
      in IClimber climber,
      in IEnumerable<IClimber> climbers)
    {
      if (!climber.GradedRoutes.Any())
      {
        return false;
      }

      int highestGrade =
        climbers
          .Where(c => c.GradedRoutes.Any())
          .Max(c => c.GradedRoutes.Max(r => r.Grade));

      return highestGrade == climber.GradedRoutes.Max(r => r.Grade);
    }

    private static bool DetermineIfClimberHasHighestAverageGrade(
      in IClimber climber,
      in IEnumerable<IClimber> climbers)
    {
      if (!climber.GradedRoutes.Any())
      {
        return false;
      }

      int highestAverageGrade = (int)
        climbers
          .Where(c => c.GradedRoutes.Any())
          .Max(c => c.GradedRoutes.Average(r => r.Grade));

      return highestAverageGrade == (int)climber.GradedRoutes.Average(r => r.Grade);
    }
  }
}
