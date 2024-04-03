global using AdventOfCode;
using System.Collections.Specialized;
using AdventOfCode._2015;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt"); 
var day = new Day13(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());


