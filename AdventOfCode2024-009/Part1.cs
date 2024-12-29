using System.Collections.Immutable;

namespace AdventOfCode2024_009;

internal static class Part1
{
	public static string Process(string line, bool withConsoleOutput = false)
	{
		var blocks = line
			.ToCharArray()
			.SelectMany(BlockFactory.CreateBlock)
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

		var deFragmentedBlocks = blocks
			.Select(b => ChooseBlock(b, candidatesForDisplacement))
			.Where(b => b.IsFileBlock)
			.Take(blocks.Count(b => b.IsFileBlock)) // To remove duplicates
			.ToList();

		if (withConsoleOutput)
		{
			foreach (var block in deFragmentedBlocks) Console.Write(block);
			Console.WriteLine();
		}

		return deFragmentedBlocks
			.Select((b, index) => (long)index * (long)b.FileId!.Value)
			.Sum()
			.ToString();
	}

	private static Block ChooseBlock(Block block, Queue<Block> candidatesForDisplacement)
	{
		return block.FileId is not null || !candidatesForDisplacement.Any()
			? block
			: candidatesForDisplacement.Dequeue();
	}
}