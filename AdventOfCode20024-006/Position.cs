using System.Collections.Immutable;

namespace AdventOfCode20024_006;

public class Position
{
	public bool IsObstacle { get; }

	public bool Visited { get; set; }

	public Coordinates Coordinates { get; }

	public Position(Coordinates coordinates, bool isObstacle, bool visited)
	{
		Coordinates = coordinates;
		IsObstacle = isObstacle;
		Visited = visited;
	}

	public Position? Neighbour(Direction direction, IImmutableDictionary<Coordinates, Position> allPositions)
	{
		return allPositions.TryGetValue(Coordinates.GetNeighboringCoordinates(direction), out var position)
			? position
			: null;
	}
}