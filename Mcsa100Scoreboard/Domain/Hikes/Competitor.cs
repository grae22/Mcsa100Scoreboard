using System;
using System.Collections.Generic;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class Competitor
  {
    public string Name { get; }
    public IEnumerable<Scoreable> Scoreables => _scoreables;

    private List<Scoreable> _scoreables = new List<Scoreable>();

    public Competitor(in string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.");
      }

      Name = name;
    }

    public void AddScoreable(in string text)
    {
      Scoreable newScoreable = Scoreable.Create(text);

      if (newScoreable == null)
      {
        return;
      }

      _scoreables.Add(newScoreable);
    }
  }
}
