namespace Xorog.UniversalExtensions;

public static class StringTools
{
    /// <summary>
    /// Generate an ASCII Progressbar
    /// </summary>
    /// <param name="current">The current progress</param>
    /// <param name="max">The maximum progress</param>
    /// <param name="charlength">How long the ASCII Progressbar should be (default: <code>44</code>)</param>
    /// <param name="fill">What character the filled part should be (default: <code>█</code>)</param>
    /// <param name="empty">What character the not-filled part should be (default: <code>∙</code>)</param>
    /// <param name="start">What character the start-part should be (default: <code>[</code>)</param>
    /// <param name="end">What character the end-part part should be (default: <code>]</code>)</param>
    /// <returns>A progressbar</returns>
    public static string GenerateASCIIProgressbar(double current, double max, int charlength = 44, char fill = '█', char empty = '∙', char start = '[', char end = ']')
    {
        long first = (long)Math.Round((current / max) * charlength, 0);

        long second = charlength - first;

        string mediadisplay = start.ToString();

        for (long i = 0; i < first; i++)
            mediadisplay += fill;

        for (long i = 0; i < second; i++)
            mediadisplay += empty;

        mediadisplay += end;

        return mediadisplay;
    }
}
