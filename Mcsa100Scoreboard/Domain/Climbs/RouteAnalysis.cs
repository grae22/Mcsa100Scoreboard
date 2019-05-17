using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Climbs
{
  public class RouteAnalysis
  {
    public IEnumerable<Route> PopularRoutes
    {
      get
      {
        return _routes
          .OrderByDescending(r => r.AscentCount)
          .ThenBy(r => r.Name)
          .Take(_maxPopularRoutes);
      }
    }

    private readonly List<Route> _routes = new List<Route>();
    private readonly int _maxPopularRoutes;

    public RouteAnalysis(
      in IEnumerable<IClimber> climbers,
      in int maxPopularRoutes)
    {
      if (climbers == null)
      {
        throw new ArgumentNullException(nameof(climbers));
      }

      if (maxPopularRoutes < 1)
      {
        throw new ArgumentException(
          $"Invalid value '{maxPopularRoutes}' specified.",
          nameof(maxPopularRoutes));
      }

      _maxPopularRoutes = maxPopularRoutes;

      UpdatePopularRoutes(climbers);
    }

    private void UpdatePopularRoutes(in IEnumerable<IClimber> climbers)
    {
      AddRouteNames(climbers);
      AddRouteCounts(climbers);
    }

    private void AddRouteNames(in IEnumerable<IClimber> climbers)
    {
      foreach (var climber in climbers)
      {
        foreach (var route in climber.Routes)
        {
          bool alreadExists =
            _routes
              .Any(r =>
                r.Name.Equals(
                  route.Name,
                  StringComparison.OrdinalIgnoreCase));

          if (alreadExists)
          {
            continue;
          }

          _routes.Add(route.Clone());
        }
      }
    }

    private void AddRouteCounts(in IEnumerable<IClimber> climbers)
    {
      foreach (var climber in climbers)
      {
        foreach (var route in climber.Routes)
        {
          try
          {
            var uniqueRoute =
              _routes
                .First(r =>
                  r.Name.Equals(
                    route.Name,
                    StringComparison.OrdinalIgnoreCase));

            uniqueRoute.LogAscent();
          }
          catch (InvalidOperationException)
          {
            // We're just going to ignore this.
          }
        }
      }
    }
  }
}
