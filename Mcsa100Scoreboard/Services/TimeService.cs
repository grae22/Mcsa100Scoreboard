using System;

namespace Mcsa100Scoreboard.Services
{
  internal class TimeService : ITimeService
  {
    public DateTime Now => DateTime.Now;
  }
}
