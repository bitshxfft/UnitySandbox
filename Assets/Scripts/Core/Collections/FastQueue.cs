namespace Bitwise.Core.Collections
{
	public class FastQueue<T> : IQueue<T>
	{
		private CircularList<T> _contents;

		// ----------------------------------------------------------------------------

		public int Count => _contents.Count;
		public int Capacity => _contents.Capacity;
	}
}