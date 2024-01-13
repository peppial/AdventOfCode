namespace AdventOfCode._2022;

public class Day3(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int total = 0;

        foreach (string line in lines)
        {
            string[] parts = [line.Substring(0, line.Length / 2), line.Substring(line.Length / 2)];
            char match = parts[0].ToCharArray().Intersect(parts[1].ToCharArray()).First();
            total += GetPriority(match);
        }

        return total;
    }

    public long GetTotalPartB()
    {
        int total = 0;

        for(int i=0;i<lines.Length;i+=3)
        {
            var matches = lines[i].ToCharArray().Intersect(lines[i+1].ToCharArray());
            var match = lines[i + 2].ToCharArray().Intersect(matches).First();
            total += GetPriority(match);
        }

        return total;    
    }

    private int GetPriority(char c)
    {
        return char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27;
    }
    
    
}