namespace AdventOfCode2024_009;

public class Blocky
{
	public int? FileId { get; }

	public Blocky(int? fileId)
	{
		FileId = fileId;
	}

	public bool IsEmptyBlock => FileId is null;
	public bool IsFileBlock => FileId is not null;

	public override string ToString()
	{
		return FileId is not null ? $"{FileId}" : ".";
	}
}