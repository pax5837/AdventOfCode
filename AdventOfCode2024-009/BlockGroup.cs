namespace AdventOfCode2024_009;

internal class BlockGroup
{
	public List<Block> Blocks { get; }

	public bool IsFileBlockGroup => Blocks.First().IsFileBlock;

	public bool IsEmptyBlockGroup => Blocks.First().IsEmptyBlock;

	public int Size => Blocks.Count;

	public BlockGroup(List<Block> blocks)
	{
		Blocks = blocks;
	}

	public static BlockGroup NewEmpty(int count)
	{
		return new BlockGroup(Enumerable.Range(0, count).Select(i => new Block(null)).ToList());
	}

	public override string ToString()
	{
		return string.Join("", Blocks.Select(b => b.FileId?.ToString() ?? "."));
	}
}