using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day3(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(2);

    public long GetTotalPartB() => Calculate(12);


    private long Calculate(int count)
    {
        long total = 0;

        foreach (var line in lines)
        {
            int lastIndex = -1;
            string currentNumber = "";
            bool possible = true;

            for (int k = 0; k < count; k++)
            {
                int remainingNeeded = count - 1 - k;
                int searchStart = lastIndex + 1;
                int searchEnd = line.Length - 1 - remainingNeeded;

                if (searchStart > searchEnd)
                {
                    possible = false;
                    break;
                }

                int bestDigit = -1;
                int bestDigitIndex = -1;

                for (int d = 9; d >= 1; d--)
                {
                    char c = (char)('0' + d);
                    int idx = line.IndexOf(c, searchStart);
                    if (idx != -1 && idx <= searchEnd)
                    {
                        bestDigit = d;
                        bestDigitIndex = idx;
                        break;
                    }
                }

                if (bestDigit != -1)
                {
                    currentNumber += bestDigit;
                    lastIndex = bestDigitIndex;
                }
                else
                {
                    possible = false;
                    break;
                }
            }

            if (possible && currentNumber.Length == count)
            {
                total += long.Parse(currentNumber);
            }
        }

        return total;
    }

}
