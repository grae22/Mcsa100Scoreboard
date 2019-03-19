﻿using System;
using System.Collections.Generic;
using System.Linq;

using Mcsa100Scoreboard.Models;

namespace Mcsa100Scoreboard.Domain
{
  public class Scoreboard
  {
    public IEnumerable<Climber> RankedClimbers
    {
      get
      {
        return _climbers.OrderByDescending(c => c.RouteCount);
      }
    }

    private readonly List<Climber> _climbers = new List<Climber>();

    public Scoreboard(in InputModel input)
    {
      if (input == null)
      {
        throw new ArgumentNullException(nameof(input));
      }

      CreateClimbersFromInput(input);
    }

    private void CreateClimbersFromInput(in InputModel input)
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

      foreach (int key in climberNameByClimberIndex.Keys)
      {
        Climber climber = Climber.Create(
          climberNameByClimberIndex[key],
          routesByClimberIndex[key]);

        if (climber == null)
        {
          continue;
        }

        _climbers.Add(climber);
      }
    }
  }
}
