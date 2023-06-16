namespace Xorog.UniversalExtensions;

public static class MathTools
{
    /// <summary>
    /// Calculates the percentage of the given 2 values.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The percentage.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static int CalculatePercentage(double current, double max)
    {
        if (max == 0)
            throw new ArgumentException("Max cannot be zero.");

        double percentage = (current / max) * 100;
        return Convert.ToInt32(percentage);
    }
}
