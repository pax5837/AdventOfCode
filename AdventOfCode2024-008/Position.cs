using System.Collections.Immutable;

namespace AdventOdCode2024_008;

internal class Position
{
	public Position(Coordinates coordinates, string? antennaFrequency)
	{
		Coordinates = coordinates;
		AntennaFrequency = antennaFrequency;
	}

	public Coordinates Coordinates { get; }

	public string? AntennaFrequency { get; }

	public bool IsAntinode { get; }

	public IImmutableList<Coordinates> GetAntiNodePositions(Position other, Coordinates max)
	{
		var deltaX = other.Coordinates.X - Coordinates.X;
		var deltaY = other.Coordinates.Y - Coordinates.Y;

		var antiNodeCoordinates = new List<Coordinates>();

		antiNodeCoordinates.AddCoordinatesWhenValid(
			candidateX: Coordinates.X - deltaX,
			candidateY: Coordinates.Y - deltaY,
			max: max);
		antiNodeCoordinates.AddCoordinatesWhenValid(
			candidateX: other.Coordinates.X + deltaX,
			candidateY: other.Coordinates.Y + deltaY,
			max: max);

		if (deltaX % 3 == 0 && deltaY % 3 == 0)
		{
			antiNodeCoordinates.AddCoordinatesWhenValid(
				candidateX: Coordinates.X + (deltaX / 3),
				candidateY: Coordinates.Y + (deltaY / 3),
				max: max);
			antiNodeCoordinates.AddCoordinatesWhenValid(
				candidateX: Coordinates.X + (deltaX * 2 / 3),
				candidateY: Coordinates.Y + (deltaY * 2 / 3),
				max: max);
		}

		return antiNodeCoordinates.ToImmutableList();
	}
}