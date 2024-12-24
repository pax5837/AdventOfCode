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

        long result = 0;
        for (int i = 0; i < eqn.Operands.Count(); i++)
        {
            switch (oc2[i])
            {
                case Operators.Addition:
                    result += eqn.Operands[i];
                    break;
                case Operators.Multiplication:
                    result *= eqn.Operands[i];
                    break;
                case Operators.Concatenation:
                    var rank = eqn.Operands[i].ToString().Length;
                    result = (result * (long)Math.Pow(10, rank)) + eqn.Operands[i];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var isValid = result == eqn.Result;
        
   
        
        return isValid;
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