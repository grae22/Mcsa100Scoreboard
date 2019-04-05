using System;

namespace Mcsa100Scoreboard.Domain
{
  public class Route : IEquatable<Route>
  {
    public const int NoGrade = 0;

    public static Route Create(in string nameAndGrade)
    {
      ExtractNameAndGrade(
        nameAndGrade,
        out string name,
        out int grade,
        out string gradeFriendly);

      return new Route(name, grade, gradeFriendly);
    }

    private static void ExtractNameAndGrade(
      in string nameAndGrade,
      out string name,
      out int grade,
      out string gradeFriendly)
    {
      name = null;
      grade = NoGrade;
      gradeFriendly = "(?)";

      if (string.IsNullOrWhiteSpace(nameAndGrade))
      {
        return;
      }

      int openingBraceIndex = nameAndGrade.IndexOf('(');
      int closingBraceIndex = nameAndGrade.IndexOf(')');

      if (openingBraceIndex < 0 ||
          closingBraceIndex < 0 ||
          closingBraceIndex < openingBraceIndex)
      {
        name = nameAndGrade.Trim();
        return;
      }

      name = nameAndGrade
        .Remove(openingBraceIndex, closingBraceIndex - openingBraceIndex + 1)
        .Replace("  ", " ")
        .Trim();

      if (string.IsNullOrWhiteSpace(name))
      {
        name = nameAndGrade;
      }

      string gradeText = nameAndGrade.Substring(openingBraceIndex + 1, closingBraceIndex - openingBraceIndex - 1);

      bool gradeParsedOk = int.TryParse(gradeText, out grade);

      if (!gradeParsedOk)
      {
        int convertedGrade = GradeConverter.ConvertOldSaToSaSport(gradeText);

        grade = convertedGrade > 0 ? convertedGrade : grade;
      }

      gradeFriendly = $"({gradeText})";
    }

    public string Name { get; }
    public int Grade { get; }
    public string GradeFriendly { get; }
    public bool HasGrade => Grade != NoGrade;

    private Route(
      in string name,
      in int grade,
      in string gradeFriendly)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Grade = grade;
      GradeFriendly = gradeFriendly ?? throw new ArgumentNullException(nameof(gradeFriendly));
    }

    // IEquatable ---------------------------------------------------------------------------------

    public bool Equals(Route other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return string.Equals(
               Name,
               other.Name,
               StringComparison.OrdinalIgnoreCase) && Grade == other.Grade;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != this.GetType())
      {
        return false;
      }

      return Equals((Route)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0) * 397) ^ Grade;
      }
    }

    public static bool operator ==(Route left, Route right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Route left, Route right)
    {
      return !Equals(left, right);
    }
  }
}
