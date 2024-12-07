// https://adventofcode.com/2024/day/4

using System.Collections.Immutable;
using System.Text.RegularExpressions;

Regex xmasRegex = new Regex("XMAS", RegexOptions.Compiled);

var lines = File.ReadAllLines("InputDataExampleSmall.txt").ToImmutableList();

var contentArray = To2DArray(lines);

var columns = GetColumns(contentArray);

var minus45Diagonals = GetMinus45DegDiagonals(contentArray);

var xmasCount = lines.Select(CountXmas)
	.Concat(columns.Select(CountXmas))
	.Concat(minus45Diagonals.Select(CountXmas))
	.Sum();

Console.WriteLine(xmasCount);


int CountXmas(string content)
{
	var reversedContent = string.Join("", content.Reverse().ToArray());

	var count = xmasRegex.Matches(content).Count;
	var reversedCount = xmasRegex.Matches(reversedContent).Count;
	return count + reversedCount;
}



char[,] To2DArray(ImmutableList<string> input)
{
	var arr = new char[input.Count, input.First().Length];

	foreach (var (line, lineNr) in input.Select((line, lineNr) => (line, lineNr)))
	{
		foreach (var (c, colNr) in line.Select((c, colNr) => (c, colNr)))
		{
			arr[lineNr, colNr] = c;
		}
	}

	return arr;
}


ImmutableList<string> GetColumns(char[,] inputArray)
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

ImmutableList<string> GetMinus45DegDiagonals(char[,] inputArray)
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
			li++;
			co++;
		}

		result.Add(new string(charList.ToArray()));
	}

	for (int colNr = 1; colNr <= inputArray.GetUpperBound(1); colNr++)
	{
		var charList = new List<char>();

		var li = 0;
		var co = colNr;

		while (InArray(li, co, inputArray))
		{
			charList.Add(inputArray[li, co]);
			li++;
			co++;
		}

		result.Add(new string(charList.ToArray()));
	}

	return result.ToImmutableList();
}

bool InArray(int lineIndex, int columnIndex, char[,] chars)
{
	return lineIndex >= 0
	       && lineIndex <= chars.GetUpperBound(0)
	       && columnIndex >= 0
	       && columnIndex <= chars.GetUpperBound(1);
}