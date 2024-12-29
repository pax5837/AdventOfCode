
namespace AdventOfCode2024_009;

internal static class Part2
{
	public static string Process(string line, bool withConsoleOutput = false)
	{
		var blocks = line
			.ToCharArray()
			.SelectMany(BlockFactory.CreateBlocky)
			.ToList();

		PrintBlocks(withConsoleOutput, blocks);

		var doDefragment = true;

		var fileIdsToProcess = new Queue<int>(blocks.Where(b => b.IsFileBlock).Select(b => b.FileId!.Value).Reverse());

		while (doDefragment)
		{
			if (fileIdsToProcess.TryDequeue(out var fileId))
			{
				var firstBlockWithLastFileId = blocks.First(b => b.IsFileBlock && b.FileId!.Value == fileId);
				var lastBlockWithLastFileId = blocks.Last(b => b.IsFileBlock && b.FileId!.Value == fileId);
				var indexFirstFileBlock = blocks.IndexOf(firstBlockWithLastFileId);
				var indexLastFileBlock = blocks.IndexOf(lastBlockWithLastFileId);
				var size = indexLastFileBlock - indexFirstFileBlock + 1;
				var firstIndexMatchingSpace = FindFirstIndexMatchingSpace(blocks, size, indexFirstFileBlock);
				if (firstIndexMatchingSpace is null)
				{
					continue;
				}

				for (int index = 0; index < size; index++)
				{
					(blocks[index + indexFirstFileBlock], blocks[index + firstIndexMatchingSpace!.Value]) = (blocks[index + firstIndexMatchingSpace!.Value], blocks[index + indexFirstFileBlock]);
				}
			}
			else
			{
				doDefragment = false;
			}

			PrintBlocks(withConsoleOutput, blocks);
		}



		return blocks
			.Select((b, index) => b.FileId is not null ? ((long)index * (long)b.FileId.Value) : 0l)
			.Sum()
			.ToString();
	}

	private static int? FindFirstIndexMatchingSpace(
		List<Blocky> blocks,
		int size,
		int beforeIndex)
	{
		for (var index = 0; index < beforeIndex; index++)
		{
			if (blocks.Skip(index).Take(size).All(b => b.IsEmptyBlock))
			{
				return index;
			}
		}

		return null;
	}

	private static void PrintBlocks(bool withConsoleOutput, List<Blocky> blocks)
	{
		if (withConsoleOutput)
		{
			Console.WriteLine(string.Join("", blocks.Select(g => g.ToString())));
		}
	}


}