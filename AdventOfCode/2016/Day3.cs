using AdventOfCode.Extensions;

namespace AdventOfCode._2016;

public class Day3(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int triangles = 0;
        foreach (string line in lines)
        {
            int[] parts = line.GetNumbers();
            if (parts[0] < parts[1] + parts[2] && parts[1] < parts[0] + parts[2] &&
                parts[2] < parts[1] + parts[0]) triangles++;
        }

        return triangles;
    }

    public long GetTotalPartB()
    {
        int triangles = 0;
        List<int> first = [];
        List<int> second = [];
        List<int> third = [];
        foreach (string line in lines)
        {
            int[] parts = line.GetNumbers();
            first.Add(parts[0]);
            second.Add(parts[1]);
            third.Add(parts[2]);
        }
        for(int i=0;i<lines.Length;i+=3)
        {
            if (first[i] < first[i+1] + first[i+2] && first[i+1] < first[i] + first[i+2] &&
                first[i+2] < first[i+1] + first[i]) triangles++;
            if (second[i] < second[i+1] + second[i+2] && second[i+1] < second[i] + second[i+2] &&
                second[i+2] < second[i+1] + second[i]) triangles++;
            if (third[i] < third[i+1] + third[i+2] && third[i+1] < third[i] + third[i+2] &&
                third[i+2] < third[i+1] + third[i]) triangles++;
        }

        return triangles;    
    }
}