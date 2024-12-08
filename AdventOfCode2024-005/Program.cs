// https://adventofcode.com/2024/day/5


using System.Collections.Immutable;
using AdventOfCode2024_005;

var folder = "InputData";
var pageOrderingRulesLines = File.ReadAllLines($"./{folder}/PageOrderingRules.txt").ToImmutableList();
var pagesToProduceLines = File.ReadAllLines($"./{folder}/PagesToProduce.txt").ToImmutableList();

Part1.Solve(pageOrderingRulesLines, pagesToProduceLines);