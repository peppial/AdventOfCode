namespace AdventOfCode._2024;

public class Day25 : IDay
{
    private List<int[]> keys = [];
    private List<int[]> locks = [];

    public Day25(string[] lines)
    {
        for (int i = 0; i < lines.Length; i += 8)
        {
            if (lines[i][0] == '#')
            {
                int[] l = new int[5];
                for (int j = i + 1; j <= i + 5; j++)
                {
                    Add(j, l, 0);
                    Add(j, l, 1);
                    Add(j, l, 2);
                    Add(j, l, 3);
                    Add(j, l, 4);
                }

                locks.Add(l);
            }
            else
            {
                int[] k = new int[5];
                for (int j = i + 1; j <= i + 5; j++)
                {
                    Add(j, k, 0);
                    Add(j, k, 1);
                    Add(j, k, 2);
                    Add(j, k, 3);
                    Add(j, k, 4);
                }

                keys.Add(k);
            }
        }

        void Add(int ij, int[] l, int index)
        {
            if (lines[ij][index] == '#')
                l[index]++;
        }
    }

    public long GetTotalPartA()
    {
        int count = 0;
        foreach (var key in keys)
        {
            foreach (var l in locks)
            {
                if (!key.Where((k, i) => k + l[i] > 5).Any())
                {
                    count++;
                }
            }
        }

        return count;
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}