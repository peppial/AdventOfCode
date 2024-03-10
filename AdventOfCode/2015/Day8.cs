using System.Text.RegularExpressions;

namespace AdventOfCode._2015;

public class Day8(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int count = 0;
        int countTotal = 0;

        foreach (string line in lines)
        {
            countTotal += line.Length;
            var linem = line.Substring(1, line.Length - 2);
            linem = linem.Replace("\\\"","~");
            linem = linem.Replace("\\\\","~");

            Regex rx = new Regex("[\\\\]{1}[x]{1}[0-9a-fA-F]{2}");
            var limenew = linem;
            var countX = rx.Matches(linem).Count();
            var cc = linem.Length - countX * 3;
            count+= cc;

        }

        return countTotal-count;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        int countTotal = 0;

        foreach (string line in lines)
        {
            countTotal += line.Length;
            var linem = line;



            var a = line.Count(c => c is '\\' or '\"')+2 ;
            count+= a;

        }

        return count;    }
}