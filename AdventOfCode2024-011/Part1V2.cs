using System.Collections.Immutable;
using System.Diagnostics;

namespace AdventOfCode2024_011;

internal class Part1V2
{
	public static string Process(IImmutableList<string> lines, int blinkCount, bool withConsoleOutput = false)
	{
		var stoneMarkings = new LinkedList<long>(lines.First()
			.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(long.Parse));

		for (int iteration = 0; iteration < blinkCount; iteration++)
		{
			Console.Write($"Processing, iteration {iteration}... ");
			var sw = Stopwatch.StartNew();
			Blink(stoneMarkings);
			sw.Stop();
			Console.WriteLine($"done in {sw.ElapsedMilliseconds}ms. {stoneMarkings.Count}");
		}

		return stoneMarkings.Count.ToString();
	}

	private static void Blink(LinkedList<long> stoneMarkings)
	{
		var element = stoneMarkings.Last;

		while (true)
		{
			if (element.Value == 0)
			{
				element.Value = 1;
			}
			else if (IsLengthEven(element.Value, out var s, out var length))
			{
				element.Value=int.Parse(s.Substring(0, length / 2));
				stoneMarkings.AddAfter(element, int.Parse(s.Substring(length / 2)));
			}
			else
			{
				element.Value *= 2024;
			}

			element = element.Previous;
			if (element is null)
			{
				break;
			}
		}
	}

	private static bool IsLengthEven(long value, out string s, out int length)
	{
		s = value.ToString();
		length = s.Length;
		return length % 2 == 0;
	}
}