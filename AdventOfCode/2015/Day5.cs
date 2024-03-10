namespace AdventOfCode._2015;

public class Day5(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int count = 0;
        string vowels = "aeiou";
        string[] notAllowed = ["ab", "cd", "pq", "xy"];
        foreach (string line in lines)
        {
            int vowelsCount = line.Count(c => vowels.Contains(c));
            if(vowelsCount < 3) continue;
            if(!line.Where((item, index) => index > 0 && item.Equals(line.ElementAt(index-1))).Any()) continue;

            bool nice = true;
            foreach (string s in notAllowed)
            {
                if (line.Contains(s))
                {
                    nice = false;
                    break;
                }
            }

            if (nice) count++;
        }

        return count;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        foreach (string line in lines)
        {
            bool nice = HasDuplicatePair(line);

            
            if(!nice) continue;
            if(!line.Where((item, index) => index > 1 && item.Equals(line.ElementAt(index-2))).Any()) continue;

            
            if (nice) count++;
        }

        return count;    
    }
    private static bool HasThreeVowels(string str) => str
        .Where(c => c is 'a' or 'e' or 'i' or 'o' or 'u')
        .Take(3)
        .Count() == 3;

      private static bool HasDuplicatePair(string str) =>
        Enumerable.Range(0, str.Length - 1)
            .Select(i => new { index = i, pair = str.Substring(i, 2) })
            .GroupBy(_ => _.pair)
            .Where(g => g.Count() > 1)
            .Any(g => g.Last().index - g.First().index > 1);

 
    
}