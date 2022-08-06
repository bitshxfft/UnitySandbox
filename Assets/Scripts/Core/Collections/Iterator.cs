namespace Bitwise.Core.Collections
{
	public struct Iterator
	{
		public int Index { get; }

		// ----------------------------------------------------------------------------

		public Iterator(int index) => Index = index;

		// ----------------------------------------------------------------------------

		public static Iterator operator++(Iterator original) => new Iterator(original.Index + 1);

		public static Iterator operator--(Iterator original) => new Iterator(original.Index - 1);

		public static bool operator==(Iterator original, Iterator other) => original.Index == other.Index;

		public static bool operator!=(Iterator original, Iterator other) => original.Index != other.Index;

		public static bool operator<(Iterator original, Iterator other) => original.Index < other.Index;

		public static bool operator<=(Iterator original, Iterator other) => original.Index <= other.Index;

		public static bool operator>(Iterator original, Iterator other) => original.Index > other.Index;

		public static bool operator>=(Iterator original, Iterator other) => original.Index >= other.Index;
	}
}