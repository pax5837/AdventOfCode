using System.Collections.Immutable;
using System.Diagnostics;

namespace AdventOfCode2024_010;

internal class Part2V2
{
	public static string Process(IImmutableList<string> lines, bool withConsoleOutput = false)
	{
		var sw = Stopwatch.StartNew();
		var positionsByCoordinates = Positions2Factory.GetPositionsWithPopulatedOneLevelUpNeighbours(lines);
		sw.Stop();
		Console.WriteLine($"Building: {sw.ElapsedMilliseconds}ms");

		var trailHeads = positionsByCoordinates.Where(pos => pos.Height == 0 && pos.OneLevelUpNeighbours.Any()).ToImmutableList();


		var topCount = 0;
		foreach (var trailHead in trailHeads)
		{
			var tops = new List<Coordinates>();
			PopulateConnectionToTrailhead(trailHead, tops, withConsoleOutput);
			topCount += tops.Count();
		}

		return topCount
			.ToString();
	}

	private static void PopulateConnectionToTrailhead(
		Position2 position,
		List<Coordinates> tops,
		bool withConsoleOutput)
	{
		if (position.Height == 9)
		{
			tops.Add(position.Coordinates);
		}

		foreach (var neighbour in position.OneLevelUpNeighbours)
		{
			PopulateConnectionToTrailhead(neighbour, tops, withConsoleOutput);
		}
	}
}