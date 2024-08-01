using System.Text;

namespace AdventOfCode._2016;

public class Day6(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        var result = GetPassword(true);

        Console.WriteLine(result.ToString());
        return 0; 
    }


    public long GetTotalPartB()
    {
        Console.WriteLine(GetPassword(false).ToString());
        return 0;
    }

    private StringBuilder GetPassword(bool partA)
    {
        string[] columns = new string[lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                columns[j] += (lines[i][j]);
            }
        }

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < columns.Length; i++)
        {
            if (partA)
                result.Append(columns[i].GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
            else
                result.Append(columns[i].GroupBy(x => x).OrderBy(x => x.Count()).Select(x => x.Key).First());
        }

        return result;
    }
}