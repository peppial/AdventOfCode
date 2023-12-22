global using AdventOfCode;

using Advent2023;
using Advent2023.Structures;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt");
var day = new Day22(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
Coordinate3D c = new Coordinate3D("1,2,3");
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
