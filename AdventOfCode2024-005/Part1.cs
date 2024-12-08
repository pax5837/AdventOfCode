using System.Collections.Immutable;
using System.ComponentModel.Design;

namespace AdventOfCode2024_005;

internal static class Part1
{
    public static void Solve(
        IImmutableList<string> pageOrderingRulesLines,
        IImmutableList<string> pagesToProduceLines)
    {
        var precedingPagesByPage = GetPrecedingPagesByPage(pageOrderingRulesLines);
        var pagesToProduce = pagesToProduceLines
            .Select(line => line
                .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToImmutableList())
            .ToImmutableList();

        var pagesToProduceWithCorrectOrdering = pagesToProduce
            .Where(ptp => IsOrderingRespected(ptp, precedingPagesByPage))
            .ToImmutableList();

        // foreach (var x in pagesToProduceWithCorrectOrdering)
        // {
        //     Console.WriteLine(string.Join(", ", x));
        // }
        
        pagesToProduceWithCorrectOrdering
            .Select(pstp => pstp.)



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