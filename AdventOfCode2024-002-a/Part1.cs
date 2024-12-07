using System.Collections.Immutable;

namespace AdventOfCode2024_Dos_a;

internal static class Part1
{
	public static int GetNumberOfSafeReports(ImmutableList<ImmutableList<int>> input)
	{
		var numberOfSafeReports = input.Count(IsReportsSafe);

		return numberOfSafeReports;
	}

	private static bool IsReportsSafe(ImmutableList<int> numbers)
	{
		Console.Write($"{string.Join(", ", numbers)} > ");

		var allDeltas = numbers.Take(numbers.Count - 1)
			.Select((number, index) => number - numbers[index+1]);

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