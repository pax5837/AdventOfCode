namespace AdventOfCode2024_010;

internal class Position
{
	public Coordinates Coordinates { get; }

	public int Height { get; }

	public HashSet<Coordinates> ConnectedTopCoordinates { get; } = new();

	public List<Position> OneLevelDownNeighbours { get; } = new();

	public Position(Coordinates coordinates, int height)
	{
		Coordinates = coordinates;
		Height = height;
	}
}