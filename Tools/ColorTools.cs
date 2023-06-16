namespace Xorog.UniversalExtensions;

public static class ColorTools
{
    /// <summary>
    /// Get closest Color to given Color
    /// </summary>
    /// <param name="colorArray"></param>
    /// <param name="baseColor"></param>
    /// <returns></returns>
    public static Color GetClosestColor(List<Color> colorArray, Color baseColor)
    {
        var colors = colorArray.Select(x => new { Value = x, Diff = Internal.GetDiff(x, baseColor) }).ToList();
        var min = colors.Min(x => x.Diff);
        return colors.Find(x => x.Diff == min).Value;
    }

    /// <summary>
    /// Convert Hex to Color
    /// </summary>
    /// <returns>The converted color</returns>
    public static Color ToColor(this string str)
    {
        return ColorTranslator.FromHtml(str);
    }

    /// <summary>
    /// Convert RGB Value to Hex
    /// </summary>
    /// <param name="R">Red</param>
    /// <param name="G">Green</param>
    /// <param name="B">Blue</param>
    /// <returns>A string that represents the color in hex (e.g. 255, 0, 0 -> #FF0000)</returns>
    public static string ToHex(int R, int G, int B)
    {
        return "#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
    }
}
