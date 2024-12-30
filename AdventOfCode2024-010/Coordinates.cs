using System.Collections.Immutable;

namespace AdventOfCode2024_010;

internal readonly record struct Coordinates(uint X, uint Y)
{
	public IImmutableList<Coordinates> GetNeighbours(uint maxX, uint maxY)
	{
		var neighbours = new List<Coordinates>();
		if(X > 0) neighbours.Add(new Coordinates(X - 1, Y));
		if(X < maxX) neighbours.Add(new Coordinates(X + 1, Y));
		if(Y > 0) neighbours.Add(new Coordinates(X, Y - 1));
		if(Y < maxY) neighbours.Add(new Coordinates(X, Y + 1));

		return neighbours.ToImmutableList();
	}
}