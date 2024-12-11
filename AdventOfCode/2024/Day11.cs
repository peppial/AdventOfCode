using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day11(string[] lines): IDay
{
    string line=lines[0];
    public long GetTotalPartA()
    {
        return GetCount(25);
    }

    public long GetTotalPartB()
    {
        return GetCount(75);
    }
    
    private long GetCount(int blinks)
    {
        var numbers = line.GetNumbersULong().GroupBy(v => v)
            .ToDictionary(g => g.Key, g => (long)g.Count());
     

        for (var i = 0; i < blinks; i++)
        {
            numbers = numbers
                .SelectMany(p => Process(p.Key).Select(value => (value, count: p.Value)))
                .GroupBy(x => x.value, x => x.count)
                .ToDictionary(g => g.Key, g => g.Sum());
        }

        return numbers.Values.Sum();
    }
    static IEnumerable<ulong> Process(ulong number)
    {
        if (number == 0)
        {
            yield return 1;
        }
        else
        {
            string val = number.ToString();
            int len = val.Length;
            if (len % 2 == 0)
            {
                yield return ulong.Parse(val[..(len/2)]);
                yield return ulong.Parse(val[(len/2)..]);
            }
            else
            {
                yield return number * 2024;
            }
        }
    }
}