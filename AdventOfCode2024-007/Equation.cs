using System.Collections.Immutable;

namespace AdventOfCode2024_007;

internal record Equation(long Result, IImmutableList<long> Operands)
{
    internal bool IsSolvable(IImmutableDictionary<int, Ops> operatorCombinationsByOperandCount)
    {
        var operatorCombinations = operatorCombinationsByOperandCount[Operands.Count].Operators;
        foreach (var operatorCombination in operatorCombinations)
        {
            if (CalculateResultWithOperators(operatorCombination))
            {
                return true;
            }
        }

        return false;
    }

    private bool CalculateResultWithOperators(IImmutableList<Operator> operatorCombination)
    {
        long result = 0;
        for (int i = 0; i < Operands.Count; i++)
        {
            switch (operatorCombination[i])
            {
                case Operator.Addition:
                    result += Operands[i];
                    break;
                case Operator.Multiplication:
                    result *= Operands[i];
                    break;
                case Operator.Concatenation:
                    result = long.Parse(result.ToString() + Operands[i]);
                    break;
            }

            if (result > Result)
            {
                break;
            }
        }

        if (result == Result)
        {
            return true;
        }

        return false;
    }
}