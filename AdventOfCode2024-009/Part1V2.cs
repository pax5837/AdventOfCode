using System.Collections.Immutable;

namespace AdventOfCode2024_009;

internal static class Part1V2
{
	public static string Process(string line, bool withConsoleOutput = false)
	{
		var blocks = line
			.ToCharArray()
			.SelectMany(CreateBlock)
			.ToImmutableList();

		if (withConsoleOutput)
		{
			foreach (var block in blocks) Console.Write(block);
			Console.WriteLine();
		}

		var candidatesForDisplacement = new Queue<Block>(
			blocks
				.Where(b => b.IsFileBlock) // Empty blocks are not candidates for displacement
				.Reverse()
				.Take(blocks.Count(b => b.IsEmptyBlock))); // We can only replace so many empty blocks

		var totalFileBlocks = blocks.Count(b => b.IsFileBlock);

		var result = blocks.Aggregate(
			seed: new Blocks(),
			func: (blocks, block) => SortBlock(blocks, block, candidatesForDisplacement, totalFileBlocks),
			resultSelector: blocks => blocks.FileBlocks);

		return result
			.Select((b, index) => (long)index * (long)b.FileId!.Value)
			.Sum()
			.ToString();
	}

	private static Blocks SortBlock(
		Blocks blocks,
		Block block,
		Queue<Block> candidatesForDisplacement,
		int totalFileBlocks)
	{
		if (block.IsEmptyBlock)
		{
			blocks.EmptyBlocks.Add(block);
			if (candidatesForDisplacement.TryDequeue(out var candidate) && blocks.FileBlocks.Count < totalFileBlocks)
			{
				blocks.FileBlocks.Add(candidate);
			}
		}
		else if (blocks.FileBlocks.Count < totalFileBlocks)
		{
			blocks.FileBlocks.Add(block);
		}

		return blocks;
	}


	private static IEnumerable<Block> CreateBlock(char chr, int index)
	{
		var length = int.Parse(chr.ToString());

		return index % 2 == 0
			? Enumerable.Range(0, length).Select(_ => new Block(index/2))
			: Enumerable.Range(0, length).Select(_ => new Block(null));
	}

	private class Blocks
	{
		public List<Block> FileBlocks { get; } = new List<Block>();
		public List<Block> EmptyBlocks { get; } = new List<Block>();
	}
}