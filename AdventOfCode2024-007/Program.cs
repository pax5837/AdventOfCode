// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using System.Diagnostics;
using AdventOfCode2024_007;

Console.WriteLine("[e] For example data\n[2] For real data");
var useExampleData = Console.ReadLine().Equals("e", StringComparison.OrdinalIgnoreCase);

var folder = useExampleData ? "ExampleData" : "InputData";
var lines = File.ReadAllLines($"./{folder}/input.txt").ToImmutableList();

var eqNumbers = EquationNumberExtractor.ExtractEquationNumbers(lines);

Processor.Process(eqNumbers, [Operator.Addition, Operator.Multiplication]);

// Execute("proc 1 part 2",
//     () => Processor.Process(eqNumbers, [Operator.Addition, Operator.Multiplication, Operator.Concatenation]));


Execute(
        "proc2 part2, expected 362646859298554", 
        () => Processor2.Process(eqNumbers, [Operator.Addition, Operator.Multiplication, Operator.Concatenation]));

void Execute(string name, Action action)
{
    Console.WriteLine("##############################");
    Console.Write($"[{name}]");
    var sw = Stopwatch.StartNew();
    action();
    sw.Stop();
    Console.WriteLine($" in {sw.ElapsedMilliseconds} ms");
}