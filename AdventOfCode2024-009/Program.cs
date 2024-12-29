// https://adventofcode.com/2024/day/9

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_009;

const bool isExample = false;

const string exampleFolder = "ExampleData";
const string inputFolder = "InputData";
var folder = isExample ? exampleFolder : inputFolder;
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var expectedPart1 = isExample ? "1928" : "6398252054886";
var expectedPart2 = isExample ? "2858" : "6415666220005";


Execute("Part 1", expectedPart1, () => Part1.Process(lines[0], false && isExample));
Execute("Part 1 V2", expectedPart1, () => Part1V2.Process(lines[0], false && isExample));
// Execute("Part 2", expectedPart2, () => Part2.Process(lines[0], true && isExample));
Execute("Part 2 V2", expectedPart2, () => Part2V2.Process(lines[0], true && isExample));
Execute("Part 2 V3", expectedPart2, () => Part2V3.Process(lines[0], true && isExample));

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