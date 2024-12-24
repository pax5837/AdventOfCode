using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal static class EquationNumberExtractor
{
    public static IImmutableList<EquationNumbers> ExtractEquationNumbers(IImmutableList<string> lines)
    {
        return lines.Select(ExtractEquationNumbers).ToImmutableList();
    }

    static EquationNumbers ExtractEquationNumbers(string line)
    {
        var allNumbers = line
            .Split([' ', ':'], StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToImmutableList();
        return new EquationNumbers(allNumbers.First(), allNumbers.Skip(1).ToImmutableList());
    }
}