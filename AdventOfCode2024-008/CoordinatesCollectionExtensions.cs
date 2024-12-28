namespace AdventOdCode2024_008;

internal static class CoordinatesCollectionExtensions
{
	/// <summary>
	/// add a coordinates to the collection when it is within 0,0 and maxX,maxY
	/// </summary>
	/// <returns>True when added, False when not.</returns>
	public static bool TryAddCoordinates(
		this List<Coordinates> collection,
		int candidateX,
		int candidateY,
		Coordinates max)
	{
		if (candidateX > max.X || candidateY > max.Y || candidateX < 0 || candidateY < 0)
		{
			return false;
		}

		collection.Add(new Coordinates(candidateX, candidateY));

		return true;
	}
}