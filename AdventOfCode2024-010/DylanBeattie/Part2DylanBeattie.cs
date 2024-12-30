// https://github.com/dylanbeattie/advent-of-code-2024/blob/main/day10/Program.cs

using System.Collections.Immutable;

namespace AdventOfCode2024_010.DylanBeattie;

internal class Part2DylanBeatie
{
	public static string Process(IImmutableList<string> lines, bool withConsoleOutput = false)
	{
		var grid = lines
			.Select(line => line.Select(c => c - '0').ToArray())
			.ToArray();

		List<(int Row, int Col)> trailheads = [];
		for (var row = 0; row < grid.Length; row++) {
			for (var col = 0; col < grid[0].Length; col++) {
				if (grid[row][col] == 0) trailheads.Add((row, col));
			}
		}

		var part2 = 0;
		foreach(var th in trailheads) part2 += grid.FindTrails(th.Row, th.Col);

		return part2.ToString();
	}
}