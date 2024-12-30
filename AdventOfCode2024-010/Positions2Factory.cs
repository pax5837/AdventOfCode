using System.Collections.Immutable;

namespace AdventOfCode2024_010;

internal static class Positions2Factory
{
	public static IImmutableDictionary<Coordinates, Position2> GetPositionsWithPopulatedOneLevelDownNeighbours(IImmutableList<string> lines)
	{
		var positions = lines.SelectMany((line, yCoord) => GetPositions2(line, (uint)yCoord)).ToImmutableDictionary(pos => pos.Coordinates, x => x);

		PopulateOneLevelDownNeighbours(positions);

		return positions;
	}

	public static Position2[] GetPositionsWithPopulatedOneLevelUpNeighbours(IImmutableList<string> lines)
	{
		var maxY = lines.Count - 1;
		var maxX = lines.First().Length - 1;
		var positions = lines.Select((line, yCoord) => GetPositions2(line, (uint)yCoord)).ToArray();

		PopulateOneLevelUpNeighbours(positions, maxX, maxY);

		return positions.SelectMany(row => row.Select(x => x)).ToArray();
	}

	private static Position2[] GetPositions2(string line, uint yCoord)
	{
		return line
			.Select((chr, xCoord) => new Position2(new Coordinates((uint)xCoord, yCoord), int.Parse(chr.ToString())))
			.ToArray();
	}

	private static void PopulateOneLevelUpNeighbours(Position2[][] positions, int maxX, int maxY)
	{
		for (int row = 0; row < positions.Length; row++)
		{
			for (int col = 0; col < positions[row].Length; col++)
			{
				PopulateOneLevelUpNeighbours(positions[row][col], positions, (uint)maxX, (uint)maxY);
			}
		}
	}

	private static void PopulateOneLevelDownNeighbours(IImmutableDictionary<Coordinates, Position2> positions)
	{
		var maxX = positions.Keys.Max(coord => coord.X);
		var maxY = positions.Keys.Max(coord => coord.Y);

		foreach (var position in positions.Values)
		{
			PopulateOneLevelDownNeighbours(position, positions, maxX, maxY);
		}
	}

	private static void PopulateOneLevelUpNeighbours(
		Position2 position,
		Position2[][] positions,
		uint maxX,
		uint maxY)
	{
		var neighbourCoordinates = position.Coordinates.GetNeighbours(maxX, maxY);
		var neighbourOneLevelDownPositions = neighbourCoordinates
			.Select(c => positions[(int)c.Y][(int)c.X])
			.Where(p => p.Height == position.Height + 1);

		position.OneLevelUpNeighbours.Clear();
		position.OneLevelUpNeighbours.AddRange(neighbourOneLevelDownPositions);
	}

	private static void PopulateOneLevelDownNeighbours(
		Position2 position,
		IImmutableDictionary<Coordinates, Position2> positions,
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