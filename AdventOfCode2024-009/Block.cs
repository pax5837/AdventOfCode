namespace AdventOfCode2024_009;

public record Block(int? Id)
{
	public override string ToString()
	{
		return Id is not null ? $"[{Id}]" : ".";
	}
}