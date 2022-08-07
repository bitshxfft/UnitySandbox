using System;

// #SD: TODO - Tests

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
			for (int i = 0; i < _contents.Count; ++i)
			{
				if (value.CompareTo(_contents[i]) <= 0)
				{
					_contents.Insert(value, i);
					return;
				}
			}

			_contents.Add(value);
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