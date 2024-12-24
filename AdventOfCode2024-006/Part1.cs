using System.Collections.Immutable;
using System.Diagnostics;

namespace AdventOfCode20024_006;

internal static class Part1
{
	public static IImmutableList<Coordinates> ProcessAndReturnVisitedCoordinates(ImmutableList<string> mapLines)
	{
		var (allPositions, startingPosition) = PositionExtractor.ExtractPositions(mapLines);

		Console.WriteLine($"{startingPosition.Coordinates.X} {startingPosition.Coordinates.Y}");

		var guard = new Guard(startingPosition, Direction.North);

		var sw = Stopwatch.StartNew();
		while (guard.MoveAndReturnWhetherInMap(allPositions))
		{ }

		sw.Stop();

		Console.WriteLine($"{allPositions.Values.Count(p => p.Visited)} in {sw.ElapsedMilliseconds} ms");

		return allPositions.Values
			.Where(p => p.Visited && p.Coordinates != startingPosition.Coordinates)
			.Select(p => p.Coordinates)
			.ToImmutableList();
	}
}