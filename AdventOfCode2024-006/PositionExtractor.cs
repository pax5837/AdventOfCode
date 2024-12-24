using System.Collections.Immutable;

namespace AdventOfCode20024_006;

internal class PositionExtractor
{
	public static (IImmutableDictionary<Coordinates, Position> allPositions, Position currentPosition) ExtractPositions(ImmutableList<string> mapLines)
	{
		var positions = mapLines
			.SelectMany(ExtractPositionsFromLine)
			.ToImmutableDictionary(p => p.Coordinates, p => p);

		return (positions, positions.Values.Single(p => p.Visited));
	}

	private static IImmutableList<Position> ExtractPositionsFromLine(string line, int y)
	{
		return line.Select((c, x) =>  ExtractPosition(c, x, y)).ToImmutableList();
	}

	private static Position ExtractPosition(char c, int x, int y)
	{
		return c switch
		{
			'.' => new Position(new Coordinates(x, y), isObstacle: false, visitedDirection: null),
			'#' => new Position(new Coordinates(x, y), isObstacle: true, visitedDirection: null),
			'^' => new Position(new Coordinates(x, y), isObstacle: false, visitedDirection: Direction.North),
			_ => throw new ArgumentException(message: "Invalid position", paramName: nameof(c)),
		};
	}
}