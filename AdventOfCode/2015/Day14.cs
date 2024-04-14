using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day14(string[] lines):IDay
{
    List<(string name, int velocity, int seconds, int rest)> reindeers = [];

    public long GetTotalPartA()
    {
        SetReindeers();

        int totalSec = 2503;
        int maxKm = 0;
        foreach(var reindeer in reindeers)
        {
            int turns = totalSec / (reindeer.seconds + reindeer.rest);
            int leftSec = totalSec - turns * (reindeer.seconds + reindeer.rest);
            leftSec = leftSec > reindeer.seconds ? reindeer.seconds : leftSec;
            int km = turns * reindeer.seconds* reindeer.velocity + leftSec*reindeer.velocity;
            if (km > maxKm) maxKm = km;
        }

        return maxKm;
    }

   

    public long GetTotalPartB()
    {
        SetReindeers();
        int totalSec = 2503;
        int maxPoints = 0;
        Dictionary<string, int> dict = [];
        string lastWin = "";
        Dictionary<string, int> km=[];
        foreach (var reindeer in reindeers)
        {
            km.TryAdd(reindeer.name, 0);
            dict.TryAdd(reindeer.name, 0);

        }
        for (int i = 0; i < totalSec; i++)
        {
            int maxkm = 0;
            

            foreach (var reindeer in reindeers)
            {
                if (!IsResting(reindeer, i))
                {
                    if (reindeer.velocity > maxkm)
                    {
                       
                       
                        maxkm = reindeer.velocity;
                    }
                    km[reindeer.name]+=reindeer.velocity;

                }
            }

            int max=km.MaxBy(x => x.Value).Value;
            var list = km.Where(x => x.Value == max).Select(x=>x.Key).ToList();
            foreach (string reindeer in list)
            {
                dict[reindeer]++;
            }
        }

        return dict.MaxBy(x => x.Value).Value;
    }

    private bool IsResting((string name, int velocity, int seconds, int rest) reindeer, int totalSec)
    {
        int turns = totalSec / (reindeer.seconds + reindeer.rest);
        int leftSec = totalSec - turns * (reindeer.seconds + reindeer.rest);
        return leftSec+1 > reindeer.seconds;
    }

    private void SetReindeers()
    {
        foreach (string line in lines)
        {
            string[] parts = line.Split();
            int[] numbers = line.GetNumbers();
            reindeers.Add((parts[0],numbers[0],numbers[1],numbers[2]));
        }
    }
}