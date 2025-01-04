// https://adventofcode.com/2024/day/11

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_011;

const bool isExample = false;

const string exampleFolder = "ExampleData";
const string inputFolder = "InputData";
var folder = isExample ? exampleFolder : inputFolder;
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var blinkCountPart1a = isExample ? 6 : 25;
var expectedPart1a = isExample ? "22" : "188902";

var blinkCountPart1b = isExample ? 25 : 25;
var expectedPart1b = isExample ? "55312" : "188902";

var blinkCountPart2 = isExample ? 25 : 75;
var expectedPart2 = isExample ? "55312" : "1764";

Execute("Part 1a", expectedPart1a, () => Part1.Process(lines, blinkCountPart1a, false && isExample));
Execute("Part 1b", expectedPart1b, () => Part1.Process(lines, blinkCountPart1b, false && isExample));
Execute("Part 1V2 b", expectedPart1b, () => Part1V2.Process(lines, blinkCountPart1b, false && isExample));
Execute("Part 2 using 1V2 b", expectedPart2, () => Part1V2.Process(lines, blinkCountPart2, false && isExample));
Execute("Part 2", expectedPart2, () => Part2.Process(lines, blinkCountPart2, false && isExample));

void Execute(string name, string expected, Func<string> func)
{
	const int totalWidth = 50;
	Console.WriteLine("\n".PadRight(totalWidth, '#'));
	Console.WriteLine($"[{name}]");
	var sw = Stopwatch.StartNew();
	var result = func();
	sw.Stop();
	var okText = result == expected ? "OK" : "NOT OK";
	Console.WriteLine($"{okText}, expected: <{expected}>, result: <{result}>, in {sw.Elapsed.TotalMilliseconds} ms");
	Console.WriteLine("\n\n".PadLeft(totalWidth, '#'));
}