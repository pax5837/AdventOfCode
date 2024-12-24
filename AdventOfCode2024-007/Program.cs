// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using AdventOfCode2024_007;

Console.WriteLine("[e] For example data\n[2] For real data");
var useExampleData = Console.ReadLine().Equals("e", StringComparison.OrdinalIgnoreCase);

var folder = useExampleData ? "ExampleData" : "InputData";
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var eqNumbers = EquationNumberExtractor.ExtractEquationNumbers(lines);

Processor.Process(eqNumbers, [Operators.Addition, Operators.Multiplication]);

Console.WriteLine("##############################");

Processor.Process(eqNumbers, [Operators.Addition, Operators.Multiplication, Operators.Concatenation]);