using System.Text;

namespace RateGames.Common.Extensions;
public static class StringExtensions
{
    /// <summary>
    /// Converts string to snake case style.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="skipQuotedText">Use if quoted text does not need to be snake cased.</param>
    /// <returns></returns>
    public static string ToSnakeCase(this string text, bool skipQuotedText = false)
    {
        if (text.Length < 2)
        {
            return text;
        }
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToLowerInvariant(text[0]));
        var underQuotes = false;
        for (var i = 1; i < text.Length; i++)
        {
            var character = text[i];

            if (skipQuotedText && character == '"')
            {
                underQuotes = !underQuotes;
            }

            var prevCharacter = text[i - 1];
            if (!underQuotes && char.IsUpper(character) && char.IsLetterOrDigit(prevCharacter))
            {
                stringBuilder.Append('_');
                stringBuilder.Append(char.ToLowerInvariant(character));
                continue;
            }
            if (!underQuotes && char.IsUpper(character))
            {
                stringBuilder.Append(char.ToLowerInvariant(character));
                continue;
            }
            stringBuilder.Append(character);
        }
        return stringBuilder.ToString();
    }
}