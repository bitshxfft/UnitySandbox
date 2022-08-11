namespace Bitwise.Core.Collections
{
	public static class ListExtensionMethods
	{
		public static void AddUnique<T>(this IList<T> list, T value)
		{
			if (false == list.Contains(value))
			{
				list.Add(value);
			}
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			// #SD: TODO - Fisher-Yates shuffle
		}
	}
}