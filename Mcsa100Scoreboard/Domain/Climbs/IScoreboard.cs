namespace Mcsa100Scoreboard.Domain.Climbs
{
  public interface IScoreboard
  {
    IClimberAnalysis[] AnalysedClimbersInRankOrder { get; }
  }
}