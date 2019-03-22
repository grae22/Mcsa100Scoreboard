using System;
using System.Collections.Generic;

using Mcsa100Scoreboard.Models;

namespace Mcsa100Scoreboard.Domain
{
  public class Scoreboard
  {
    public IReadOnlyDictionary<Climber, int> RankingByClimber => _ranker.RankingByClimber;

    private ClimberRanker _ranker;

    public Scoreboard(in InputModel input)
    {
      if (input == null)
      {
        throw new ArgumentNullException(nameof(input));
      }

      CreateClimberRankingFromInput(input);
    }

    private void CreateClimberRankingFromInput(in InputModel input)
    {
      if (input.Values == null)
      {
        throw new ArgumentNullException("Input 'values' cannot be null.", nameof(input));
      }

      if (input.Values.Length == 0)
      {
        return;
      }

      var climberNameByClimberIndex = new Dictionary<int, string>();
      var routesByClimberIndex = new Dictionary<int, List<string>>();

      for (int column = 1; column < input.Values[0].Length; column++)
      {
        int climberIndex = column - 1;

        climberNameByClimberIndex.Add(climberIndex, input.Values[0][column]);
        routesByClimberIndex.Add(climberIndex, new List<string>());
      }

      for (int row = 1; row < input.Values.Length; row++)
      {
        for (int column = 1; column < input.Values[row].Length; column++)
        {
          int climberIndex = column - 1;

          if (climberIndex >= routesByClimberIndex.Count)
          {
            break;
          }

          string routeName = input.Values[row][column];

          if (string.IsNullOrWhiteSpace(routeName) ||
              routeName.StartsWith("#"))
          {
            continue;
          }

          routesByClimberIndex[climberIndex].Add(routeName);
        }
      }

      var climbers = new List<Climber>();

      foreach (int key in climberNameByClimberIndex.Keys)
      {
        var routes = new List<Route>();

        foreach (string routeText in routesByClimberIndex[key])
        {
          Route route = Route.Create(routeText);

          if (route == null)
          {
            continue;
          }

          routes.Add(route);
        }

        Climber climber = Climber.Create(
          climberNameByClimberIndex[key],
          routes);

        if (climber == null)
        {
          continue;
        }

        climbers.Add(climber);
      }

      _ranker = new ClimberRanker(climbers);
    }
  }
}
