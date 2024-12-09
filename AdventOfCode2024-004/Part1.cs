using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2024_004;

public static class Part1
{
	private static readonly Regex xmasRegex = new Regex("XMAS", RegexOptions.Compiled);

	public static void Solve(ImmutableList<string> lines, char[,] contentArray)
	{
		var columns = GetColumns(contentArray);

		var minus45Diagonals = Get45DegDiagonals(contentArray, DiagonalDirection.Minus45Deg);
		var plus45Diagonals = Get45DegDiagonals(contentArray, DiagonalDirection.Plus45Deg);

        var xmasCount = lines.Select(CountXmas)
            .Concat(columns.Select(CountXmas))
            .Concat(minus45Diagonals.Select(CountXmas))
            .Concat(plus45Diagonals.Select(CountXmas))
            .Sum();

        Console.WriteLine(xmasCount);

    }


	private static int CountXmas(string content)
	{
		var reversedContent = string.Join("", content.Reverse().ToArray());

		var count = xmasRegex.Matches(content).Count;
		var reversedCount = xmasRegex.Matches(reversedContent).Count;
		return count + reversedCount;
	}






	private static ImmutableList<string> GetColumns(char[,] inputArray)
	{
		var result = new List<string>();

		for (int colNr = 0; colNr <= inputArray.GetUpperBound(1); colNr++)
		{
			var charList = new List<char>();

			for (int lineNr = 0; lineNr <= inputArray.GetUpperBound(0); lineNr++)
			{
				charList.Add(inputArray[lineNr, colNr]);
			}

			result.Add(string.Join("", charList));
		}

		return result.ToImmutableList();
	}

	private static ImmutableList<string> Get45DegDiagonals(char[,] inputArray, DiagonalDirection direction)
	{
		var result = new List<string>();

		for (int lineNr = 0; lineNr <= inputArray.GetUpperBound(0); lineNr++)
		{
			var charList = new List<char>();

			var li = lineNr;
			var co = 0;

			while (InArray(li, co, inputArray))
			{
				charList.Add(inputArray[li, co]);
				(li, co) = direction switch
				{
					DiagonalDirection.Minus45Deg => (li + 1, co + 1),
					DiagonalDirection.Plus45Deg => (li - 1, co + 1),
					_ => throw new ArgumentOutOfRangeException()
				};
			}

			result.Add(new string(charList.ToArray()));
		}

		for (int colNr = 1; colNr <= inputArray.GetUpperBound(1); colNr++)
		{
			var charList = new List<char>();

			var li = direction switch
			{
				DiagonalDirection.Minus45Deg => 0,
				DiagonalDirection.Plus45Deg => inputArray.GetUpperBound(0),
				_ => throw new ArgumentOutOfRangeException()
			};
			var co = colNr;

			while (InArray(li, co, inputArray))
			{
				charList.Add(inputArray[li, co]);
				(li, co) = direction switch
				{
					DiagonalDirection.Minus45Deg => (li + 1, co + 1),
					DiagonalDirection.Plus45Deg => (li - 1, co + 1),
					_ => throw new ArgumentOutOfRangeException()
				};
			}

			result.Add(new string(charList.ToArray()));
		}

		return result.ToImmutableList();
	}

	private static bool InArray(int lineIndex, int columnIndex, char[,] chars)
	{
		return lineIndex >= 0
		       && lineIndex <= chars.GetUpperBound(0)
		       && columnIndex >= 0
		       && columnIndex <= chars.GetUpperBound(1);
	}

	enum DiagonalDirection
	{
		Plus45Deg,
		Minus45Deg,
	}
}