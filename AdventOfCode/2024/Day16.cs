using System.Runtime.InteropServices;
using Advent2023;

namespace AdventOfCode._2024;

public class Day16: IDay
{
    int rows;
    int cols;
    private string[] lines;

    (int row,int col) SPos = (0, 0);
    private int bestScore = int.MaxValue;
    
    Dictionary<(int row, int col, Direction dir), int> lowestCost =[];

    private HashSet<(int, int)> finalSet = [];
    private PriorityQueue<(int row, int col, Direction direction, int score, HashSet<(int,int)> set, string path), int> queue = new();
    enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
    public Day16(string[] lines)
    {
        this.lines = lines;
        rows = lines.Length;
        cols = lines[0].Length;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if(lines[i][j] == 'S') SPos = (i, j);
            }
        }
    }
    public long GetTotalPartA()
    {
        return GetResult(true);
    }
    public long GetTotalPartB()
    {
        return GetResult(false);
    }
    long GetResult(bool partA)
    {
        Direction direction = Direction.Right;
        
        queue.Enqueue((SPos.row, SPos.col,Direction.Right,0,[(SPos.row, SPos.col)],$"({SPos.row}, {SPos.col})"),0);
        queue.Enqueue((SPos.row, SPos.col,Direction.Down,1000,[(SPos.row, SPos.col)], $"({SPos.row}, {SPos.col})"),1000);
        queue.Enqueue((SPos.row, SPos.col,Direction.Up,1000,[(SPos.row, SPos.col)],$"({SPos.row}, {SPos.col})"),1000);
        
        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            
            direction = pos.direction;
            (int row, int col) pos1 = direction switch
            {
                Direction.Right => (pos.row, pos.col + 1),
                Direction.Down => (pos.row + 1, pos.col),
                Direction.Left => (pos.row, pos.col - 1),
                Direction.Up => (pos.row - 1, pos.col)
            };
     
            int nextscore=pos.score + 1;
            if (IsInGrid(pos1))
            {
                if (lines[pos1.row][pos1.col] == 'E')
                {
                    
                    Console.WriteLine(pos.path);
                    if (partA) return nextscore;
                    if(nextscore>bestScore) continue;
                    foreach(var s in pos.set) finalSet.Add(s);
                    bestScore = nextscore;
                }
               
                if (lines[pos1.row][pos1.col] == '.')
                {
                    AddToQueue(pos1.row, pos1.col, direction, nextscore,pos.set, pos.path + $"({pos1.row}, {pos1.col})");
                }
            }
            nextscore = pos.score + 1001;
            (int row, int col) pos2 = direction switch
            {
                Direction.Right => (pos.row + 1, pos.col),
                Direction.Down => (pos.row, pos.col - 1),
                Direction.Left => (pos.row - 1, pos.col),
                Direction.Up => (pos.row, pos.col + 1)
            };
            if (IsInGrid(pos2))
            {
                if (lines[pos2.row][pos2.col] == 'E' )
                {
                    Console.WriteLine(pos.path);
                    if (partA) return nextscore;
                    if(nextscore>bestScore) continue;
                    foreach(var s in pos.set) finalSet.Add(s);
                    bestScore = nextscore;
                }
                if (lines[pos2.row][pos2.col] == '.')
                {   
                    AddToQueue(pos2.row, pos2.col, EnumUtils.IncrementEnum(direction), nextscore, pos.set,pos.path + $"({pos2.row}, {pos2.col})");
                }
            }
            
            (int row, int col) pos3 = direction switch
            {
                Direction.Right => (pos.row-1, pos.col),
                Direction.Down => (pos.row, pos.col+1),
                Direction.Left => (pos.row+1, pos.col),
                Direction.Up => (pos.row, pos.col-1)
            };
            
            if (IsInGrid(pos3))
            {
                if (lines[pos3.row][pos3.col] == 'E')
                {
                    Console.WriteLine(pos.path);
                    if (partA) return nextscore;
                    if(nextscore>bestScore) continue;
                    foreach(var s in pos.set) finalSet.Add(s);
                    bestScore = nextscore;
                }

                if (lines[pos3.row][pos3.col] == '.')
                {
                    AddToQueue(pos3.row, pos3.col, EnumUtils.DecrementEnum(direction), nextscore, pos.set,pos.path + $"({pos3.row}, {pos3.col})");
                }
            }
        }
        
        return finalSet.Count+1;

        void AddToQueue(int row, int col, Direction direction, int score, HashSet<(int row, int col)> set, string path)
        {
            var key = (row, col, direction);
            if (lowestCost.TryGetValue(key, out int existingCost) && existingCost < score)
            {
                return;
            }

            lowestCost[key] = score;
            HashSet<(int row, int col)> nset = new(set);
            nset.Add((row, col));
            queue.Enqueue((row, col, direction, score,nset, path + $"({row}, {col})"), score);
        }
        bool IsInGrid((int row, int col) pos)
        {
            return (pos.row > 0 && pos.row < rows && pos.col > 0 && pos.col < cols);
        }
    }
}