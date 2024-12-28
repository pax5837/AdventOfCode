// https://adventofcode.com/2024/day/9

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_009;

Console.WriteLine("[e] For example data\nother For real data");
var useExampleData = Console.ReadLine().Equals("e", StringComparison.OrdinalIgnoreCase);

var folder = useExampleData ? "ExampleData" : "InputData";
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();


Execute("Part 1", () => Part1.Process(lines[0], false));

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