// https://adventofcode.com/2024/day/4
// https://adventofcode.com/2024/day/4#part2

using System.Collections.Immutable;
using System.Text.RegularExpressions;
using AdventOfCode2024_004;


var lines = File.ReadAllLines("InputData.txt").ToImmutableList();

var contentArray = To2DArray(lines);

Part1.Solve(lines, contentArray);
Console.WriteLine("\n\n");
Part2.Solve(lines, contentArray);

char[,] To2DArray(ImmutableList<string> input)
{
	var arr = new char[input.Count, input.First().Length];

	foreach (var (line, lineNr) in input.Select((line, lineNr) => (line, lineNr)))
	{
		foreach (var (c, colNr) in line.Select((c, colNr) => (c, colNr)))
		{
			arr[lineNr, colNr] = c;
		}
	}

	return arr;
}
