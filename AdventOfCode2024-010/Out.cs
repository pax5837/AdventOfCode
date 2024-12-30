namespace AdventOfCode2024_010;

internal static class Out
{
	public static void PrintLn(Func<string> message, bool doPrint)
	{
		if (!doPrint)
		{
			return;
		}

		Console.WriteLine(message());
	}

	public static void Print(Func<string> message, bool doPrint)
	{
		if (!doPrint)
		{
			return;
		}

		Console.Write(message());
	}
}