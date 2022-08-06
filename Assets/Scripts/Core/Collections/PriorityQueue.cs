using System;

namespace Bitwise.Core.Collections
{
	public class PriorityQueue<T> : IQueue<T> where T : IComparable<T>
	{
		private FastList<T> _contents;

		// ----------------------------------------------------------------------------

		public PriorityQueue(int capacity)
		{
			_contents = new FastList<T>(capacity, FastListPreferences.PreserveOrder);
		}

		// ----------------------------------------------------------------------------
		// IContainer

		public int Count => _contents.Count;

		public int Capacity => _contents.Capacity;

		public void Clear()
		{
			_contents.Clear();
		}

		// ----------------------------------------------------------------------------
		// IQueue

		public void Push(T value)
		{
			// #SD: TODO
		}

		public T Peek()
		{
			return _contents[_contents.Last()];
		}

		public T Pop()
		{
			return _contents.RemoveAt(_contents.Last().Index);
		}
	}
}