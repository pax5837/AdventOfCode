using System.Collections.Immutable;

namespace AdventOfCode2024_Dos_a;

internal static class Part2
{
	public static int GetNumberOfSafeReports(ImmutableList<ImmutableList<int>> input)
	{
		var numberOfSafeReports = input.Count(IsReportSafeWithToleration);

		return numberOfSafeReports;
	}

	private static bool IsReportSafeWithToleration(ImmutableList<int> levels)
	{
		if (IsReportSafe(levels))
		{
			return true;
		}

		for (int i = 0; i < levels.Count; i++)
		{
			var newLevels = levels.ToList();
			newLevels.RemoveAt(i);
			if (IsReportSafe(newLevels.ToImmutableList()))
			{
				return true;
			}
		}

		return false;
	}

	private static bool IsReportSafe(ImmutableList<int> levels)
	{
		Console.Write($"{string.Join(", ", levels)} > ");

		var allDeltas = levels.Take(levels.Count - 1)
			.Select((number, index) => number - levels[index+1]);

		if (allDeltas.Select(Math.Abs).Any(x => x > 3 || x < 1))
		{
			Console.WriteLine($"not safe as some delta either greater than 3 or less than 1");
			return false;
		}

		if (allDeltas.Select(x => x >= 0 ? 1 : -1).GroupBy(x => x).Count() > 1)
		{
			Console.WriteLine($"not safe as different signs");
			return false;
		}

		Console.WriteLine("safe");

		return true;
	}
}