using System;

// #SD: TODO - Tests

namespace Bitwise.Core.Collections
{
	public class FastQueue<T> : IQueue<T> where T : IComparable<T>
	{
		private readonly FastList<T> _contents;

		// ----------------------------------------------------------------------------

		public FastQueue(int capacity)
		{
			_contents = new FastList<T>(capacity, FastListPreferences.PreserveOrder);
		}

		// ----------------------------------------------------------------------------
		// IContainer

		public int Count => _contents.Count;

		public int Capacity => _contents.Capacity;

		public void Clear() => _contents.Clear();

		// ----------------------------------------------------------------------------
		// IQueue

		public void Push(T value) => _contents.Insert(value, 0);

		public T Peek() => _contents[_contents.Last()];

		public T Pop() => _contents.RemoveAt(_contents.Last().Index);
	}
}