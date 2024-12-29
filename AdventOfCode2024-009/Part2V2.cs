
using System.Collections.Immutable;

namespace AdventOfCode2024_009;

internal static class Part2V2
{
	public static string Process(string line, bool withConsoleOutput = false)
	{
		var blocks = line
			.ToCharArray()
			.SelectMany(BlockFactory.CreateBlock)
			.ToList();

		PrintBlocks(withConsoleOutput, blocks);

		var initialFilePositions = blocks
			.Select((block, index) => (block, index))
			.Where(x => x.block.IsFileBlock)
			.GroupBy(x => x.block.FileId!.Value)
			.Select(x => new BlockGroup(x.Key, x.Min(y => y.index), x.Max(y => y.index)))
			.ToImmutableDictionary(x => x.FileId);

		var emptyBlockIndices = blocks
			.Select((block, index) => (block, index))
			.Where(x => x.block.IsEmptyBlock)
			.Select(x => x.index)
			.ToHashSet();

		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join(", ", initialFilePositions.Values.Select(x => $"{x.FileId}:{x.firstIndex}-{x.lastIndex}")));
		}

		var fileIdsToProcess = new Queue<int>(blocks.Where(b => b.IsFileBlock).Select(b => b.FileId!.Value).Distinct().Reverse());

		while (fileIdsToProcess.TryDequeue(out var fileId))
		{
			var indexFirstFileBlock = initialFilePositions[fileId].firstIndex;
			var indexLastFileBlock = initialFilePositions[fileId].lastIndex;
			var size = indexLastFileBlock - indexFirstFileBlock + 1;
			var firstIndexMatchingSpace = FindFirstIndexMatchingSpace(blocks, size, indexFirstFileBlock, emptyBlockIndices);
			if (firstIndexMatchingSpace is null)
			{
				continue;
			}

			for (int index = 0; index < size; index++)
			{
				var oldFileBlockIndex = index + indexFirstFileBlock;
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

	private static int? FindFirstIndexMatchingSpace(List<Block> blocks,
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

			// if (blocks.Skip(index).Take(size).All(b => b.IsEmptyBlock))
			// {
			// 	return index;
			// }
			var emptyCount = 0;
			for (var i = 0; i < size; i++)
			{
				if (emptyBlockIndices.Contains(index + i))
				{
					emptyCount++;
				}
			}

			if (emptyCount == size)
			{
				return index;
			}
		}

		// var orderedEmptyIndices = emptyBlockIndices.Where(x => x < beforeIndex).OrderBy(x => x).ToList();
		// foreach (var index in orderedEmptyIndices)
		// {
		// 	if (blocks.Skip(index).Take(size).All(b => b.IsEmptyBlock))
		// 	{
		// 		return index;
		// 	}
		// }


		// var emptyBlockIndices = blocks
		// 	.Select((block, index) => (block, index))
		// 	.Where(x => x.block.IsEmptyBlock)
		// 	.Select(x => x.index)
		// 	.ToList();
		//
		// foreach (var index in emptyBlockIndices)
		// {
		// 	if (blocks.Skip(index).Take(size).All(b => b.IsEmptyBlock))
		// 	{
		// 		return index;
		// 	}
		// }

		return null;
	}

	private static void PrintBlocks(bool withConsoleOutput, List<Block> blocks)
	{
		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join("", blocks.Select(g => g.ToString())));
		}
	}

	record BlockGroup(int FileId, int firstIndex, int lastIndex);
}