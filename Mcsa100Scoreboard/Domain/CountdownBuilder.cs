using System;
using System.Text;

namespace Mcsa100Scoreboard.Domain
{
  internal class CountdownBuilder
  {
    public static string Build(in DateTime end, in DateTime now)
    {
      if (end < now)
      {
        return string.Empty;
      }

      TimeSpan timeDifference = end - now;

      int monthsRemaining = (int)timeDifference.TotalDays / 30;
      int weeksComponent = (int)timeDifference.TotalDays % 30 / 7;
      int daysComponent = (int)timeDifference.TotalDays % 30 % 7;

      var stringBuilder = new StringBuilder();

      if (monthsRemaining > 0)
      {
        stringBuilder.Append($"{monthsRemaining}");
        stringBuilder.Append(monthsRemaining > 1 ? " months, " : " month, ");
      }

      if (weeksComponent > 0)
      {
        stringBuilder.Append($"{weeksComponent}");
        stringBuilder.Append(weeksComponent > 1 ? " weeks, " : " week, ");
      }

      if (daysComponent > 0)
      {
        stringBuilder.Append($"{daysComponent}");
        stringBuilder.Append(daysComponent > 1 ? " days" : " day");
      }

      string result = stringBuilder.ToString().Trim();

      if (result.EndsWith(','))
      {
        result = result.Substring(0, result.Length - 1);
      }

      return result;
    }
  }
}
