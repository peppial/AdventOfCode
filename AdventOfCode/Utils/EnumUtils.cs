namespace AdventOfCode.Utils;
public class EnumUtils
{
    public static T IncrementEnum<T>(T currentValue) where T : Enum
    {
        T[] values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        int currentIndex = Array.IndexOf(values, currentValue);
    
        if (currentIndex == values.Length - 1)
        {
            return values[0];
        }
    
        return values[currentIndex + 1];
    }
    public static T DecrementEnum<T>(T currentValue) where T : Enum
    {
        T[] values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        int currentIndex = Array.IndexOf(values, currentValue);
    
        // If it's the first enum value, return the last value
        if (currentIndex == 0)
        {
            return values[^1];
        }
    
        // Otherwise, return the previous enum value
        return values[currentIndex - 1];
    }
}