namespace AdventOfCode2024_009;

internal static class BlockFactory
{
	public static IEnumerable<Block> CreateBlock(char chr, int index)
	{
		var length = int.Parse(chr.ToString());

		return index % 2 == 0
			? Enumerable.Range(0, length).Select(_ => new Block(index/2))
			: Enumerable.Range(0, length).Select(_ => new Block(null));
	}

	public static IEnumerable<Blocky> CreateBlocky(char chr, int index)
	{
		var length = int.Parse(chr.ToString());

		return index % 2 == 0
			? Enumerable.Range(0, length).Select(_ => new Blocky(index/2))
			: Enumerable.Range(0, length).Select(_ => new Blocky(null));
	}
}