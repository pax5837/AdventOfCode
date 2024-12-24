using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal static class Processor
{
    public static void Process(
        IImmutableList<EquationNumbers> equationNumbers,
        IImmutableList<Operators> possibleOperators)
    {
        var result = equationNumbers
            .Where(x => IsValid1(x, possibleOperators))
            .Sum(eqn => eqn.Result);
        
        Console.WriteLine($"\n\n{result}");
    }

    private static bool IsValid1(
        EquationNumbers eqn,
        IImmutableList<Operators> possibleOperators)
    {
        // Console.WriteLine($"\n{eqn.Result}: {string.Join(" ", eqn.Operands)}");
        
        IImmutableList<IImmutableList<Operators>> operators = Enumerable
            .Range(0, eqn.Operands.Count - 1)
            .Select(_ => possibleOperators.ToImmutableList() as IImmutableList<Operators>)
            .ToImmutableList();
        
        var operatorCombinations = CombineCollections(operators);

        // foreach (var combination in operatorCombinations)
        // {
        //     Console.WriteLine(string.Join("; ", combination.Select(x => ((Operators)x).ToString())));
        // }
        
        var isValid =  operatorCombinations
            .Any(oc => IsValid(eqn, oc));
        
        Console.WriteLine($"{eqn.Result}: {string.Join(" ", eqn.Operands)} {isValid}");
        return isValid;

    }

    private static bool IsValid(EquationNumbers eqn, IImmutableList<Operators> oc)
    {
        var oc2 = oc.Prepend(Operators.Addition).ToImmutableList();

        var res = eqn.Operands
            .Select((operand, index) => (operand, oc2[index]))
            .Aggregate(
                seed: 0l,
                func: (acc, pair) => Acc(acc, pair.operand, pair.Item2));
        
        return res == eqn.Result;
    }

    private static long Acc(long previous, long operand, Operators op)
    {
        return op switch
        {
            Operators.Addition => previous + operand,
            Operators.Multiplication => previous * operand,
            Operators.Concatenation => previous * (long)Math.Pow(10, operand.ToString().Length) + operand,
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
    }

    static IImmutableList<IImmutableList<Operators>> CombineCollections(IImmutableList<IImmutableList<Operators>> collections)
    {
        IImmutableList<IImmutableList<Operators>> seed = [[]];
        
        return collections.Aggregate(
            seed: seed, // Seed with an empty combination
            func: Aggregate
        ).ToImmutableList();
    }

    private static IImmutableList<IImmutableList<Operators>> Aggregate(
        IImmutableList<IImmutableList<Operators>> current,
        IImmutableList<Operators> operators)
    {
        return current
            .SelectMany(
                collectionSelector: _ => operators,
                resultSelector: (c, item) => c.Append(element: item).ToImmutableList() as IImmutableList<Operators>)
            .ToImmutableList();
    }
}