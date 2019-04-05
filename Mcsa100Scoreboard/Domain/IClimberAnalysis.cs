namespace Mcsa100Scoreboard.Domain
{
  public interface IClimberAnalysis
  {
    IClimber Climber { get; }
    int Rank { get; }
    int? HighestGradeClimbed { get; }
    int? LowestGradeClimbed { get; }
    int? AverageGradeClimbed { get; }
    bool HasHighestGradedClimb { get; }
    bool HasHighestAverageGrade { get; }
  }
}
