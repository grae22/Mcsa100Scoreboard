using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mcsa100Scoreboard.Domain
{
  public class ScoreboardNarrator
  {
    public string Narrative => _narrativeBuilder.ToString();

    private readonly StringBuilder _narrativeBuilder = new StringBuilder();

    public ScoreboardNarrator(
      in IScoreboard scoreboardAtTime1,
      in IScoreboard scoreboardAtTime2)
    {
      GetClimbersWhoAddedRoutes(
        scoreboardAtTime1,
        scoreboardAtTime2,
        out Dictionary<string, IEnumerable<string>> newRouteNamesByClimber);

      GetNewClimbers(
        scoreboardAtTime1,
        scoreboardAtTime2,
        out Dictionary<string, int> routeCountsByNewClimber);

      CreateAddedRoutesNarrative(newRouteNamesByClimber);
      CreateNewClimbersNarrative(routeCountsByNewClimber);
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

      foreach (var climberAtTime1 in climbersAtTime1)
      {
        IClimber climberAtTime2 =
          climbersAtTime2.FirstOrDefault(c =>
            c.Name.Equals(climberAtTime1.Name, StringComparison.OrdinalIgnoreCase));

        if (climberAtTime2 == null ||
            climberAtTime2.RouteCount <= climberAtTime1.RouteCount)
        {
          continue;
        }

        var newRoutes =
          climberAtTime2
            .Routes
            .Where(r => !climberAtTime1.Routes.Contains(r))
            .Select(r => r.Name);

        newRouteNamesByClimber.Add(climberAtTime1.Name, newRoutes);
      }
    }

    private static void GetNewClimbers(
      in IScoreboard scoreboardAtTime1,
      in IScoreboard scoreboardAtTime2,
      out Dictionary<string, int> routeCountsByClimber)
    {
      routeCountsByClimber = new Dictionary<string, int>();

      IEnumerable<IClimber> climbersAtTime1 =
        scoreboardAtTime1
          .AnalysedClimbersInRankOrder
          .Select(c => c.Climber);

      IEnumerable<IClimber> climbersAtTime2 =
        scoreboardAtTime2
          .AnalysedClimbersInRankOrder
          .Select(c => c.Climber);

      IEnumerable<IClimber> newClimbers =
        climbersAtTime2
          .Where(c => !climbersAtTime1.Any(x =>
            x.Name.Equals(c.Name, StringComparison.OrdinalIgnoreCase)));

      foreach (var climber in newClimbers)
      {
        int routeCount =
          climbersAtTime2
            .First(c => c.Name.Equals(climber.Name, StringComparison.OrdinalIgnoreCase))
            .RouteCount;

        routeCountsByClimber.Add(climber.Name, routeCount);
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
          _narrativeBuilder.AppendLine($"{climberName} added '{newRoutes.First()}' and {numberOfNewRoutes - 1} other climb(s).");
        }
      }
    }

    private void CreateNewClimbersNarrative(in Dictionary<string, int> routeCountsByNewClimber)
    {
      foreach (var climberName in routeCountsByNewClimber.Keys)
      {
        int routeCount = routeCountsByNewClimber[climberName];

        if (routeCount == 0)
        {
          _narrativeBuilder.AppendLine($"{climberName} joined.");
        }
        else
        {
          _narrativeBuilder.AppendLine($"{climberName} joined and added {routeCount} climb(s).");
        }
      }
    }
  }
}
