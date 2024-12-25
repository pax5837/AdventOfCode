using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal static class Processor
{
    public static void Process(
        IImmutableList<Equation> equations,
        IImmutableList<Operator> possibleOperators)
    {
        var result = equations
            .Where(x => IsValid1(x, possibleOperators))
            .Sum(eqn => eqn.Result);
        
        Console.WriteLine($"\n\n{result}");
    }

    private static bool IsValid1(
        Equation eqn,
        IImmutableList<Operator> possibleOperators)
    {
        // Console.WriteLine($"\n{eqn.Result}: {string.Join(" ", eqn.Operands)}");
        
        IImmutableList<IImmutableList<Operator>> operators = Enumerable
            .Range(0, eqn.Operands.Count - 1)
            .Select(_ => possibleOperators.ToImmutableList() as IImmutableList<Operator>)
            .ToImmutableList();
        
        var operatorCombinations = CombineCollections(operators);

        // foreach (var combination in operatorCombinations)
        // {
        //     Console.WriteLine(string.Join("; ", combination.Select(x => ((Operators)x).ToString())));
        // }
        
        var isValid =  operatorCombinations
            .Any(oc => IsValid(eqn, oc));
        
        // Console.WriteLine($"{eqn.Result}: {string.Join(" ", eqn.Operands)} {isValid}");
        return isValid;

    }

    private static bool IsValid(Equation eqn, IImmutableList<Operator> oc)
    {
        var oc2 = oc.Prepend(Operator.Addition).ToImmutableList();

        var res = eqn.Operands
            .Select((operand, index) => (operand, oc2[index]))
            .Aggregate(
                seed: 0l,
                func: (acc, pair) => Acc(acc, pair.operand, pair.Item2));
        
        return res == eqn.Result;
    }

    private static long Acc(long previous, long operand, Operator op)
    {
        return op switch
        {
            Operator.Addition => previous + operand,
            Operator.Multiplication => previous * operand,
            Operator.Concatenation => previous * (long)Math.Pow(10, operand.ToString().Length) + operand,
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
    }

    static IImmutableList<IImmutableList<Operator>> CombineCollections(IImmutableList<IImmutableList<Operator>> collections)
    {
        IImmutableList<IImmutableList<Operator>> seed = [[]];
        
        return collections.Aggregate(
            seed: seed, // Seed with an empty combination
            func: Aggregate
        ).ToImmutableList();
    }

    private static IImmutableList<IImmutableList<Operator>> Aggregate(
        IImmutableList<IImmutableList<Operator>> current,
        IImmutableList<Operator> operators)
    {
        return current
            .SelectMany(
                collectionSelector: _ => operators,
                resultSelector: (c, item) => c.Append(element: item).ToImmutableList() as IImmutableList<Operator>)
            .ToImmutableList();
    }
}