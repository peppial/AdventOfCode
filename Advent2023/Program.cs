using Advent2023;

Console.WriteLine(DateTime.Now) ;
var lines = File.ReadAllLines("Test.txt");
var day = new Day12(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
