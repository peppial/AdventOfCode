using Advent2023.Structures;

namespace AdventOfCode._2015;

public class Day18(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        var all = Iterate(true);

        return all.Count(x => x.Value);
    }

    private Dictionary<Coordinate2D, bool> Iterate(bool partA)
    {
        Dictionary<Coordinate2D, bool> all = [];
        
        for(int row=0; row<lines.Length; row++)
        {
            for (int column = 0; column < lines[0].Length; column++)
            {
                all.Add(new Coordinate2D(row,column), lines[row][column]=='#');
            }
        }
        
        int maxCount = 100;
        
        int count = 0;
        while (count < maxCount)
        {
            Dictionary<Coordinate2D, bool> clone = new Dictionary<Coordinate2D, bool>(all);
            for(int row=0; row<lines.Length; row++)
            {

                for (int column = 0; column < lines[0].Length; column++)
                {
                    if(!partA)
                    {
                        if ((row==0 ||row==lines.Length-1) && (column==0 || column==lines[0].Length-1)) continue;
                    } 

                    var coord = new Coordinate2D(row, column);
                    bool val = all[coord];
                    int lightsR = GetCountRound(coord,all);
                    clone[coord] = (val && (lightsR == 2 || lightsR == 3) || !val && lightsR == 3);
                }
            }

            all = clone;
            count++;
        }

        return all;
    }
    public long GetTotalPartB()
    {
        var all = Iterate(false);

        return all.Count(x => x.Value);
    }
    private int GetCountRound(Coordinate2D coord, Dictionary<Coordinate2D, bool> all)
    {
        Coordinate2D[] array =
        [
            new Coordinate2D(coord.X - 1, coord.Y),
            new Coordinate2D(coord.X + 1, coord.Y),
           
            new Coordinate2D(coord.X, coord.Y-1),
            new Coordinate2D(coord.X, coord.Y+1),
            new Coordinate2D(coord.X - 1, coord.Y-1),
            new Coordinate2D(coord.X + 1, coord.Y+1),
            new Coordinate2D(coord.X - 1, coord.Y+1),
            new Coordinate2D(coord.X + 1, coord.Y-1)
        ];
        return all.Count(x => x.Value && array.Contains(x.Key));
    }
    
}