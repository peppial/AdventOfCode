namespace AdventOfCode._2015;

public class Day3(string[] lines):IDay
{
    private HashSet<(int, int)> coordinates = [];
    public long GetTotalPartA()
    {
        (int X,int Y) start = (0, 0);
        foreach(char c in lines[0])
        {
            if (c == '>') start.Y++;
            
            else if (c == '<') start.Y--;
            
            else if (c == '^') start.X--;

            else if (c == 'v') start.X++;
            coordinates.Add(start);
        }
        return coordinates.Count();
    }

    public long GetTotalPartB()
    {
        (int X,int Y) startA = (0, 0);
        (int X,int Y) startB = (0, 0);

        (int X,int Y)[] arr = [startA, startB];
        int num = 0;
        foreach(char c in lines[0])
        {
            if (c == '>') arr[num].Y++;
            
            else if (c == '<') arr[num].Y--;
            
            else if (c == '^') arr[num].X--;

            else if (c == 'v') arr[num].X++;
            coordinates.Add(arr[num]);
            num = num == 0 ? 1 : 0;
        }
        return coordinates.Count()+1;    
    }
}