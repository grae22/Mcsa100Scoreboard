using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mcsa100Scoreboard.Domain
{
  public class ScoreboardNarrator
  {
    public string Narrative => _narrativeBuilder.ToString();

    private StringBuilder _narrativeBuilder = new StringBuilder();

    public ScoreboardNarrator(
      in IScoreboard scoreboardAtTime1,
      in IScoreboard scoreboardAtTime2)
    {
      GetClimbersWhoAddedRoutes(
        scoreboardAtTime1,
        scoreboardAtTime2,
        out Dictionary<string, IEnumerable<string>> newRouteNamesByClimber);

      CreateAddedRoutesNarrative(newRouteNamesByClimber);
    }

    private static void GetClimbersWhoAddedRoutes(
      in IScoreboard scoreboardAtTime1,
      in IScoreboard scoreboardAtTime2,
      out Dictionary<string, IEnumerable<string>> newRouteNamesByClimber)
    {
      newRouteNamesByClimber = new Dictionary<string, IEnumerable<string>>();

      IEnumerable<IClimber> climbersAtTime1 =
        scoreboardAtTime1
          .AnalysedClimbersInRankOrder
          .Select(c => c.Climber);

      IEnumerable<IClimber> climbersAtTime2 =
        scoreboardAtTime2
          .AnalysedClimbersInRankOrder
          .Select(c => c.Climber);

      foreach (var climber in climbersAtTime1)
      {
        IClimber climberAtTime2 =
          climbersAtTime2.FirstOrDefault(c =>
            c.Name.Equals(climber.Name, StringComparison.OrdinalIgnoreCase));

        if (climberAtTime2 == null ||
            climber.RouteCount <= climberAtTime2.RouteCount)
        {
          continue;
        }

        var newRoutes =
          climberAtTime2
            .Routes
            .Where(r => !climber.Routes.Contains(r))
            .Select(r => r.Name);

        newRouteNamesByClimber.Add(climber.Name, newRoutes);
      }
    }

    private void CreateAddedRoutesNarrative(in Dictionary<string, IEnumerable<string>> newRouteNamesByClimber)
    {
      foreach (var climberName in newRouteNamesByClimber.Keys)
      {
        IEnumerable<string> newRoutes = newRouteNamesByClimber[climberName];

        int numberOfNewRoutes = newRoutes.Count();

        if (numberOfNewRoutes == 1)
        {
          _narrativeBuilder.AppendLine($"{climberName} added '{newRoutes.First()}'.");
        }
        else
        {
          _narrativeBuilder.AppendLine($"{climberName} added '{newRoutes.First()}' and {numberOfNewRoutes - 1} other climbs.");
        }
      }
    }
  }
}
