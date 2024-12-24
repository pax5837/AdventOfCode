using System.Collections.Immutable;

namespace AdventOfCode20024_006;

internal class Guard
{
	public Position? CurrentPosition { get; private set; }

	public Direction CurrentDirection { get; private set; }

	public Guard(Position currentPosition, Direction currentDirection)
	{
		CurrentPosition = currentPosition;
		CurrentDirection = currentDirection;
	}

	public bool MoveAndReturnWhetherInMap(IImmutableDictionary<Coordinates, Position> allPositions)
	{
		if (CurrentPosition is null)
		{
			throw new InvalidOperationException("Does not have a current position.");
		}

		var nextPosition = CurrentPosition!.Neighbour(CurrentDirection, allPositions);

		if (nextPosition is null)
		{
			CurrentPosition = null;
			return false;
		}

		if (nextPosition.IsObstacle)
		{
			CurrentDirection = CurrentDirection.TurnRight();
			return MoveAndReturnWhetherInMap(allPositions);
		}

		CurrentPosition = nextPosition;
		CurrentPosition.VisitedDirections.Add(CurrentDirection);
		return true;
	}

	public MovementResult Move(IImmutableDictionary<Coordinates, Position> allPositions)
	{
		if (CurrentPosition is null)
		{
			throw new InvalidOperationException("Does not have a current position.");
		}

		var nextPosition = CurrentPosition!.Neighbour(CurrentDirection, allPositions);

		if (nextPosition is null)
		{
			CurrentPosition = null;
			return MovementResult.OutsideMap;
		}

		if (nextPosition.IsObstacle)
		{
			CurrentDirection = CurrentDirection.TurnRight();
			return Move(allPositions);
		}

		CurrentPosition = nextPosition;

		if (!CurrentPosition.VisitedDirections.Add(CurrentDirection))
		{
			return MovementResult.InLoop;
		}

		return MovementResult.InsideMap;
	}
}

public enum MovementResult
{
	InsideMap,
	OutsideMap,
	InLoop,
}