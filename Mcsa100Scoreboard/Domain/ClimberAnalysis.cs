using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class ClimberAnalysis : IClimberAnalysis
  {
    public IClimber Climber { get; }
    public int Rank { get; }
    public int? HighestGradeClimbed { get; private set; }
    public int? LowestGradeClimbed { get; private set; }
    public int? AverageGradeClimbed { get; private set; }
    public bool HasHighestGradedClimb { get; private set; }
    public bool HasHighestAverageGrade { get; private set; }

    public ClimberAnalysis(
      in IClimber climber,
      in int rank,
      in IEnumerable<IClimber> allClimbers)
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

      AnalyseGradedClimbs(allClimbers);
    }

    private void AnalyseGradedClimbs(in IEnumerable<IClimber> allClimbers)
    {
      if (!Climber.GradedRoutes.Any())
      {
        return;
      }

      HighestGradeClimbed =
        Climber
          .GradedRoutes
          .Max(r => r.Grade);

      LowestGradeClimbed =
        Climber
          .GradedRoutes
          .Min(r => r.Grade);

      AverageGradeClimbed =
        (int?)Climber
          .GradedRoutes
          .Average(r => (decimal?)r.Grade);

      HasHighestGradedClimb = DetermineIfClimberHasHighestGradedClimb(Climber, allClimbers);
      HasHighestAverageGrade = DetermineIfClimberHasHighestAverageGrade(Climber, allClimbers);
    }

    private static bool DetermineIfClimberHasHighestGradedClimb(
      in IClimber climber,
      in IEnumerable<IClimber> allClimbers)
    {
      if (!climber.GradedRoutes.Any())
      {
        return false;
      }

      int highestGrade =
        allClimbers
          .Where(c => c.GradedRoutes.Any())
          .Max(c => c.GradedRoutes.Max(r => r.Grade));

      return highestGrade == climber.GradedRoutes.Max(r => r.Grade);
    }

    private static bool DetermineIfClimberHasHighestAverageGrade(
      in IClimber climber,
      in IEnumerable<IClimber> allClimbers)
    {
      if (!climber.GradedRoutes.Any())
      {
        return false;
      }

      int highestAverageGrade = (int)
        allClimbers
          .Where(c => c.GradedRoutes.Any())
          .Max(c => c.GradedRoutes.Average(r => r.Grade));

      return highestAverageGrade == (int)climber.GradedRoutes.Average(r => r.Grade);
    }
  }
}
