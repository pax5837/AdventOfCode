var lines = File.ReadAllLines("InputData.txt")
	.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList())
	.ToList();
var list1 = lines.Select(line => line[0]).OrderBy(x => x).ToList();
var list2 = lines.Select(line => line[1]).OrderBy(x => x).ToList();
var distancesSum = list1.Select((x, idx) => Math.Abs(list2[idx] - x)).Sum();
Console.WriteLine(distancesSum);