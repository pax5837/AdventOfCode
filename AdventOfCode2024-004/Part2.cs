using System.Collections.Immutable;
using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode2024_004;

public static  class Part2
{
    private const string XMas = "MAS";
    private const string XMasReversed = "SAM";

    public static void Solve(ImmutableList<string> lines, char[,] contentArray)
    {
        var count = Enumerable.Range(0, contentArray.GetUpperBound(0) - 1)
            .SelectMany(li => 
                Enumerable.Range(0, contentArray.GetUpperBound(1) - 1).Select(co => (li, co)))
            .Select(x =>
            {
                var box = BuildBox(contentArray, x.li, x.co);
                return HasXmas(box);
            })
            .Count(x => x);
        Console.WriteLine(count);
    }

    private static char[,] BuildBox(char[,] contentArray, int lineNr, int colNr)
    {
        char[,] box = new char[3, 3];
        for (int boxRow = 0; boxRow <= box.GetUpperBound(0); boxRow++)
        {
            for (int boxCol = 0; boxCol <= box.GetUpperBound(1); boxCol++)
            {
                box[boxRow, boxCol] = contentArray[boxRow + lineNr, boxCol + colNr];
            }
        }

        return box;
    }

    private static bool HasXmas(char[,] box)
    {
        char[] diag1 = [box[0, 0], box[1, 1], box[2, 2]];
        char[] diag2 = [box[0, 2], box[1, 1], box[2, 0]];
        
        var diag1String = new string(diag1);
        var diag2String = new string(diag2);
        var diag1ContainsMas = diag1String.Equals(XMas) || diag1String.Equals(XMasReversed);
        var diag2ContainsMas = diag2String.Equals(XMas) || diag2String.Equals(XMasReversed);
        var result = diag1ContainsMas && diag2ContainsMas;
        return result;
    }
}