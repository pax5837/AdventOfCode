// https://adventofcode.com/2024/day/3

using System.Text.RegularExpressions;

var code = File.ReadAllText("InputData.txt");



ProcessPart1(code);
Console.WriteLine("\n\n");
ProcessPart2(code);



void ProcessPart1(string s)
{
	var regex = new Regex("mul[(][0-9]+[,][0-9]+[)]", RegexOptions.Compiled);

	var res = regex.Matches(s)
		.Select(x => x.Value.Replace("mul(", string.Empty).Replace(")", string.Empty).Split(',').Select(int.Parse).ToList())
		.Select(tuple => tuple[0] * tuple[1])
		.Sum();

	Console.WriteLine(res);
}

void ProcessPart2(string s)
{
	var mulRegex = new Regex("mul[(][0-9]+[,][0-9]+[)]", RegexOptions.Compiled);
	var mulMatches = mulRegex.Matches(s).ToList();

	var doRegex = new Regex("do[(][)]", RegexOptions.Compiled);
	var doMatches = doRegex.Matches(s);
	var doIndices = doMatches
		.Select(x => x.Index)
		.Prepend(0) // code starts implicitly with a do.
		.ToList();

	var dontRegex = new Regex("don[']t[(][)]", RegexOptions.Compiled);
	var dontMatches = dontRegex.Matches(s).ToList();
	var dontIndices = dontMatches.Select(x => x.Index).ToList();

	var res = mulMatches
		.Select(x => EvaluateMulMatch(x, doIndices, dontIndices))
		.Sum();

	Console.WriteLine(res);
}

int EvaluateMulMatch(Match match, List<int> doIndices, List<int> dontIndices)
{
	var currentIndex = match.Index;

	var lastDoIndex = doIndices.Any(idx => idx < currentIndex)
		? doIndices.Where(idx => idx < currentIndex).Max()
		: int.MinValue;
	var lastDontIndex = dontIndices.Any(idx => idx < currentIndex)
		? dontIndices.Where(idx => idx < currentIndex).Max()
		: int.MinValue;

	var doInclude = lastDoIndex > lastDontIndex;

	if (!doInclude)
	{
		return 0;
	}

	var numbers = match.Value.Replace("mul(", string.Empty).Replace(")", string.Empty).Split(',').Select(int.Parse).ToList();

	return numbers[0] * numbers[1];
}