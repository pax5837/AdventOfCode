// https://adventofcode.com/2024/day/10

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_010;

const bool isExample = false;

const string exampleFolder = "ExampleData";
const string inputFolder = "InputData";
var folder = isExample ? exampleFolder : inputFolder;
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var expectedPart1 = isExample ? "36" : "760";
var expectedPart2 = isExample ? "??" : "??";

Execute("Part 1", expectedPart1, () => Part1.Process(lines, false && isExample));

void Execute(string name, string expected, Func<string> func)
{
	const int totalWidth = 50;
	Console.WriteLine(string.Empty.PadLeft(totalWidth, '#'));
	Console.WriteLine($"[{name}]");
	var sw = Stopwatch.StartNew();
	var result = func();
	sw.Stop();
	var okText = result == expected ? "OK" : "NOT OK";
	Console.WriteLine($"{okText}, expected: <{expected}>, result: <{result}>, in {sw.ElapsedMilliseconds} ms");
	Console.WriteLine("\n".PadLeft(totalWidth, '#'));
}