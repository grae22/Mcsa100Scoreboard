using System;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class Scoreable
  {
    private static readonly char[] ValidTypes = { 'S', 'C', 'P' };

    public static Scoreable Create(in string rawName)
    {
      char? scoreableTypeId = GetScoreableType(rawName);

      if (scoreableTypeId == null)
      {
        return null;
      }

      string name = GetScoreableNameFromPrefixedRawName(rawName);

      if (name == null)
      {
        return null;
      }

      return new Scoreable(
        scoreableTypeId.Value,
        name);
    }

    private static char? GetScoreableType(in string rawName)
    {
      if (rawName == null ||
          rawName.Length < 3)
      {
        return null;
      }

      string prefixWithTypeId =
        rawName
          .Substring(0, 3)
          .ToUpper();

      char id = prefixWithTypeId[1];

      if (!IsValidScoreableTypeId(id))
      {
        return null;
      }

      return id;
    }

    private static bool IsValidScoreableTypeId(in char id)
    {
      return ValidTypes.Contains(id);
    }

    private static string GetScoreableNameFromPrefixedRawName(in string rawName)
    {
      if (rawName.Length < 3)
      {
        return null;
      }

      string name = rawName.Remove(0, 3);

      name = name.Trim();

      if (!name.Any())
      {
        return null;
      }

      return name;
    }

    public char TypeId { get; }
    public string Name { get; }
    public string FullName => $"({TypeId}) {Name}";

    private Scoreable(
      in char typeId,
      in string name)
    {
      TypeId = typeId;
      Name = name ?? throw new ArgumentNullException(nameof(typeId));
    }
  }
}
