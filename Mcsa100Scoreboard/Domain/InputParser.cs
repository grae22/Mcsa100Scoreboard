using System;
using System.Collections.Generic;

using Mcsa100Scoreboard.Models;

namespace Mcsa100Scoreboard.Domain
{
  internal class InputParser
  {
    public IEnumerable<Climber> Climbers { get; private set; }

    public InputParser(in InputModel input)
    {
      if (input == null)
      {
        throw new ArgumentNullException(nameof(input));
      }

      ParseInput(input);
    }

    private void ParseInput(in InputModel input)
    {
      var climbers = new List<Climber>();

      Climbers = climbers;

      if (input.Values.Length == 0)
      {
        return;
      }

      var climberNameByClimberIndex = new Dictionary<int, string>();
      var routesByClimberIndex = new Dictionary<int, List<string>>();

      // Get names of each climber (1st row).
      // [Climb number][Climber 1][Climber 2][Climber N]
      for (int column = 1; column < input.Values[0].Length; column++)
      {
        int climberIndex = column - 1;

        climberNameByClimberIndex.Add(climberIndex, input.Values[0][column]);
        routesByClimberIndex.Add(climberIndex, new List<string>());
      }

      // Get each climber's routes climbed.
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

      // Create a climber object for each climber.
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
    }
  }
}
