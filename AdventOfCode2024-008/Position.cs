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

	public IImmutableList<Coordinates> Get2To1AntiNodePositions(Position other, Coordinates max)
	{
		var deltaX = other.Coordinates.X - Coordinates.X;
		var deltaY = other.Coordinates.Y - Coordinates.Y;

		var antiNodeCoordinates = new List<Coordinates>();

		antiNodeCoordinates.TryAddCoordinates(
			candidateX: Coordinates.X - deltaX,
			candidateY: Coordinates.Y - deltaY,
			max: max);
		antiNodeCoordinates.TryAddCoordinates(
			candidateX: other.Coordinates.X + deltaX,
			candidateY: other.Coordinates.Y + deltaY,
			max: max);

		if (deltaX % 3 == 0 && deltaY % 3 == 0)
		{
			antiNodeCoordinates.TryAddCoordinates(
				candidateX: Coordinates.X + (deltaX / 3),
				candidateY: Coordinates.Y + (deltaY / 3),
				max: max);
			antiNodeCoordinates.TryAddCoordinates(
				candidateX: Coordinates.X + (deltaX * 2 / 3),
				candidateY: Coordinates.Y + (deltaY * 2 / 3),
				max: max);
		}

		return antiNodeCoordinates.ToImmutableList();
	}

	public IImmutableList<Coordinates> GetInlineAntiNodePositions(Position other, Coordinates max)
	{
		var deltaX = other.Coordinates.X - Coordinates.X;
		var deltaY = other.Coordinates.Y - Coordinates.Y;

		var antiNodeCoordinates = new List<Coordinates>();

		antiNodeCoordinates.TryAddCoordinates(Coordinates.X, Coordinates.Y, max);

		var addMinusCoordinates = true;
		var minusIncrement = 0;
		while (addMinusCoordinates)
		{
			minusIncrement--;
			var added = antiNodeCoordinates.TryAddCoordinates(
				Coordinates.X + minusIncrement * deltaX,
				Coordinates.Y + minusIncrement * deltaY,
				max);
			addMinusCoordinates = added;
		}

		var addPlusCoordinates = true;
		var plusIncrement = 0;
		while (addPlusCoordinates)
		{
			plusIncrement++;
			var added = antiNodeCoordinates.TryAddCoordinates(
				Coordinates.X + plusIncrement * deltaX,
				Coordinates.Y + plusIncrement * deltaY,
				max);
			addPlusCoordinates = added;
		}

		return antiNodeCoordinates.ToImmutableList();
	}
}