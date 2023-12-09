// See https://aka.ms/new-console-template for more information


using Advent2023;

Console.WriteLine(DateTime.Now) ;
var lines = File.ReadAllLines("Test.txt");
var day = new Day8(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
Console.WriteLine(DateTime.Now) ;




