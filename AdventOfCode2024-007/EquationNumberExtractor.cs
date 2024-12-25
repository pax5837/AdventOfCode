using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal static class EquationNumberExtractor
{
    public static IImmutableList<Equation> ExtractEquationNumbers(IImmutableList<string> lines)
    {
        return lines.Select(ExtractEquationNumbers).ToImmutableList();
    }

    static Equation ExtractEquationNumbers(string line)
    {
        var allNumbers = line
            .Split([' ', ':'], StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToImmutableList();
        return new Equation(allNumbers.First(), allNumbers.Skip(1).ToImmutableList());
    }
}