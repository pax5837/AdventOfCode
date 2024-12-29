namespace AdventOfCode2024_009;

internal static class Part2V3
{
	public static string Process(string line, bool withConsoleOutput = false)
	{
		var blocks = line
			.ToCharArray()
			.SelectMany(BlockFactory.CreateBlock)
			.ToList();

		PrintBlocks(withConsoleOutput, blocks);

		var files = new Queue<FileBlockGroup>(
			blocks
				.Select((block, index) => (block, index))
				.Where(x => x.block.IsFileBlock)
				.GroupBy(x => x.block.FileId!.Value)
				.Select(x => new FileBlockGroup(x.Key, x.Min(y => y.index), x.Max(y => y.index)))
				.Reverse());

		var emptyBlockIndices = blocks
			.Select((block, index) => (block, index))
			.Where(x => x.block.IsEmptyBlock)
			.Select(x => x.index)
			.ToHashSet();

		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join(", ", files.Select(x => $"{x.FileId}:{x.InitialFirstIndex}-{x.InitialLastIndex}")));
		}

		while (files.TryDequeue(out var fileBlockGroup))
		{
			var size = fileBlockGroup.InitialLastIndex - fileBlockGroup.InitialFirstIndex + 1;
			var firstIndexMatchingSpace = FindFirstIndexMatchingSpace(blocks, size, fileBlockGroup.InitialFirstIndex, emptyBlockIndices);
			if (firstIndexMatchingSpace is null)
			{
				continue;
			}

			for (int index = 0; index < size; index++)
			{
				var oldFileBlockIndex = index + fileBlockGroup.InitialFirstIndex;
				var oldEmptyBlockIndex = index + firstIndexMatchingSpace!.Value;
				(blocks[oldFileBlockIndex], blocks[oldEmptyBlockIndex]) = (blocks[oldEmptyBlockIndex], blocks[oldFileBlockIndex]);
				emptyBlockIndices.Remove(oldEmptyBlockIndex);
				emptyBlockIndices.Add(oldFileBlockIndex);
			}

			PrintBlocks(withConsoleOutput, blocks);
		}

		return blocks
			.Select((b, index) => b.FileId is not null ? ((long)index * (long)b.FileId.Value) : 0l)
			.Sum()
			.ToString();
	}

	private static int? FindFirstIndexMatchingSpace(
		List<Block> blocks,
		int size,
		int beforeIndex,
		HashSet<int> emptyBlockIndices)
	{
		for (var index = emptyBlockIndices.Min(); index < beforeIndex; index++)
		{
			if (blocks[index].IsFileBlock)
			{
				continue;
			}

			if (HasSpecifiedContiguousEmptyBlocks(size, emptyBlockIndices, index))
			{
				return index;
			}
		}

		return null;
	}

	private static bool HasSpecifiedContiguousEmptyBlocks(int size, HashSet<int> emptyBlockIndices, int index)
	{
		for (var i = 0; i < size; i++)
		{
			if (!emptyBlockIndices.Contains(index + i))
			{
				return false;
			}
		}

		return true;
	}

	private static void PrintBlocks(bool withConsoleOutput, List<Block> blocks)
	{
		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join("", blocks.Select(g => g.ToString())));
		}
	}

	record FileBlockGroup(int FileId, int InitialFirstIndex, int InitialLastIndex);
}