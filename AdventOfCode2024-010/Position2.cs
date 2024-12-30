namespace AdventOfCode2024_010;

internal class Position2
{
	public Coordinates Coordinates { get; }

	public int Height { get; }

	public List<Coordinates> ConnectedTopCoordinates { get; } = new();

	public List<Position2> OneLevelDownNeighbours { get; } = new();

	public Position2(Coordinates coordinates, int height)
	{
		Coordinates = coordinates;
		Height = height;
	}
}