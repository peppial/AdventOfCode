namespace AdventOfCode._2022;
using AdventOfCode.Extensions;

public class Day4(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int count = 0;
        foreach (string line in lines)
        {
            (Range range00, Range range11) = GetRanges(line);
            if (IsRangeContained(range00, range11)) count++;
            else if (IsRangeContained(range11, range00)) count++;
        }

       

        return count;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        foreach (string line in lines)
        {
            (Range range00, Range range11) = GetRanges(line);
            if (IsRangeOverlapped(range00, range11)) count++;
        }

        return count;
    }

    private (Range, Range) GetRanges(string line)
    {
        var parts = line.Split(',');
        var range0 = parts[0].Split('-').Select(m => int.Parse(m)).ToArray();  
            
        var range1 = parts[1].Split('-').Select(m => int.Parse(m)).ToArray();  
           
        Range range00 = new Range(range0[0], (range0[1]));
        Range range11 = new Range(range1[0], (range1[1]));
        return (range00, range11);
    }
    private bool  IsRangeContained(Range outerRange, Range innerRange)
    {
        // Check if the start and end of the inner range are within the outer range
        bool startContained = outerRange.Start <= innerRange.Start && innerRange.Start <= outerRange.End;
        bool endContained = outerRange.Start <= innerRange.End && innerRange.End <= outerRange.End;

        var a = startContained && endContained;
        // Return true if both start and end are contained within the outer range
        return startContained && endContained;
    }
    
    private bool  IsRangeOverlapped(Range range1, Range range2)
    {
        bool noOverlap = range1.End < range2.Start || range2.End < range1.Start;

        // If there is no overlap, the ranges do not overlap
        return !noOverlap;
    }
    struct Range
    {
        public int Start { get; }
        public int End { get; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }
    }

}