﻿global using AdventOfCode;
using AdventOfCode._2016;

Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff tt")) ;
var lines = File.ReadAllLines("Test.txt"); 
var day = new Day5(lines);
Console.WriteLine("-----------");
Console.WriteLine(day.GetTotalPartB());
