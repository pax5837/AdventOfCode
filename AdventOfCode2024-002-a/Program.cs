using System.Collections.Immutable;
using AdventOfCode2024_Dos_a;

var lines = File.ReadAllLines("InputData.txt")
	.Select(line =>
		line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(int.Parse)
			.ToImmutableList())
	.ToImmutableList();

var numberOfSafeReports = Part1.GetNumberOfSafeReports(lines);
Console.WriteLine($"\n\nPart 1, number of safe reports: {numberOfSafeReports}");

var numberOfSafeReports2 = Part2.GetNumberOfSafeReports(lines);
Console.WriteLine($"\n\nPart 2, number of safe reports: {numberOfSafeReports2}");