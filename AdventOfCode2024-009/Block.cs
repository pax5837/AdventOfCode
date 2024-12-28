namespace AdventOfCode2024_009;

public record Block(int? FileId)
{
	public bool IsEmptyBlock => FileId is null;
	public bool IsFileBlock => FileId is not null;

	public override string ToString()
	{
		return FileId is not null ? $"[{FileId}]" : ".";
	}
}