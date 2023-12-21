global using AdventOfCode;

using Advent2023;
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt");
var day = new Day21(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartA());
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
