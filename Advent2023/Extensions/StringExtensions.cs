namespace Advent2023;

public static class StringExtensions
{
    public static int[] GetNumbers(this string line)
    {
        return line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
    public static string[] SplitDefault(this string line, string chars)
    {
        return line.Split(chars.ToCharArray(), 
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
  
    public static int[] GetNumbersAfterSeparator(this string line)
    {
        return line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }
    
    public static int HasNumber(this string s, int starIndex)
    {
        if (char.IsDigit(s[starIndex])) return GetNumber(s, starIndex);
        if(starIndex>0 && char.IsDigit(s[starIndex-1])) return GetNumber(s, starIndex-1);
        if(starIndex<s.Length-1 && char.IsDigit(s[starIndex+1])) return GetNumber(s, starIndex+1);
        return 0;
    }
    
    public static int HasNumberBefore(this string s, int starIndex)
    {
        if (starIndex == 0) return 0;
        if( char.IsDigit(s[starIndex-1])) return GetNumber(s, starIndex-1);
        return 0;
    }
    public static int HasNumberAfter(this string s, int starIndex)
    {
        if (starIndex == s.Length-1) return 0;
        if(char.IsDigit(s[starIndex+1]))
        {
            return GetNumber(s, starIndex+1);
        }
    
        return 0;
    }
    
    public static int GetNumber(this string line, int index)
    {
        int startIndex = index;
        int endIndex= index;
        while (startIndex > 0 && char.IsDigit(line[startIndex-1]) ) startIndex--;
        while ( endIndex+1 < line.Length && char.IsDigit(line[endIndex+1])) endIndex++;
        return int.Parse(line.Substring(startIndex, endIndex - startIndex+1 ));
    }

    
    public static int CountDuplicates(this string str) =>
        (from c in str.ToLower()
            group c by c
            into grp
            where grp.Count() > 1
            select grp.Key).Count();
}