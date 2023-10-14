namespace Strings;

public static class StringExtensions
{
    public static string? DuplicateCharacters(this string? str)
    {
        return string.Create(str.Length * 2, str, (span, orig) =>
        {
            for (int i = 0; i < orig.Length; i++)
            {
                span[i * 2] = span[i * 2 + 1] = orig[i];
            }
        });
    }
}