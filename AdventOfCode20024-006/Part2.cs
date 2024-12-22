using System.Collections.Immutable;

namespace AdventOfCode20024_006;

internal static class Part2
{
	public static void Process(ImmutableList<string> mapLines, IImmutableList<Coordinates> VisitedCoordinatesPart1)
	{
		var (allInitialPositions, startingPosition) = PositionExtractor.ExtractPositions(mapLines);

		var count = VisitedCoordinatesPart1
			.Select((coordinates, index) => PerformTestForOneModifiedPositionAndReturnWhetherLoop(
				oneBasedIndex: index + 1,
				allPositions: CloneAllPositions(allInitialPositions),
				coordinates: coordinates,
				startingPosition: startingPosition))
			.Count(isLoop => isLoop);

		Console.WriteLine($"LoopCount: {count}");
	}

	private static ImmutableDictionary<Coordinates, Position> CloneAllPositions(IImmutableDictionary<Coordinates, Position> allInitialPositions)
	{
		return allInitialPositions.Values
			.Select(p => new Position(p.Coordinates, p.IsObstacle, p.Visited ? p.VisitedDirections.Single() : null))
			.ToImmutableDictionary(p => p.Coordinates, p => p);
	}

	private static bool PerformTestForOneModifiedPositionAndReturnWhetherLoop(int oneBasedIndex,
		IImmutableDictionary<Coordinates, Position> allPositions,
		Coordinates coordinates,
		Position startingPosition)
	{
		if (oneBasedIndex % 100 == 0)
		{
			Console.WriteLine($"Indey: {oneBasedIndex}");
		}

		var posToModify = allPositions[coordinates];
		posToModify.IsObstacle = true;

		var guard = new Guard(startingPosition, Direction.North);

		var done = false;
		while (!done)
		{
			var movementResult = guard.Move(allPositions);

			if (movementResult == MovementResult.InsideMap)
			{
				continue;
			}

			if (movementResult == MovementResult.InLoop)
			{
				return true;
			}

			if (movementResult == MovementResult.OutsideMap)
			{
				return false;
			}
		}

		return false;
	}

	private static (ImmutableList<Position> allPositions,  ImmutableList<Coordinates> allCoordinatesToBeEdited, Position startingPosition) GetAllPositionsToModify(ImmutableList<string> mapLines)
	{
		var (allPos, startingPosition) = PositionExtractor.ExtractPositions(mapLines);
		var allFreePositions = allPos.Values.Where(p => !p.Visited && !p.IsObstacle).Select(p => p.Coordinates).ToImmutableList();
		return (allPos.Values.ToImmutableList(), allFreePositions, startingPosition);
	}
}