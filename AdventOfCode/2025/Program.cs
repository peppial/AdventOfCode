global using AdventOfCode;
using AdventOfCode._2025;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt"));
var lines = File.ReadAllLines("Test.txt");
var day = new Day7(lines);
Console.WriteLine("-----------");
Console.WriteLine("Part A: " + day.GetTotalPartA());
Console.WriteLine("Part B: " + day.GetTotalPartB());
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt"));

