using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day1(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        return CalculateTurns(false);
    }

    public long GetTotalPartB()
    {
        return CalculateTurns(true);
    }

    private long CalculateTurns(bool all)
    {
        int currentPosition=50;
        int result=0;
        foreach (var line in lines)
        {
            int direction = line[0];
            int distance = int.Parse(line.Substring(1));
            int completeCircles = distance / 100;
            if (all) result += completeCircles;

            int remainder = distance % 100;
            int newPosition;

            if (direction == 'L') newPosition = currentPosition - remainder;
            else newPosition = currentPosition + remainder;

            int finalPos = (newPosition + 100) % 100;

            if (all)
            {
                if (remainder > 0)
                {
                    if (finalPos == 0)
                    {
                        result++;
                    }
                    else
                    {
                        bool add = (direction == 'L' && newPosition < 0) ||
                                       (direction == 'R' && newPosition >= 100);
                        if (add && currentPosition != 0) result++;
                    }
                }
            }
            else
            {
                if (finalPos == 0) result++;
            }

            currentPosition = finalPos;

        }

        return result;
    }

}
