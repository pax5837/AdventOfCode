using System.Collections.Immutable;

namespace AdventOfCode2024_011;

internal static class Part1
{
	public static string Process(IImmutableList<string> lines, int blinkCount, bool withConsoleOutput = false)
	{
		var stoneMarkings = lines.First()
			.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(long.Parse)
			.ToList();

		for (int iteration = 0; iteration < blinkCount; iteration++)
		{
			Blink(stoneMarkings);
		}

		return stoneMarkings.Count.ToString();
	}

	private static void Blink(List<long> stoneMarkings)
	{
		for (int i = stoneMarkings.Count - 1; i >= 0; i--)
		{
			if (stoneMarkings[i] == 0)
			{
				stoneMarkings[i] = 1;
				continue;
			}

			var s = stoneMarkings[i].ToString();
			var length = s.Length;
			if (length % 2 == 0)
			{
				stoneMarkings[i]=int.Parse(s.Substring(0, length / 2));
				stoneMarkings.Insert(i + 1, int.Parse(s.Substring(length / 2)));
				continue;
			}

			stoneMarkings[i] = stoneMarkings[i] * 2024;
		}
	}
}