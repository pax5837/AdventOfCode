using System.Collections.Immutable;

namespace AdventOfCode2024_005;

internal static class Part1
{
    public static void Solve(
        IImmutableList<string> pageOrderingRulesLines,
        IImmutableList<string> pagesToProduceLines)
    {
        var precedingPagesByPage = GetPrecedingPagesByPage(pageOrderingRulesLines);

        var sumOfMiddlePages = pagesToProduceLines
            .Select(ExtractIntegers)
            .Where(ptp => IsOrderingRespected(ptp, precedingPagesByPage))
            .Select(pstp => pstp[pstp.Count / 2])
            .Sum();

        Console.WriteLine(sumOfMiddlePages);
    }

    private static ImmutableList<int> ExtractIntegers(string line)
    {
        return line
            .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToImmutableList();
    }

    private static bool IsOrderingRespected(ImmutableList<int> pagesToProduce, ILookup<int, int> precedingPagesByPage)
    {
        return pagesToProduce
            .All(ptp => IsOrderingRespected(ptp, pagesToProduce,  precedingPagesByPage));
    }

    private static bool IsOrderingRespected(int currentPage, ImmutableList<int> pagesToProduce, ILookup<int, int> precedingPagesByPage)
    {
        var indexOfCurrent = pagesToProduce.IndexOf(currentPage);
        var precedingPages = precedingPagesByPage[currentPage].ToImmutableHashSet();
        return pagesToProduce
            .Select((ptp, idx) => (ptp, idx))
            .All(pair => !precedingPages.Contains(pair.ptp) || pair.idx < indexOfCurrent);

    }


    private static ILookup<int, int> GetPrecedingPagesByPage(IImmutableList<string> pageOrderingRulesLines)
    {
        var precedingPagesByPage = pageOrderingRulesLines
            .Select(line => line.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .Select(x => (mustComeBefore: int.Parse(x[0]), refPage: int.Parse(x[1])))
            .ToLookup(x => x.refPage, x => x.mustComeBefore);

        // foreach (var bla in precedingPagesByPage)
        // {
        //     Console.WriteLine($"{bla.Key} preceded by {string.Join(", ", bla.Select(y => y))}");
        // }

        return precedingPagesByPage;
    }
}