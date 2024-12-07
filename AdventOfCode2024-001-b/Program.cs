var lines = File.ReadAllLines("InputData.txt")
	.Select(line =>
		line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(int.Parse)
			.ToList())
	.ToList();

var right = lines.Select(line => line[1]).ToList();

var similarityScore = lines.Select(line => line[0] * right.Count(r => r == line[0])).Sum();
Console.WriteLine($"Similarity Score: {similarityScore}");