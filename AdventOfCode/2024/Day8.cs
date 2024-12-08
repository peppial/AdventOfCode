namespace AdventOfCode._2024;

public class Day8(string[] lines): IDay
{
    public long GetTotalPartA()
    {

        return GetAntidotes(true).Count;
    }

    public long GetTotalPartB()
    {
        return GetAntidotes(false).Count;
    }

    private HashSet<(int, int)> GetAntidotes(bool partA)
    {
        var map = GetAntennas();
        HashSet<(int,int)> antidotes = [];
        foreach (var (antenna, positions) in map)
        {
            for (int pos0 = 0; pos0 < positions.Count; pos0++)
            {
                for (int pos1 = pos0+1; pos1 < positions.Count; pos1++)
                {
                    if (!partA)
                    {
                        antidotes.Add((positions[pos0].row, positions[pos0].column));
                        antidotes.Add((positions[pos1].row, positions[pos1].column));
                    }

                    int count = 0;
                    var positions0 = positions[pos0];
                    var positions1 = positions[pos1];
                    
                    while (true)
                    {
                        int row1 = 2 * positions0.row - positions1.row;
                       
                        int column1 = 0;
                        int diff = Math.Abs(positions0.column - positions1.column);
                        bool positive = positions1.column > positions0.column;
                        
                        if (positive)
                            column1 = positions0.column - diff;
                        else
                            column1 = positions0.column + diff;

                        if (row1 >= 0 && row1 < lines.Length && column1 >= 0 && column1 < lines[0].Length)
                            antidotes.Add((row1, column1));
                        else break;
                        if (partA) break;
                        count++;
                        positions1 = (positions0.row, positions0.column);
                        positions0 = (row1, column1);
                        
                    }
                    positions0 = positions[pos0];
                    positions1 = positions[pos1];

                    count = 0;
                    while (true)
                    {
                        int row2 = 2 * positions1.row - positions0.row;

                        int column2 = 0;
                        int diff = Math.Abs(positions0.column - positions1.column);
                        bool positive = positions1.column > positions0.column;
                        if (positive)
                            column2 = positions1.column + diff;
                        else
                            column2 = positions1.column - diff;
                        
                        if (row2 >= 0 && row2 < lines.Length && column2 >= 0 && column2 < lines[0].Length)
                            antidotes.Add((row2, column2));
                        else break;
                        
                        if (partA) break;
                        count++;
                        positions0 = (positions1.row, positions1.column);
                        positions1 = (row2, column2);                        
                    }
                }
            }
        }
        for (int row = 0; row <lines.Length; row++)
        {
            for (int column = 0; column <lines[0].Length; column++)
            {
                if(antidotes.Contains((row, column))) Console.Write("#");
                else Console.Write(lines[row][column]);
               
            }
            Console.WriteLine("");
        }

        return antidotes;
    }
    private Dictionary<char, List<(int row, int column)>> GetAntennas()
    {
        Dictionary<char, List<(int row, int column)>> map = [];
        for (int row = 0; row <lines.Length; row++)
        {
            for (int column = 0; column <lines[0].Length; column++)
            {
                if (lines[row][column] != '.')
                {
                    if (!map.ContainsKey(lines[row][column]))
                        map.Add(lines[row][column], [(row, column)]);
                    else
                        map[lines[row][column]].Add((row, column));
             
                }
            }
        }

        return map;
    }
  
}