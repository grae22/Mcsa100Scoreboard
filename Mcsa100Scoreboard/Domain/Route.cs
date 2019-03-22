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

      int.TryParse(
        nameAndGrade.Substring(openingBraceIndex + 1, closingBraceIndex - openingBraceIndex - 1),
        out grade);
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
