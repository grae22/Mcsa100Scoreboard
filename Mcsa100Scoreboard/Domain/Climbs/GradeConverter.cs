using System;
using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain.Climbs
{
  internal static class GradeConverter
  {
    // Grades from Peak High Mountaineering website.
    private static readonly Dictionary<string, int> _saSportByOldSaGrades = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
      { "D", 9 },
      { "E1", 10 },
      { "E2", 11 },
      { "E3", 12 },
      { "F1", 13 },
      { "F2", 15 },
      { "F3", 18 },
      { "G1", 19 },
      { "G2", 20 },
      { "G3", 21 },
      { "H1", 22 },
      { "H2", 23 },
      { "H3", 24 },
      { "I1", 25 },
      { "I2", 26 },
      { "I3", 27 },
      { "J1", 28 },
      { "J2", 29 },
      { "J3", 30 }
    };

    public static int ConvertOldSaToSaSport(in string oldSaGrade)
    {
      if (!_saSportByOldSaGrades.ContainsKey(oldSaGrade))
      {
        return -1;
      }

      return _saSportByOldSaGrades[oldSaGrade];
    }

    public static bool IsValidGrade(in string gradeText)
    {
      if (int.TryParse(gradeText, out int grade))
      {
        return true;
      }

      return _saSportByOldSaGrades.ContainsKey(gradeText);
    }
  }
}
