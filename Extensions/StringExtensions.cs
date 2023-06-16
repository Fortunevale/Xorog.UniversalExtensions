namespace Xorog.UniversalExtensions;

public static class StringExtensions
{
    /// <summary>
    /// Extensions for string.IsNullOrWhiteSpace
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Whether the string is null, empty or only contains whitespaces</returns>
    public static bool IsNullOrWhiteSpace(this string str)
        => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// Extensions for string.IsNullOrEmpty
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Whether the string is null or empty</returns>
    public static bool IsNullOrEmpty(this string str)
        => string.IsNullOrEmpty(str);

    /// <summary>
    /// Check if a string contains only digits
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsDigitsOnly(this string str)
    {
        foreach (char c in str)
        {
            if (c is < '0' or > '9')
                return false;
        }

        return true;
    }

    /// <summary>
    /// Get all digits from a string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetAllDigits(this string str) =>
        new(str.Where(Char.IsDigit).ToArray());

    /// <summary>
    /// Get country flag emoji based on Iso Country Code
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    public static string IsoCountryCodeToFlagEmoji(this string country)
    {
        return string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
    }

    /// <summary>
    /// Shorten a string to the given length
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        return value.Length <= maxLength ? value : value[..maxLength];
    }

    /// <summary>
    /// Shorten a string to the given length and add ".." at the end
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string TruncateWithIndication(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value.Length <= maxLength ? value : $"{value[..(maxLength - 2)]}..";
    }

    /// <summary>
    /// Remove unsupported characters from string to generate a valid filename
    /// </summary>
    /// <param name="name">The string with potentionally unwanted characters</param>
    /// <param name="replace_char">The character the unwanted characters get replaced with (default: <code>_</code>)</param>
    /// <returns>A valid filename</returns>
    public static string MakeValidFileName(this string name, char replace_char = '_')
    {
        string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

        return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, replace_char.ToString()).Replace('&', replace_char);
    }

    /// <summary>
    /// Changes the first letter to upper.
    /// </summary>
    /// <param name="str">The string to modify.</param>
    /// <returns>The string with the first letter changed to upper.</returns>
    public static string FirstLetterToUpper(this string str)
    {
        return $"{str.First().ToString().ToUpper()}{str.Remove(0, 1)}";
    }
}
