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

		var filesToProcess = new Queue<FileBlockGroup>(
			blocks
				.Select((block, index) => (block, index))
				.Where(x => x.block.IsFileBlock)
				.GroupBy(x => x.block.FileId!.Value)
				.Select(x => new FileBlockGroup(
					fileId: x.Key,
					initialFirstIndex: x.Min(y => y.index),
					initialLastIndex: x.Max(y => y.index)))
				.Reverse());

		var emptyBlockIndices = blocks
			.Select((block, index) => (block, index))
			.Where(x => x.block.IsEmptyBlock)
			.Select(x => x.index)
			.ToHashSet();

		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join(", ", filesToProcess.Select(x => $"{x.FileId}:{x.InitialFirstIndex}-{x.Size}")));
		}

		while (filesToProcess.TryDequeue(out var fileBlockGroup))
		{
			var firstIndexMatchingSpace = FindFirstIndexMatchingEmptyBlockGroupOrNull(
				size: fileBlockGroup.Size,
				beforeIndex: fileBlockGroup.InitialFirstIndex,
				emptyBlockIndices: emptyBlockIndices);

			if (firstIndexMatchingSpace is null)
			{
				continue;
			}

			for (int index = 0; index < fileBlockGroup.Size; index++)
			{
				SwapBlocksAndUpdateEmptyBlockIndicesSet(
					index: index,
					fileBlockGroup: fileBlockGroup,
					firstIndexMatchingSpace: firstIndexMatchingSpace!.Value,
					blocks: blocks,
					emptyBlockIndices: emptyBlockIndices);
			}

			PrintBlocks(withConsoleOutput, blocks);
		}

		return blocks
			.Select((b, index) => b.FileId is not null ? ((long)index * (long)b.FileId.Value) : 0l)
			.Sum()
			.ToString();
	}

	private static void SwapBlocksAndUpdateEmptyBlockIndicesSet(
		int index,
		FileBlockGroup fileBlockGroup,
		int firstIndexMatchingSpace,
		List<Block> blocks,
		HashSet<int> emptyBlockIndices)
	{
		var oldFileBlockIndex = index + fileBlockGroup.InitialFirstIndex;
		var oldEmptyBlockIndex = index + firstIndexMatchingSpace;
		var newFileBlockIndex = oldEmptyBlockIndex;
		var newEmptyBlockIndex = oldFileBlockIndex;
		(blocks[newEmptyBlockIndex], blocks[newFileBlockIndex]) = (blocks[oldEmptyBlockIndex], blocks[oldFileBlockIndex]);
		emptyBlockIndices.Remove(oldEmptyBlockIndex);
		emptyBlockIndices.Add(newEmptyBlockIndex);
	}

	private static int? FindFirstIndexMatchingEmptyBlockGroupOrNull(
		int size,
		int beforeIndex,
		HashSet<int> emptyBlockIndices)
	{
		for (var index = emptyBlockIndices.Min(); index < beforeIndex; index++)
		{
			if (!emptyBlockIndices.Contains(index))
			{
				continue;
			}

			if (HasSpecifiedContiguousEmptyFollowingBlocks(size, emptyBlockIndices, index))
			{
				return index;
			}
		}

		return null;
	}

	private static bool HasSpecifiedContiguousEmptyFollowingBlocks(int size, HashSet<int> emptyBlockIndices, int index)
	{
		for (var followingIndex = 1; followingIndex < size; followingIndex++) // we tested first index of range, so we can start with second one, hence followingIndex starts with 1
		{
			if (!emptyBlockIndices.Contains(index + followingIndex))
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

	record FileBlockGroup
	{
		public int FileId { get; }
		public int InitialFirstIndex { get; }
		public int Size { get; }

		public FileBlockGroup(int fileId, int initialFirstIndex, int initialLastIndex)
		{
			FileId = fileId;
			InitialFirstIndex = initialFirstIndex;
			Size = initialLastIndex - initialFirstIndex + 1;
		}
	}
}