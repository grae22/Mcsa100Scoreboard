using System;
using System.Collections.Generic;

using Mcsa100Scoreboard.Models;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  internal class InputParser
  {
    public IEnumerable<Competitor> Competitors { get; private set; }

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
      var competitors = new List<Competitor>();

      Competitors = competitors;

      if (input.values.Length == 0)
      {
        return;
      }

      for (int column = 0; column < input.values[1].Length; column++)
      {
        string competitorName = input.values[1][column];

        if (string.IsNullOrWhiteSpace(competitorName))
        {
          continue;
        }

        var competitor = new Competitor(competitorName);

        competitors.Add(competitor);

        for (int row = 2; row < input.values.Length; row++)
        {
          if (column >= input.values[row].Length)
          {
            break;
          }

          string scoreableText = input.values[row][column];

          competitor.AddScoreable(scoreableText);
        }
      }
    }
  }
}
