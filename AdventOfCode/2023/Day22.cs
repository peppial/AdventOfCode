
using Advent2023.Structures;

using AdventOfCode.Extensions;
namespace Advent2023;

public class Day22(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        return GetBrickFallCounts().Count(kvp => kvp.Value == 0);
    }

    public long GetTotalPartB()
    {
        return GetBrickFallCounts().Sum(k => k.Value);
    }
    
    private Dictionary<char, int> GetBrickFallCounts()
    {
        Dictionary<char, int> brickFallCounts = [];
        List<Brick> allBricks = [];
            
        char label = 'A';
        foreach (var line in lines)
        {
            var numbers = line.GetNumbers().ToList();
            Brick brick = new(label, (numbers[0], numbers[1], numbers[2]), (numbers[3], numbers[4], numbers[5]));
            allBricks.Add(brick);
            label++;
        }

        int numBricksShifted;
        while(true)
        {
            numBricksShifted = 0;

            HashSet<Coordinate3D> allBrickPoints = [];

            foreach (var brick in allBricks)
                foreach (var point in brick.Locations())
                    allBrickPoints.Add(point);

            foreach(int i in allBricks.Count-1)
            {
                var brick = allBricks[i];
                if (brick.Locations().Any(m =>
                        m.Z == 1 || (allBrickPoints.Contains(m - (0, 0, 1)) && !brick.Locations().Contains(m - (0, 0, 1)))))
                    continue;

                allBricks[i] = new Brick(brick.Label, brick.Start - (0, 0, 1), brick.End - (0, 0, 1));
                numBricksShifted++;
            }

            if (numBricksShifted == 0) break;
        } 

        allBricks.Sort((a, b) => a.Start.Z.CompareTo(b.Start.Z));

       foreach (var brick in allBricks)
        {
            foreach (var nextBrick in allBricks.Where(a => a.Start.Z == brick.End.Z + 1))
            {
                foreach (var location in brick.Locations())
                {
                    if (nextBrick.Locations().Any(a => a.X == location.X && a.Y == location.Y && a.Z - 1 == location.Z))
                    {
                        brick.Supports.Add(nextBrick);
                        nextBrick.SupportedBy.Add(brick);
                        break;
                    }
                }
            }
        }

        foreach (var b in allBricks) BrickFallCount(brickFallCounts, b, new(), true);
        
        return brickFallCounts;
    }
    

    private void BrickFallCount(Dictionary<char, int> brickFallCounts, Brick brick, List<char> toIgnore, bool updateDict = false)
    {
        toIgnore.Add(brick.Label);
        if (brick.Supports.Count == 0)
        {
            if (updateDict) brickFallCounts[brick.Label] = 0;
        }

        var bricksThatFall = brick.Supports.Where(p => p.SupportedBy.Count(a => !toIgnore.Contains(a.Label)) == 0).ToList();
        foreach (var p in bricksThatFall)
        {
            toIgnore.Add(p.Label);
        }

        foreach (var p in bricksThatFall)
        {
            BrickFallCount(brickFallCounts, p, toIgnore);
        }

        if (updateDict) brickFallCounts[brick.Label] = toIgnore.Distinct().Count() - 1;
    }

    record Brick(char Label, Coordinate3D Start, Coordinate3D End)
    {
        public List<Brick> Supports = new();
        public List<Brick> SupportedBy = new();
        
        public IEnumerable<Coordinate3D> Locations()
        {
            yield return Start;
            var currentLocation = Start;
            while (currentLocation != End)
            {
                if (Start.X == End.X && Start.Y == End.Y) currentLocation = currentLocation + (0, 0, 1); //Vertically Oriented Brick 
                else if (Start.X == End.X && Start.Z == End.Z) currentLocation = currentLocation + (0, 1, 0); //Oriented Along Y- Axis
                else if (Start.Y == End.Y && Start.Z == End.Z) currentLocation = currentLocation + (1, 0, 0); //Oriented Along x- Axis
                yield return currentLocation;
            }
        }
    }
}


