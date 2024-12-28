using System.Collections.Immutable;

namespace AdventOdCode2024_008;

internal static class PositionsFactory
{
	public static IImmutableList<Position> BuildPositions(IImmutableList<string> lines)
	{
		return lines
			.SelectMany(BuildPositions)
			.ToImmutableList();
	}

	private static IImmutableList<Position> BuildPositions(string line, int rowNumber)
	{
		return line
			.Select((chr, colNumber) =>
				new Position(
					coordinates: new Coordinates(colNumber, rowNumber),
					antennaFrequency: chr == '.' ? null : chr.ToString()))
			.ToImmutableList();
	}
}