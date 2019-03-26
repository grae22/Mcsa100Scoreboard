using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain
{
  public class Climber : IClimber
  {
    public static Climber Create(
      in string name,
      in IEnumerable<Route> routes)
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
    public IEnumerable<Route> Routes { get; }
    public IEnumerable<Route> GradedRoutes => _gradedRoutes;
    public int RouteCount => Routes.Count();

    private readonly List<Route> _gradedRoutes = new List<Route>();

    private Climber(
      in string name,
      in IEnumerable<Route> routes)
    {
      ValidateName(name);
      ValidateRoutes(routes);

      Name = name;
      Routes = new List<Route>(routes);

      _gradedRoutes.AddRange(routes.Where(r => r.HasGrade));
    }

    private static void ValidateName(in string name)
    {
      if (!string.IsNullOrWhiteSpace(name))
      {
        return;
      }

      throw new ArgumentException("Name cannot be null or empty.", nameof(name));
    }

    private void ValidateRoutes(in IEnumerable<Route> routes)
    {
      if (routes != null)
      {
        return;
      }

      throw new ArgumentNullException(nameof(routes));
    }
  }
}
