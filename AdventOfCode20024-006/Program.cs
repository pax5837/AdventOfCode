using System.Collections.Immutable;
using AdventOfCode20024_006;

https://adventofcode.com/2024/day/6

Console.WriteLine("[e]xample data?");
var isExampleData = Console.ReadLine()!.Equals("e", StringComparison.InvariantCultureIgnoreCase);
var folder = isExampleData ? "ExampleData" : "InputData";
var mapLines = File.ReadAllLines($"./{folder}/Map.txt").ToImmutableList();


var visitedCoordinates = Part1.ProcessAndReturnVisitedCoordinates(mapLines);
Part2.Process(mapLines, visitedCoordinates);