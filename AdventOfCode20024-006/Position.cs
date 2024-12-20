using System.Collections.Immutable;

namespace AdventOfCode20024_006;

public class Position
{
	public bool IsObstacle { get; }

	public bool Visited { get; set; }

	public int XPos { get; }

	public int YPos { get; }

	public Position(int xPos, int yPos, bool isObstacle, bool visited)
	{
		XPos = xPos;
		YPos = yPos;
		IsObstacle = isObstacle;
		Visited = visited;
	}

	public Position? Neighbour(Direction direction, IImmutableDictionary<(int XPos, int YPos), Position> allPositions)
	{
		var newCoordinates= direction switch
		{
			Direction.North => (XPos, YPos - 1),
			Direction.South => (XPos,YPos + 1),
			Direction.East => (XPos + 1, YPos),
			Direction.West => (XPos - 1, YPos),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
		};

		return allPositions.TryGetValue(newCoordinates, out var position)
			? position
			: null;
	}
}