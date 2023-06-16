namespace Xorog.UniversalExtensions;

public static class ListExtensions
{
    /// <summary>
    /// Select a random item from a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>The randomly selected item</returns>
    /// <exception cref="ArgumentNullException">The list is null</exception>
    /// <exception cref="ArgumentException">The list is empty</exception>
    public static T SelectRandom<T>(this IEnumerable<T>? obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException();
        }

        if (!obj.Any())
        {
            throw new ArgumentException("The sequence is empty.");
        }


        int rng = new Random().Next(0, obj.Count());
        return obj.ElementAt(rng) ?? throw new ArgumentNullException();
    }



    /// <summary>
    /// Check whether a list contains elements and is not null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>Whether the list contains elements and is not null</returns>
    public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T>? obj)
        => obj is not null && obj.Any();
}
