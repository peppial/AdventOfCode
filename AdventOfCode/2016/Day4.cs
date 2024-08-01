using AdventOfCode.Extensions;

namespace AdventOfCode._2016;

public class Day4(string[] lines):IDay
{
    public long GetTotalPartA() 
    {
        int sum = 0;
        foreach (string line in lines)
        {
            int pos = line.LastIndexOf('-');
            string first = line.Substring(0, pos).Replace("-","");
            var chars = first.ToCharArray();
            var hash = new string(chars.GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .ThenBy(x=>x.Key)
                .Take(5)
                .Select(g => g.Key).ToArray());
            int numberOccurence = chars.Count(x => x == hash[4]);
            var charsTies = chars.GroupBy(i => i).Where(g => g.Count() == numberOccurence).Select(x=>x.Key).Order().ToArray();
            int pos1 = line.IndexOf('[');
            var calc = line.Substring(pos1 + 1, 5);
            if (hash == calc)
            {
                sum += -line.GetNumbers()[0];
            }

        }

        return sum;
    }

    public long GetTotalPartB()
    {
        foreach (string line in lines)
        {
            int pos = line.LastIndexOf('-');
            string first = line.Substring(0, pos).Replace("-"," ");
            int sector = line.GetNumbers()[0];
            int offset = - sector % 26;
            string last = "";
            char z = 'z';
            foreach (char c in first)
            {
                if (c is ' ')
                {
                    last += " "; continue;
                    
                }
                int charN = c + offset;
                if (charN > z) charN = charN - 26;
                last += (char)(charN);
            }
            Console.WriteLine($"{last}-{sector}");
        }

        return 0;
    }
}