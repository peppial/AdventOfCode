namespace Advent2023;

using AdventOfCode.Extensions;
public class Day13(string[] lines):IDay 
{
    public long GetTotalPartA()
    {
        var total = 0;
        foreach (string[] split in Parse(lines))
        {
            total+= 100* FindHorizontalReflection(split);
            var splitPivot = Enumerable.Range(0, split[0].Length)
                .Select(col => new string(split.Select(row => row[col]).ToArray()))
                .ToArray();
            total+= FindHorizontalReflection(splitPivot);
        }
        return total;
    }

   
    public long GetTotalPartB()
    {
        var total = 0;
        foreach (string[] split in Parse(lines))
        {
            var horizontals = FindHorizontalReflectionPlus(split);
            total += 100 * horizontals.Item1;
            var splitPivot = Enumerable.Range(0, split[0].Length)
                .Select(col => new string(horizontals.Item2.Select(row => row[col]).ToArray()))
                .ToArray();
            total+= FindHorizontalReflectionPlus(splitPivot).Item1;
     
        }

        return total;
    }
    static int FindHorizontalReflection(string[] patterns)
    {
        int rowReflection = -1;
        var len = patterns[0].Length;
        var confirmed = false;
        var tempLine = -1;
        while (tempLine < len)
        {
            tempLine++;
            for (int i = tempLine; i < patterns.Length - 1; i++)
            {
                if (patterns[i] == patterns[i + 1]) {rowReflection = i;break;}
                
            }

            if (rowReflection == -1) continue;

            confirmed = true;
            for (int i = 0; i < patterns.Length - 1; i++)
            {
                var diff = rowReflection - i;
                var targetIndex = rowReflection + 1 + diff;
                if (targetIndex < 0 || targetIndex >= patterns.Length)
                {
                    continue;
                }

                if (patterns[i] != patterns[targetIndex])
                {
                    confirmed = false;
                    break;
                }
            }

            if (confirmed) return rowReflection + 1;
        }

        return 0;
    }
    
    private static string[] GetSimilarLines(int i, string[] patterns)
    {
         
        HashSet<string> list = new();
        if (i + 1 == patterns.Length) return list.ToArray();
        for(int j =i+1;j<patterns.Length;j++)
        {
            if (patterns[i].LevenshteinDistance(patterns[j])==1)
                list.Add(patterns[j]);
        }

        return list.ToArray();
    }
    static (int,string[]) FindHorizontalReflectionPlus(string[] patterns)
    {
        int rowReflection = -1;
        var len = patterns.Length;
        var tempLine = -1;
        while (tempLine < len-1)
        {
            tempLine++;

            string[] newlines = [..patterns];
            bool confirmed = false;

            List<(string,string[])> similars = new ();
            similars.Add((patterns[tempLine],patterns));
            foreach (string line in GetSimilarLines(tempLine, patterns))
                similars.Add((line, null));

            List<(int, string[])> confirmedReflects = new(); 
            foreach ((string similar, _) in similars)
            {
                newlines[tempLine] = similar;

                for (int i = tempLine; i < newlines.Length - 1; i++)
                {
                    if (newlines[i] == newlines[i + 1])
                    {
                        rowReflection = i;
                        break;
                    }

                }

                if (rowReflection == -1) continue;

                confirmed = true;
                for (int i = 0; i < patterns.Length - 1; i++)
                {
                    var diff = rowReflection - i;
                    var targetIndex = rowReflection + 1 + diff;
                    if (targetIndex < 0 || targetIndex >= patterns.Length)
                    {
                        continue;
                    }

                    if (newlines[i] != newlines[targetIndex])
                    {
                        confirmed = false;
                        break;
                    }
                }
                if (confirmed) confirmedReflects.Add((rowReflection + 1,newlines));
            }

            int min = 0;
            if (confirmedReflects.Any())
                return confirmedReflects.MinBy(x => x.Item1);
            
        }

        return (0,[]);
    }
    
    private static IEnumerable<string[]> Parse(string[] lines)
    {
        List<string> temp = [];

        foreach (string line in lines)
        {
            if (line.Trim() == "")
            {
                yield return temp.ToArray();
                temp = [];
                continue;
            }
            temp.Add(line);
        }

        yield return temp.ToArray();
    }
}

    
