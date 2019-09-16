using System;
using System.Collections.Generic;

using Mcsa100Scoreboard.Models;

using SQLitePCL;

namespace Mcsa100Scoreboard.Domain.Climbs
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

      if (input.values.Length == 0)
      {
        return;
      }

      var climberNameByClimberIndex = new Dictionary<int, string>();
      var routesByClimberIndex = new Dictionary<int, List<string>>();
      var overrideScoreboardPositionByClimberIndex = new Dictionary<int, int?>();

      // Get names of each climber (1st row).
      // [Climb number][Climber 1][Climber 2][Climber N]
      for (int column = 1; column < input.values[0].Length; column++)
      {
        int climberIndex = column - 1;
        string rawClimberName = input.values[0][column];

        ExtractClimberNameAndOverrideScoreboardPosition(
          rawClimberName,
          out string climberName,
          out int? overrideScoreboardPosition);

        climberNameByClimberIndex.Add(climberIndex, climberName);
        routesByClimberIndex.Add(climberIndex, new List<string>());
        overrideScoreboardPositionByClimberIndex.Add(climberIndex, overrideScoreboardPosition);
      }

      // Get each climber's routes climbed.
      for (int row = 1; row < input.values.Length; row++)
      {
        for (int column = 1; column < input.values[row].Length; column++)
        {
          int climberIndex = column - 1;

          if (climberIndex >= routesByClimberIndex.Count)
          {
            break;
          }

          string routeName = input.values[row][column];

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
          routes,
          overrideScoreboardPositionByClimberIndex[key]);

        if (climber == null)
        {
          continue;
        }

        climbers.Add(climber);
      }
    }

    private static void ExtractClimberNameAndOverrideScoreboardPosition(
      in string rawName,
      out string name,
      out int? overrideScoreboardPosition)
    {
      overrideScoreboardPosition = null;

      int openingBracket = rawName.IndexOf('[');
      int closingBracket = rawName.IndexOf(']');

      if (openingBracket < 0 ||
          closingBracket < 0 ||
          openingBracket > closingBracket)
      {
        name = rawName;
        return;
      }

      name = rawName
        .Substring(0, openingBracket)
        .Trim();

      string overrideScoreboardPositionText =
        rawName
          .Substring(openingBracket + 1, closingBracket - openingBracket - 1)
          .Trim();

      if (int.TryParse(overrideScoreboardPositionText, out int tempOverrideScoreboardPosition))
      {
        overrideScoreboardPosition = tempOverrideScoreboardPosition;
      }
    }
  }
}
