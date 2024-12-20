namespace AdventOfCode20024_006;

public enum Direction
{
	North = 0,
	East = 1,
	South = 2,
	West = 3,
}

public static class DirectionExtensions
{
	public static Direction TurnRight(this Direction direction)
	{
		return direction switch
		{
			Direction.North => Direction.East,
			Direction.East => Direction.South,
			Direction.South => Direction.West,
			Direction.West => Direction.North,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}
}