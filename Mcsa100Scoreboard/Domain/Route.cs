namespace Mcsa100Scoreboard.Domain
{
  public class Route
  {
    public const int NoGrade = 0;

    public static Route Create(in string nameAndGrade)
    {
      ExtractNameAndGrade(
        nameAndGrade,
        out string name,
        out int grade);

      return new Route(name, grade);
    }

    private static void ExtractNameAndGrade(
      in string nameAndGrade,
      out string name,
      out int grade)
    {
      name = null;
      grade = NoGrade;

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
        .Substring(0, openingBraceIndex)
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
    }

    public string Name { get; }
    public int Grade { get; }
    public bool HasGrade => Grade != NoGrade;

    public Route(in string name, in int grade)
    {
      Name = name;
      Grade = grade;
    }
  }
}
