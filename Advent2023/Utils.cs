namespace Advent2;

public static class Utils
{
    public static int[] GetNumbers(string line)
    {
        return line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
    public static int[] GetNumbersAfterSeparator(string line)
    {
        return line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
    
    public static int HasNumber(string s, int starIndex)
    {
        if (char.IsDigit(s[starIndex])) return GetNumber(s, starIndex);
        if(starIndex>0 && char.IsDigit(s[starIndex-1])) return GetNumber(s, starIndex-1);
        if(starIndex<s.Length-1 && char.IsDigit(s[starIndex+1])) return GetNumber(s, starIndex+1);
        return 0;
    }
    
    public static int HasNumberBefore(string s, int starIndex)
    {
        if (starIndex == 0) return 0;
        if( char.IsDigit(s[starIndex-1])) return GetNumber(s, starIndex-1);
        return 0;
    }
    public static int HasNumberAfter(string s, int starIndex)
    {
        if (starIndex == s.Length-1) return 0;
        if(char.IsDigit(s[starIndex+1]))
        {
            return GetNumber(s, starIndex+1);
        }
    
        return 0;
    }
    
    static int GetNumber(string line, int index)
    {
        int startIndex = index;
        int endIndex= index;
        while (startIndex > 0 && char.IsDigit(line[startIndex-1]) ) startIndex--;
        while ( endIndex+1 < line.Length && char.IsDigit(line[endIndex+1])) endIndex++;
        return int.Parse(line.Substring(startIndex, endIndex - startIndex+1 ));
    }
    static bool IsInLine(string s, int start, int end)
    {
        var sub = s.Substring(start, end - start );
        foreach (char c in sub)
        {
            if (IsSymbol(c)) return true;
        }
    
        return false;
    }
    static bool IsSymbol(char c) =>  c == '*';
    
    static int CountDuplicates(string str) =>
        (from c in str.ToLower()
            group c by c
            into grp
            where grp.Count() > 1
            select grp.Key).Count();
    
    public static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}