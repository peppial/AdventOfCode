using System.Text.RegularExpressions;
using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day19(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        var (replacements, source) = GetReplacements();

        HashSet<string> result = [];

        for (int i = 0; i < source.Length; i++)
        {
            //one char
            var repl = replacements.Where(x => x.first == source[i].ToString());
            foreach (var rep in repl)
            {
                string s = source.Substring(0, i) + rep.second + source.Substring(i + 1);
                result.Add(s);
            }
            
            //two char
            if (i < source.Length - 1)
            {
                var repl2 = replacements.Where(x => x.first == source.Substring(i,2).ToString());
                foreach (var rep in repl2)
                {
                    string s = source.Substring(0, i) + rep.second + source.Substring(i + 2);
                    result.Add(s);
                }
            }
        }
        return result.Count;
    }

    

    public long GetTotalPartB()
    {
        var (replacements,  source) = GetReplacements();
        replacements = replacements.OrderByDescending(x => x.second.Length).ToList();
        int steps = 0;
        while (true)
        {
            foreach ((string original, string replacement) in replacements)
            {
                Regex regex = new Regex(replacement);
                if (regex.Match(source).Success)
                {
                    // Replace only one occurence
                    source = regex.Replace(source, original, 1);
                    steps++;
                    break;
                }

                if (source == "e") return steps;
            }
        }
    }
    
    private (List<(string first, string second)> replacements,string source) GetReplacements()
    {
        List<(string first, string second)> replacements = [];
        string source="";
        for(int i=0;i<lines.Length;i++)
        {
            if (lines[i].Trim() == "")
            {
                source = lines[i+1];
                break;
            }
            else
            {
                string[] parts = lines[i].SplitDefault(" => ");
                replacements.Add((parts[0],parts[1]));
            }
        }

        return (replacements, source);
    }
}