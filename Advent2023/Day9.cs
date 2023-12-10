namespace Advent2023;

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
        int newValue = histories[0][^1];

        while (index < histories.Count)
        {
            newValue+= histories[index][^1];
            index++;
        }

        return newValue;
    }
    
    private static int PredictLeft(List<int[]> histories)
    {
        int index = 0;
        histories.Reverse();
        int newValue = histories[0][0];

        while (index < histories.Count)
        {
            newValue = histories[index][0]-newValue;
            index++;
        }

        return newValue;
    }
}



