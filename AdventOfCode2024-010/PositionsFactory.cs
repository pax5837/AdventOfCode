using System.Collections.Immutable;

namespace AdventOfCode2024_010;

internal static class PositionsFactory
{
	public static IImmutableDictionary<Coordinates, Position> GetPositionsWithPopulatedOneLevelDownNeighbours(IImmutableList<string> lines)
	{
		var positions = lines.SelectMany((line, yCoord) => GetPositions(line, (uint)yCoord)).ToImmutableDictionary(pos => pos.Coordinates, x => x);

		PopulateOneLevelDownNeighbours(positions);

		return positions;
	}

	private static IImmutableList<Position> GetPositions(string line, uint yCoord)
	{
		return line
			.Select((chr, xCoord) => new Position(new Coordinates((uint)xCoord, yCoord), int.Parse(chr.ToString())))
			.ToImmutableList();
	}

	private static void PopulateOneLevelDownNeighbours(IImmutableDictionary<Coordinates, Position> positions)
	{
		var maxX = positions.Keys.Max(coord => coord.X);
		var maxY = positions.Keys.Max(coord => coord.Y);

		foreach (var position in positions.Values)
		{
			PopulateOneLevelDownNeighbours(position, positions, maxX, maxY);
		}
	}

	private static void PopulateOneLevelDownNeighbours(
		Position position,
		IImmutableDictionary<Coordinates, Position> positions,
		uint maxX,
		uint maxY)
	{
		var neighbourCoordinates = position.Coordinates.GetNeighbours(maxX, maxY);
		var neighbourOneLevelDownPositions = neighbourCoordinates
			.Select(c => positions[c])
			.Where(p => p.Height == position.Height - 1);

		position.OneLevelDownNeighbours.Clear();
		position.OneLevelDownNeighbours.AddRange(neighbourOneLevelDownPositions);
	}
}