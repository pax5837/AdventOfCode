namespace AdventOdCode2024_008;

internal static class CoordinatesCollectionExtensions
{
	public static void AddCoordinatesWhenValid(
		this List<Coordinates> collection,
		int candidateX,
		int candidateY,
		Coordinates max)
	{
		if (candidateX > max.X || candidateY > max.Y || candidateX < 0 || candidateY < 0)
		{
			return;
		}

		collection.Add(new Coordinates(candidateX, candidateY));
	}
}