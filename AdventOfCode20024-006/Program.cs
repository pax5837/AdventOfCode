using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode20024_006;

https://adventofcode.com/2024/day/6

Console.WriteLine("[e]xample data?");
var isExampleData = Console.ReadLine()!.Equals("e", StringComparison.InvariantCultureIgnoreCase);
var folder = isExampleData ? "ExampleData" : "InputData";
var mapLines = File.ReadAllLines($"./{folder}/Map.txt").ToImmutableList();

var (allPositions, currentPosition) = ExtractPositions(mapLines);

Console.WriteLine($"{currentPosition.Coordinates.X} {currentPosition.Coordinates.Y}");

(IImmutableDictionary<Coordinates, Position> allPositions, Position currentPosition) ExtractPositions(ImmutableList<string> mapLines)
{
	var positions = mapLines
		.SelectMany(ExtractPositionsFromLine)
		.ToImmutableDictionary(p => p.Coordinates, p => p);

	return (positions, positions.Values.Single(p => p.Visited));
}

var guard = new Guard(currentPosition, Direction.North);

var sw = Stopwatch.StartNew();
while (guard.MoveAndReturnWhetherInMap(allPositions))
{ }

sw.Stop();

Console.WriteLine($"{allPositions.Values.Count(p => p.Visited)} in {sw.ElapsedMilliseconds} ms");


IImmutableList<Position> ExtractPositionsFromLine(string line, int y)
{
	return line.Select((c, x) =>  ExtractPosition(c, x, y)).ToImmutableList();
}

Position ExtractPosition(char c, int x, int y)
{
	return c switch
	{
		'.' => new Position(new Coordinates(x, y), isObstacle: false, visited: false),
		'#' => new Position(new Coordinates(x, y), isObstacle: true, visited: false),
		'^' => new Position(new Coordinates(x, y), isObstacle: false, visited: true),
		_ => throw new ArgumentException(message: "Invalid position", paramName: nameof(c)),
	};
}