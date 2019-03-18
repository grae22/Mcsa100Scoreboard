using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain
{
  internal class Climber
  {
    public static Climber Create(
      in string name,
      in IEnumerable<string> routes)
    {
      return new Climber(name, routes);
    }

    public string Name { get; }
    public IEnumerable<string> Routes { get; }

    private Climber(
      in string name,
      in IEnumerable<string> routes)
    {
      Name = name;
      Routes = new List<string>(routes);
    }
  }
}
