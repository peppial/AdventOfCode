global using AdventOfCode;
using AdventOfCode._2024;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt"); 
var day = new Day19(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
