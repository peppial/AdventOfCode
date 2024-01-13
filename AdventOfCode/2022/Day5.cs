using Advent2023;
using AdventOfCode.Extensions;

namespace AdventOfCode._2022;

public class Day5(string[] lines):IDay
{
    private List<Stack<char>> piles = [];
    public long GetTotalPartA()
    {
        int index = Parse(false);
        PrintResults();
        return 0;

    }

    

    public long GetTotalPartB()
    {
        int index = Parse(true);
        PrintResults();
        return 0;
    }
    private void PrintResults()
    {
        List<char> chars = [];
        for (int i = 1; i < piles.Count; i++)
        {
       
            chars.Add(piles[i].Pop());
        }

        Console.WriteLine(new string(chars.ToArray()));
    }
    private int Parse(bool partB)
    {
        var index = lines.TakeWhile(l => l.Trim() != "").Count();
        var stack = lines[index - 1].GetNumbers();
        piles.Add(null);
        foreach (int s in stack)
        {
            piles.Add(new Stack<char>());
        }
        for (int i = index - 2; i >= 0; i--)
        {
            lines[i] = lines[i].Replace("     ", " [0] ");
            lines[i] = lines[i].Replace("    [", "[0] [");
            lines[i] = lines[i].Replace("]    ", "] [0]");

            var line = lines[i].Split(' ');
            int count = 1;

            foreach (string item in line)
            {
                var toAdd = item.Trim(new char[] { '[', ']' })[0];
                if (toAdd != '0') piles[count].Push(toAdd);
                count++;
            }
        }

        for (int i = index +1; i < lines.Length; i++)
        {
            int[] moves = lines[i].GetNumbers();
            var total = moves[0];
            Stack<char> inter = [];

            foreach (int j in 1..total)
            {
                char c = piles[moves[1]].Pop();
                if (partB) inter.Push(c);
                else
                    piles[moves[2]].Push(c);
            }

            if (partB)
            {
                while(inter.Count>0) piles[moves[2]].Push(inter.Pop());
            }
        }

        return index - 1;
    }
}