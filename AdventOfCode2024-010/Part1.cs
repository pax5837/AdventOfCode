using System.Collections.Immutable;
namespace AdventOfCode2024_010;

internal static class Part1
{
	public static string Process(IImmutableList<string> lines, bool withConsoleOutput = false)
	{
		var positionsByCoordinates = PositionsFactory.GetPositionsWithPopulatedOneLevelDownNeighbours(lines);

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
		Position position,
		Position top,
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