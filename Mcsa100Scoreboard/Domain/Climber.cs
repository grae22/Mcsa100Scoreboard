using System;
using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain
{
  internal class Climber
  {
    public static Climber Create(
      in string name,
      in IEnumerable<string> routes)
    {
      try
      {
        return new Climber(name, routes);
      }
      catch (ArgumentException)
      {
        return null;
      }
    }

    public string Name { get; }
    public IEnumerable<string> Routes { get; }

    private Climber(
      in string name,
      in IEnumerable<string> routes)
    {
      ValidateName(name);

      Name = name;
      Routes = new List<string>(routes);
    }

    private static void ValidateName(in string name)
    {
      if (!string.IsNullOrWhiteSpace(name))
      {
        return;
      }

      throw new ArgumentException("Name cannot be null or empty.", nameof(name));
    }
  }
}
