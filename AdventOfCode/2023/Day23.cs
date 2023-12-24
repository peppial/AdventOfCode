using Advent2023.Structures;
   
   namespace Advent2023;

   public class Day23(string[] lines) : IDay
   {
       int len = lines[0].Length;
       int height = lines.Length;

       public long GetTotalPartA()
       {
           return Solve(true);
       }

       //TO be optimized
       public long GetTotalPartB()
       {
           return Solve(false);
       }

       private long Solve(bool partA)
       {
           int startIndex = lines[0].IndexOf('.');
           Stack<(Coordinate2D, HashSet<Coordinate2D>, int steps)> queue = new();
           queue.Push(((0, startIndex), [(0, startIndex)], 0));
           Dictionary<Coordinate2D, bool> all = [];

           foreach (int i in height - 1)
           {
               foreach (int j in (len - 1)) all[(i, j)] = (lines[i][j] != '#');
           }

           int maxSteps = 0;
           while (queue.Any())
           {
               (Coordinate2D start, HashSet<Coordinate2D> visited, int steps) = queue.Pop();

               if (steps > 7000) continue;
               //Console.WriteLine(start);

               char symbol = lines[start.X][(int)start.Y];

               if (start.X == len - 1)
               {
                   if (steps > maxSteps)
                   {
                       maxSteps = steps;
                       Console.WriteLine(maxSteps);
                   }

                   continue;
               }

               if (partA && symbol is '.' or '<' || !partA)
               {
                   if (start.Y > 0 && all[(start.X, start.Y - 1)] && !visited.Contains((start.X, start.Y - 1)))
                   {
                       var v = new HashSet<Coordinate2D>(visited);
                       v.Add((start.X, start.Y - 1));
                       queue.Push(((start.X, start.Y - 1), v, steps + 1));
                   }
               }

               if (partA && symbol is '.' or '^'|| !partA)
               {
                   if (start.X > 0 && all[(start.X - 1, start.Y)] && !visited.Contains((start.X - 1, start.Y)))
                   {
                       var v = new HashSet<Coordinate2D>(visited);
                       v.Add((start.X - 1, start.Y));
                       queue.Push(((start.X - 1, start.Y), v, steps + 1));
                   }
               }
               if (partA && symbol is '.' or 'v' || !partA)
               {
                   if (start.X < len - 1 && all[(start.X + 1, start.Y)] && !visited.Contains((start.X + 1, start.Y)))
                   {
                       var v = new HashSet<Coordinate2D>(visited);
                       v.Add((start.X + 1, start.Y));
                       queue.Push(((start.X + 1, start.Y), v, steps + 1));

                   }
               }
               if (partA && symbol is '.' or '>'|| !partA)
               {
                   if (start.Y < height - 1 && all[(start.X, start.Y + 1)] && !visited.Contains((start.X, start.Y + 1)))
                   {
                       var v = new HashSet<Coordinate2D>(visited);
                       v.Add((start.X, start.Y + 1));
                       queue.Push(((start.X, start.Y + 1), v, steps + 1));
                   }
               }
        
           }

           return maxSteps;
       }
   }
   
   
   