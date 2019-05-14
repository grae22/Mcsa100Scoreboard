using System;
using System.Linq;

namespace Mcsa100Scoreboard.Domain.Hikes
{
  public class Scoreable
  {
    public const char CaveTypeId = 'C';
    public const char PassTypeId = 'P';
    public const char SummitTypeId = 'S';
    public const char UnknownTypeId = '?';

    private static readonly char[] ValidTypes = { CaveTypeId, PassTypeId, SummitTypeId };

    public static Scoreable Create(in string rawName)
    {
      if (rawName == null)
      {
        return null;
      }

      char? scoreableTypeId = GetScoreableType(rawName);

      if (scoreableTypeId == null)
      {
        return null;
      }

      string name = GetScoreableNameFromPrefixedRawName(rawName);

      if (string.IsNullOrWhiteSpace(name))
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
        return UnknownTypeId;
      }

      string prefixWithTypeId =
        rawName
          .Substring(0, 3)
          .ToUpper();

      char id = prefixWithTypeId[1];

      if (!IsValidScoreableTypeId(id))
      {
        return UnknownTypeId;
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
        return rawName;
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
