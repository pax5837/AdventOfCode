namespace AdventOfCode20024_006;

public record Coordinates(int X, int Y)
{
	public Coordinates GetNeighboringCoordinates(Direction direction)
	{
		return direction switch
		{
			Direction.North => new Coordinates(X, Y - 1),
			Direction.South => new Coordinates(X, Y + 1),
			Direction.East => new Coordinates(X + 1, Y),
			Direction.West => new Coordinates(X - 1, Y),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
		};
	}
}