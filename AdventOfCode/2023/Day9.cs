namespace Advent2023;

using AdventOfCode.Extensions;

public class Day9(string[] lines) : IDay
{
    
    public long GetTotalPartA()
    {
        var histories = lines.Select(CreateHistory);
        return histories.Sum(PredictRight);
    }
    public long GetTotalPartB()
    {
        var histories = lines.Select(CreateHistory);
        return histories.Sum(PredictLeft);
    }
    
    private static List<int[]> CreateHistory(string line)
    {
        var first = line.GetNumbers();
        var histories = new List<int[]> { first };
        
        while (histories[^1].Any(x => x != 0))
        {
            var newHistory = histories[^1]
                .Skip(1)
                .Select((value, index) => value - histories[^1][index]);
            
            histories.Add(newHistory.ToArray());
        }
        
        return histories;
    }
    
    private static int PredictRight(List<int[]> histories)
    {
        int index =0;
        histories.Reverse();
        
        int newValue = histories.Skip(index).Aggregate(0, (newValue, item) => (newValue + item[^1]));
        return newValue;
    }
    
    private static int PredictLeft(List<int[]> histories)
    {
        int index = 0;
        histories.Reverse();
       
        int newValue = histories.Skip(index).Aggregate(0, (acc, item) => (item[0] - acc));
        return newValue;
    }
}



