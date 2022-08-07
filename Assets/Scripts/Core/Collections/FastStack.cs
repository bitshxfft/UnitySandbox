using System;

// #SD: TODO - Tests

namespace Bitwise.Core.Collections
{
	public class FastStack<T> : IStack<T> where T : IComparable<T>
	{
		private readonly FastList<T> _contents;

		// ----------------------------------------------------------------------------

		public FastStack(int capacity)
		{
			_contents = new FastList<T>(capacity, FastListPreferences.PreserveOrder);
		}

		// ----------------------------------------------------------------------------
		// IContainer

		public int Count => _contents.Count;

		public int Capacity => _contents.Capacity;

		public void Clear() => _contents.Clear();

		// ----------------------------------------------------------------------------
		// IStack

		public void Push(T value) => _contents.Add(value);

		public T Peek() => _contents[_contents.Last()];

		public T Pop() => _contents.RemoveAt(_contents.Last().Index);
	}
}