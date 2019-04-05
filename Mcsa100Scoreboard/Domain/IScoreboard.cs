namespace Mcsa100Scoreboard.Domain
{
  public interface IScoreboard
  {
    IClimberAnalysis[] AnalysedClimbersInRankOrder { get; }
  }
}