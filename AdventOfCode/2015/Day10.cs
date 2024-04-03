using System.Text;

namespace AdventOfCode._2015;

public class Day10(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        string line = lines[0];
        StringBuilder output = new StringBuilder();

        for (int j = 1; j <= 50; j++)
        { 
            output = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                int countRepeating = GetRepeatingAfter(line, i);
                if (countRepeating > 1)
                {
                    output.Append(countRepeating);
                    output.Append(line[i]);
                    i += countRepeating - 1;

                }
                else
                {
                    output.Append(1);
                    output.Append(line[i]);
                }
            }

            Console.WriteLine(output);
            line = output.ToString();
        }

        return output.Length;
    }

    private int GetRepeatingAfter(string line, int index)
    {
        int count = 1;
        for (int i = index+1; i < line.Length; i++)
        {
            if (line[i] == line[index]) count++;
            else break;
        }

        return count;
    }
    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}