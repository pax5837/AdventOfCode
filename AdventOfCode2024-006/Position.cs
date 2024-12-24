using System.Collections.Immutable;

namespace AdventOfCode20024_006;

public class Position
{
	public bool IsObstacle { get; set; }

	public bool Visited => VisitedDirections.Any();

	public HashSet<Direction> VisitedDirections { get; } = new();

	public Coordinates Coordinates { get; }

	public Position(Coordinates coordinates, bool isObstacle, Direction? visitedDirection)
	{
		Coordinates = coordinates;
		IsObstacle = isObstacle;
		if (visitedDirection.HasValue)
		{
			VisitedDirections.Add(visitedDirection.Value);
		}
	}

	public Position? Neighbour(Direction direction, IImmutableDictionary<Coordinates, Position> allPositions)
	{
		return allPositions.TryGetValue(Coordinates.GetNeighboringCoordinates(direction), out var position)
			? position
			: null;
	}
}