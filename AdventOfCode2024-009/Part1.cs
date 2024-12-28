using System.Collections.Immutable;

namespace AdventOfCode2024_009;

internal static class Part1
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
				.Reverse()
				.Take(blocks.Count(b => b.Id is null)) // We can only replace so many empty blocks
				.Where(b => b.Id is not null)); // Empty blocks are not candidates for displacement

		var deFragmentedBlocks = blocks
			.Select(b => ChooseBlock(b, candidatesForDisplacement))
			.Where(b => b.Id is not null)
			.Take(blocks.Count(b => b.Id is not null))
			.ToList();

		if (withConsoleOutput)
		{
			foreach (var block in deFragmentedBlocks) Console.Write(block);
			Console.WriteLine();
		}

		return deFragmentedBlocks
			.Select((b, index) => (long)index * (long)b.Id!.Value)
			.Sum()
			.ToString();
	}

	private static Block ChooseBlock(Block block, Queue<Block> candidatesForDisplacement)
	{
		return block.Id is not null || !candidatesForDisplacement.Any()
			? block
			: candidatesForDisplacement.Dequeue();
	}

	private static IEnumerable<Block> CreateBlock(char chr, int index)
	{
		var length = int.Parse(chr.ToString());

		return index % 2 == 0
			? Enumerable.Range(0, length).Select(_ => new Block(index/2))
			: Enumerable.Range(0, length).Select(_ => new Block(null));
	}
}