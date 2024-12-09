// https://adventofcode.com/2024/day/3

using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024_003;

var code = File.ReadAllText("InputData.txt");



ExecuteWithMeasurement( () => ProcessPart1(code));
ExecuteWithMeasurement( () => ProcessPart1V2(code));
ExecuteWithMeasurement( () => ProcessPart2(code));
ExecuteWithMeasurement( () => ProcessPart2V2(code));



void ExecuteWithMeasurement(Action action)
{
	var sw = Stopwatch.StartNew();
	action();
	sw.Stop();
	Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
}

void ProcessPart1(string s)
{
	var regex = new Regex("mul[(][0-9]+[,][0-9]+[)]", RegexOptions.Compiled);

	var res = regex.Matches(s)
		.Select(x => x.Value.Replace("mul(", string.Empty).Replace(")", string.Empty).Split(',').Select(int.Parse).ToList())
		.Select(tuple => tuple[0] * tuple[1])
		.Sum();

	Console.WriteLine(res);
}

void ProcessPart1V2(string s)
{
	var regex = new Regex(@"mul\((?<a>\d+),(?<b>\d+)\)", RegexOptions.Compiled);

	var matches = regex.Matches(s);
	var res = matches
		.Select(x => int.Parse(x.Groups["a"].Value) * int.Parse(x.Groups["b"].Value))
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

void ProcessPart2V2(string s)
{
	var mulRegex = new Regex(@"(mul\((?<a>\d+),(?<b>\d+)\))|(?<dont>don't\(\))|(?<do>do\(\))", RegexOptions.Compiled);
	var mulMatches = mulRegex.Matches(s).ToList();

	var res = mulMatches
		.Select(selector: match => match switch
		{
			_ when match.Groups["dont"].Success => Instruction.Deactivate(),
			_ when match.Groups["do"].Success => Instruction.Activate(),
			_ => Instruction.Multiply(int.Parse(match.Groups["a"].Value), (int.Parse(match.Groups["b"].Value))),
		})
		.Aggregate<Instruction, (bool active, int total), int>(
			seed: (true, 0),
			func: (accumulator, instruction) =>
				instruction.Switch(
					whenActivate: () => (true, accumulator.total),
					whenInactivate: () => (false, accumulator.total),
					whenMultiply: (a, b) => accumulator.active ? (true, accumulator.total + a * b) : (false, accumulator.total)),
			resultSelector: x => x.total);

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