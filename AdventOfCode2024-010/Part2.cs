using System.Collections.Immutable;

namespace AdventOfCode2024_010;

internal static class Part2
{
	public static string Process(IImmutableList<string> lines, bool withConsoleOutput = false)
	{
		var positionsByCoordinates = Positions2Factory.GetPositionsWithPopulatedOneLevelDownNeighbours(lines);

		var tops = positionsByCoordinates.Values.Where(pos => pos.Height == 9).ToImmutableList();

		foreach (var top in tops)
		{
			PopulateConnectionToTrailhead(top, top, withConsoleOutput);
		}

		return positionsByCoordinates
			.Values
			.Where(pos => pos.Height == 0 && pos.ConnectedTopCoordinates.Any())
			.Select(pos => pos.ConnectedTopCoordinates.Count)
			.Sum()
			.ToString();
	}

	private static void PopulateConnectionToTrailhead(
		Position2 position,
		Position2 top,
		bool withConsoleOutput)
	{
		foreach (var neighbour in position.OneLevelDownNeighbours)
		{
			neighbour.ConnectedTopCoordinates.Add(top.Coordinates);

			if (neighbour.Height > 0)
			{
				PopulateConnectionToTrailhead(neighbour, top, withConsoleOutput);
			}
		}
	}
}