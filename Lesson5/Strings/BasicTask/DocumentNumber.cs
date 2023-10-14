
using System;
using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;

namespace Strings;

public static class DocumentNumber
{
    private const string Template = "xxxx-yyy-xxxx-yyy-xyxy";
    private const string LetteredFormat = "yyy/yyy/y/y";

    public static bool IsValid(string documentNumber)
    {
        if (string.IsNullOrWhiteSpace(documentNumber)
            || documentNumber.Length != Template.Length)
        {
            return false;
        }

        for (int i = 0; i < documentNumber.Length; i++)
        {
            var inputCharacter = documentNumber[i];
            var templateCharacter = Template[i];

            var isValidCharacter = templateCharacter switch
            {
                'x' => char.IsDigit(inputCharacter),
                'y' => char.IsLetter(inputCharacter),
                _ => inputCharacter == templateCharacter
            };

            if (!isValidCharacter)
            {
                return false;
            }
        }
        return true;
    }

    public static string GetDigitBlocks(string documentNumber)
    {
        return string.Create(9, documentNumber, (chars, buffer) =>
        {
            for (int i = 0; i < 4; i++)
            {
                chars[i] = buffer[i];
            }
            chars[4] = ' ';
            for (int i = 5; i < 9; i++)
            {
                chars[i] = buffer[i + 4];
            }
        });
    }

    public static string ReplaceNumberBlocksWithAsterisks(string documentNumber)
    {
        return string.Create(documentNumber.Length, documentNumber, (chars, buffer) =>
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                chars[i] = (i >= 5 && i <= 7)
                           || (i >= 14 && i <= 16)
                    ? '*'
                    : documentNumber[i];
            }
        });
    }

    public static string LettersLowerCase(string documentNumber)
    {
        return string.Create(LetteredFormat.Length, documentNumber, (chars, buffer) =>
        {
            for (int i = 5, spanIndex = 0; i < buffer.Length;)
            {
                if (Template[i] != 'y')
                {
                    i++;
                    continue;
                }

                if (LetteredFormat[spanIndex] == 'y')
                {
                    chars[spanIndex] = char.ToLower(buffer[i]);
                    i++;
                }
                else
                {
                    chars[spanIndex] = LetteredFormat[spanIndex];
                }
                spanIndex++;
            }
        });
    }

    public static string LettersUpperCase(string documentNumber)
    {
        var sb = new StringBuilder("Letters:", 8 + LetteredFormat.Length);
        for (int i = 5, formatIndex = 0; i < documentNumber.Length;)
        {
            if (Template[i] != 'y')
            {
                i++;
                continue;
            }

            var nextChar = LetteredFormat[formatIndex] == 'y' ? char.ToUpper(documentNumber[i++]) : LetteredFormat[formatIndex];
            sb.Append(nextChar);
            formatIndex++;
        }
        return sb.ToString();
    }

    public static bool ContainsAbc(string documentNumber)
    {
        return documentNumber.Contains("abc", StringComparison.OrdinalIgnoreCase);
    }

    public static bool StartsWith555(string documentNumber)
    {
        return documentNumber.StartsWith("555");
    }

    public static bool EndsWith1a2b(string documentNumber)
    {
        return documentNumber.EndsWith("1a2b", StringComparison.OrdinalIgnoreCase);
    }

}