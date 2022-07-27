namespace Bitwise.Core.Collections
{
	public class FastStack<T> : IStack<T>
	{
		private CircularList<T> _contents;
		
		// ----------------------------------------------------------------------------

		public int Count => _contents.Count;
		public int Capacity => _contents.Capacity;
	}
}