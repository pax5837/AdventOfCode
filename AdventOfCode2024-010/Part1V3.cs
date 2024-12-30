using System.Collections.Immutable;

namespace AdventOfCode2024_010;

internal static class Part1V3
{
	public static string Process(IImmutableList<string> lines, bool withConsoleOutput = false)
	{
		var positionsByCoordinates = Positions2Factory.GetPositionsWithPopulatedOneLevelUpNeighbours(lines);

		var trailHeads = positionsByCoordinates.Where(pos => pos.Height == 0).ToImmutableList();


		var topCount = 0;
		foreach (var trailHead in trailHeads)
		{
			var tops = new List<Coordinates>();
			PopulateConnectionToTrailhead(trailHead, tops, withConsoleOutput);
			topCount += tops.Distinct().Count();
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