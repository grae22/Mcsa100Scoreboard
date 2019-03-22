using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain
{
  public interface IClimber
  {
    string Name { get; }
    IEnumerable<Route> Routes { get; }
    int RouteCount { get; }
  }
}
