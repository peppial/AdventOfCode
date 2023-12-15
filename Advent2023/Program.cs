using System.Text;
using Advent2023;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
//var lines = File.ReadAllLines("Test.txt");
var lines = File.ReadAllText("Test.txt");
var day = new Day15(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
