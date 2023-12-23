global using AdventOfCode;

using Advent2023;
using Advent2023.Structures;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt");
var day = new Day23(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartA());
