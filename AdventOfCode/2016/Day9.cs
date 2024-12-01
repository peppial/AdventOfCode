using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode.Extensions;

namespace AdventOfCode._2016;

public class Day9(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        return Extract(true);
    }
    public long GetTotalPartB()
    {
        string line = lines[0];
        long[] weights = new long[line.Length];
        long count = 0;
        for (int i = 0; i < line.Length; i++) weights[i] = 1;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '(')
            {
                int end = line.IndexOf(')', i);
                string capture = line.Substring(i, end - i + 1);
                int[] repNumbers = capture.GetNumbers();
                //int weight = repNumbers[0] * (repNumbers[1]-1);
                for (int j = end + 1; j < end + repNumbers[0] +1; j++)
                {
                    weights[j]*= repNumbers[1];
                }

                i += capture.Length-1;
            }
            else count += weights[i];
        }
        return count;

    }
    private long Extract(bool partOne)
    {
        string pattern = @"(\(\d+x\d+\))";
        Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
        foreach (string linet in lines)
        {
            
            int matchCount = 0;
            string line = linet;
            
            StringBuilder final = new StringBuilder();
            line = linet;
            while (true)
            {
                Match m = r.Match(line);
                int lastindex = 0;
                bool success = false;
               // iteration++;
                while (m.Success)
                {
                    success = true;
                    Console.WriteLine("Match" + (++matchCount));

                    Group g = m.Groups[1];
                    Console.WriteLine("Group" + 1 + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        if (c.Index > 0 && line[c.Index - 1] == ')' || c.Index < lastindex) continue;
                        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                        int[] repNumbers = c.Value.GetNumbers();
                        string repeat = line.Substring(c.Index + c.Length, repNumbers[0]);
                        var repeated = string.Concat(Enumerable.Repeat(repeat, repNumbers[1]));
                        final.Append(line.Substring(lastindex, c.Index - lastindex) + repeated);
                        lastindex = c.Index + c.Length + repeat.Length;
                    }

                    m = m.NextMatch();
                }

                final.Append(line.Substring(lastindex));
                if (partOne || !success) break;
                
                line = final.ToString();
                final = new StringBuilder();
            }

            Console.WriteLine(final);
            return final.Length;
        }

        return 0;

    }


}