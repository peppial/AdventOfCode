using System.Reflection;

namespace AdventOfCode._2024;

public class Day22(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        long total = 0;
        foreach (var line in lines)
        {
            long secret = long.Parse(line);
            for (long step = 1; step <= 2000; step++)
            {
                secret = NextSecret(secret);
            }

            total += secret;
        }

        return total;
    }

    public long GetTotalPartB()
    {
        List<List<long>> allDiffs = [];
        List<List<long>> allSecrets = [];

        foreach (var line in lines)
        {
            var secretsTemp = new List<long> { long.Parse(line) };
            long tmp = long.Parse(line);
            List<long> diffsTemp = [];

            for (int step = 1; step <= 2000; step++)
            {
                tmp = NextSecret(tmp);
                secretsTemp.Add(tmp);
                diffsTemp.Add(secretsTemp[step] % 10 - secretsTemp[step - 1] % 10);
            }

            allDiffs.Add(diffsTemp);
            allSecrets.Add(secretsTemp.Skip(1).ToList());
        }

        Dictionary<(long, long, long, long), long> rangeCount = [];

        for (int buyer = 0; buyer < allDiffs.Count; buyer++)
        {
            var diffs = allDiffs[buyer];
            var secrets = allSecrets[buyer];
            var seen = new HashSet<(long, long, long, long)>();

            for (int i = 0; i < diffs.Count - 4; i++)
            {
                var range = (diffs[i], diffs[i + 1], diffs[i + 2], diffs[i + 3]);

                if (!seen.Contains(range))
                {
                    if (!rangeCount.ContainsKey(range))
                    {
                        rangeCount[range] = 0;
                    }

                    rangeCount[range] += secrets[i + 3] % 10;
                    seen.Add(range);
                }
            }
        }

        return rangeCount.Values.Max();
    }

    long NextSecret(long secret)
    {
        long a = secret * 64;
        a = (a ^ secret) % 16777216;
        a ^= (a / 32);
        a %= 16777216;
        a ^= (a * 2048);
        a %= 16777216;
        return a;
    }
}