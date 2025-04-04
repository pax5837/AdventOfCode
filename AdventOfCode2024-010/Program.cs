﻿// https://adventofcode.com/2024/day/10

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_010;
using AdventOfCode2024_010.DylanBeattie;

const bool isExample = false;

const string exampleFolder = "ExampleData";
const string inputFolder = "InputData";
var folder = isExample ? exampleFolder : inputFolder;
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var expectedPart1 = isExample ? "36" : "760";
var expectedPart2 = isExample ? "81" : "1764";

Execute("Part 1", expectedPart1, () => Part1.Process(lines, false && isExample));
Execute("Part 1 V2", expectedPart1, () => Part1V2.Process(lines, false && isExample));
Execute("Part 1 V3", expectedPart1, () => Part1V3.Process(lines, false && isExample));
Execute("Part 1 Dylan Beattie", expectedPart1, () => Part1DylanBeatie.Process(lines, false && isExample));
Execute("Part 2", expectedPart2, () => Part2.Process(lines, false && isExample));
Execute("Part 2 V2", expectedPart2, () => Part2V2.Process(lines, false && isExample));
Execute("Part 2 Dylan Beattie", expectedPart2, () => Part2DylanBeatie.Process(lines, false && isExample));

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