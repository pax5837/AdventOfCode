namespace AdventOfCode2024_003;

public record Instruction
{
	private Instruction(InstructionType type, int? valueA, int? valueB)
	{
		Type = type;
		ValueA = valueA;
		ValueB = valueB;
	}

	private InstructionType Type { get; init; }
	private int? ValueA { get; init; }
	private int? ValueB { get; init; }

	public static Instruction Deactivate() => new Instruction(InstructionType.Inactivate, null, null);
	public static Instruction Activate() => new Instruction(InstructionType.Activate, null, null);
	public static Instruction Multiply(int a, int b) => new Instruction(InstructionType.Multiply, a, b);

	public T Switch<T>(
		Func<T> whenActivate,
		Func<T> whenInactivate,
		Func<int, int, T> whenMultiply)
	{
		return Type switch
		{
			InstructionType.Inactivate => whenInactivate(),
			InstructionType.Activate => whenActivate(),
			InstructionType.Multiply => whenMultiply(ValueA.Value, ValueB.Value),
			_ => throw new InvalidOperationException()
		};
	}

}

public enum InstructionType
{
	Inactivate,
	Activate,
	Multiply,
}