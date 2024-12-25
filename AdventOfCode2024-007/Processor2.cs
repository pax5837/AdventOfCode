using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal static class Processor2
{
    public static void Process(
        IImmutableList<Equation> equations,
        IImmutableList<Operator> possibleOperators)
    {
        var operatorCombinationsByOperandCount 
            = GetOperatorCombinationsByOperandCount(possibleOperators, equations.Max(x => x.Operands.Count));
        
        var tasks = equations
            .Select(eqn => Calculate(eqn, operatorCombinationsByOperandCount));
        
        var r = Task.WhenAll(tasks).GetAwaiter().GetResult();

        var result = r.Sum();
        
        // var result = equations
        //     .Where(x => x.IsSolvable(operatorCombinationsByOperandCount))
        //     .Sum(eqn => eqn.Result);
        
        Console.WriteLine($"\n\nxx {result}");
    }

    private static Task<long> Calculate(Equation eqn, IImmutableDictionary<int, Ops> ops)
    {
        return Task.Factory.StartNew(() => eqn.IsSolvable(ops) ? eqn.Result : 0);
    }
    
    private static IImmutableDictionary<int, Ops>
        GetOperatorCombinationsByOperandCount(
        IImmutableList<Operator> operators,
        int maxOperandCount)
    {
        return Enumerable
            .Range(1, maxOperandCount)
            .Select(opCount => GetOperatorCombinations(operators, opCount))
            .ToImmutableDictionary(
                g => g.OperandCount,
                g => g);
    }
    
    private static Ops
        GetOperatorCombinations(
            IImmutableList<Operator> operators,
            int operandCount)
    {
           var opBase = operators.Count;
           var combinationCount = (int)Math.Pow(opBase, operandCount - 1);

           var result = new List<List<Operator>>();
           
           for (int i = 0; i < combinationCount; i++)
           {
               var operatorList = new List<Operator> { Operator.Addition };

               for (int j = 0; j < operandCount - 1; j++)
               {
                   var index = ((int)Math.Floor(i / Math.Pow(opBase, j))) % opBase;
                   operatorList.Add(operators[index]);
               }
               
               result.Add(operatorList);
           }
       
           return new Ops(
               operandCount, 
               result
                   .Select(x => x.ToImmutableList() as IImmutableList<Operator>)
                   .ToImmutableList());
    }
}

internal record Ops(int OperandCount, IImmutableList<IImmutableList<Operator>> Operators);