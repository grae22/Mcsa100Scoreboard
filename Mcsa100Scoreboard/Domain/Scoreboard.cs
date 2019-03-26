﻿using System;
using System.Collections.Generic;
using System.Linq;

using Mcsa100Scoreboard.Models;

namespace Mcsa100Scoreboard.Domain
{
  public class Scoreboard
  {
    public ClimberAnalysis[] AnalysedClimbersInRankOrder { get; }

    public Scoreboard(in InputModel input)
    {
      if (input == null)
      {
        throw new ArgumentNullException(nameof(input));
      }

      var parsedInput = new InputParser(input);
      var ranker = new ClimberRanker(parsedInput.Climbers);
      var analysedClimbers = new List<ClimberAnalysis>();

      foreach (var pair in ranker.RankingByClimber.OrderBy(p => p.Value))
      {
        IClimber climber = pair.Key;
        int rank = pair.Value;
        var analysis = new ClimberAnalysis(climber, rank, parsedInput.Climbers);
        analysedClimbers.Add(analysis);
      }

      var sortedAnalysedClimbers =
        analysedClimbers
          .OrderBy(c => c.Rank)
          .ThenByDescending(c => c.HighestGradeClimbed)
          .ThenByDescending(c => c.AverageGradeClimbed)
          .ThenByDescending(c => c.LowestGradeClimbed);

      AnalysedClimbersInRankOrder = sortedAnalysedClimbers.ToArray();
    }
  }
}
