// https://adventofcode.com/2024/day/9

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_009;

const string exampleFolder = "ExampleData";
var folder = exampleFolder;
folder = "InputData";
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();


var isExample = folder == exampleFolder;
Execute("Part 1, should return 6398252054886", () => Part1.Process(lines[0], false && isExample));
Execute("Part 1 V2, should return 6398252054886", () => Part1V2.Process(lines[0], false && isExample));
Execute("Part 2, should return 6415666220005", () => Part2.Process(lines[0], true && isExample));

void Execute(string name, Func<string> func)
{
	const int totalWidth = 50;
	Console.WriteLine(string.Empty.PadLeft(totalWidth, '#'));
	Console.WriteLine($"[{name}]");
	var sw = Stopwatch.StartNew();
	var result = func();
	sw.Stop();
	Console.WriteLine($" Result: <{result}> in {sw.ElapsedMilliseconds} ms");
	Console.WriteLine("\n".PadLeft(totalWidth, '#'));
}