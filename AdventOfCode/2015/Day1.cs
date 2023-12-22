using AdventOfCode;

namespace Advent2015;


public class Day1(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        return lines[0].ToCharArray().Count(c => c == '(') - lines[0].ToCharArray().Count(c => c == ')');
    }

    public long GetTotalPartB()
    {
        int current = 0;
        int count = 0;
        while (current != -1)
        {
            current = lines[0][count] switch
            {
                '(' => current + 1,
                ')' => current - 1,
                _=>current
            };
            count++;
        }

        return count;
    }
}