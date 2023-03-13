using System.Text.Json;

using RateGames.Common.Extensions;

namespace RateGames.Common.Utils;
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToSnakeCase();
}
