using System.Collections.Immutable;

namespace AdventOdCode2024_008;

internal static class Part1
{
	public static string Process(IImmutableList<string> lines)
	{
		var positions = PositionsFactory.BuildPositions(lines);
		var maxX = positions.Select(pos => pos.Coordinates).MaxBy(c => c.X)!.X;
		var maxY = positions.Select(pos => pos.Coordinates).MaxBy(c => c.Y)!.Y;
		var maxCoordinates = positions.Single(p => p.Coordinates.X == maxX && p.Coordinates.Y == maxY).Coordinates;
		var positionsByFrequency = positions
			.Where(pos => pos.AntennaFrequency is not null)
			.GroupBy(position => position.AntennaFrequency)
			.Select(group => group.ToImmutableList())
			.ToImmutableList();

		var antiNodeCoordinates = positionsByFrequency
			.SelectMany(freqGroup => freqGroup.SelectMany((pos, index) => FindAntiNodeCoordinates(pos, index, freqGroup, maxCoordinates)))
			.ToImmutableHashSet();


		return antiNodeCoordinates.Count.ToString();
	}

	private static List<Coordinates> FindAntiNodeCoordinates(
		Position pos,
		int currentIndex,
		IImmutableList<Position> freqGroup,
		Coordinates maxCoordinates)
	{
		var size = freqGroup.Count;
		if (currentIndex >= size - 1)
		{
			return [];
		}

		return Enumerable
			.Range(currentIndex + 1, size - 1 - currentIndex)
			.SelectMany(index => pos.GetAntiNodePositions(freqGroup[index], maxCoordinates))
			.ToList();
	}
}