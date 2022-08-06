using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Bitwise.Core.Collections
{
	[Serializable]
	public class FastList<T> : IList<T> where T : IComparable<T>
	{
		[SerializeField, ReadOnly] private List<T> _contents;
		[SerializeField, ReadOnly] private int _firstIndex;
		[SerializeField, ReadOnly] private int _lastIndex;
		[SerializeField, ReadOnly] private FastListPreferences _preferences;

		// ----------------------------------------------------------------------------

		public FastList(int initialCapacity = 4, FastListPreferences preferences = FastListPreferences.None)
		{
			_contents = new List<T>(4);
			_preferences = preferences;

			Resize(initialCapacity);
		}

		// ----------------------------------------------------------------------------

		private void IncrementFirstIndex()
		{
			_firstIndex = (_firstIndex + 1) % Capacity;
		}

		private void DecrementLastIndex()
		{
			if (--_lastIndex < 0)
			{
				_lastIndex += Count;
			}
		}

		private int ConvertIndex(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException($"[FastList::ConvertIndex] invalid index: {index}. Count: {Count}, Capacity: {Capacity}");
			}

			return (_firstIndex + index) % Capacity;
		}

		private void Resize(int newCapacity)
		{
			if (newCapacity == Capacity)
			{
				return;
			}

			ResetOffsets();

			if (newCapacity < Capacity)
			{
				_contents.RemoveRange(newCapacity, Capacity - newCapacity);
				Count = Mathf.Min(Count, Capacity);
			}
			else
			{
				for (int i = Capacity; i < newCapacity; ++i)
				{
					_contents.Add(default);
				}
			}
		}

		private void ResetOffsets()
		{
			if (Count == 0)
			{
				// nothing to reset
				return;
			}

			if (_firstIndex == 0)
			{
				// indices already mapped 1:1
				return;
			}

			// #SD: TODO
			for (int i = _firstIndex; i < Count; ++i)
			{
				//for (int j = i; j >= _firstIndex; --j)
				//{
				//	T temp = _contents[j - 1];
				//	_contents[j - 1] = _contents[j];
				//	_contents[j] = temp;
				//}
			}

			_firstIndex = 0;
			_lastIndex = Count - 1;
		}

		// ----------------------------------------------------------------------------
		// IContainer

		public int Count { get; private set; }

		public int Capacity
		{
			get => _contents.Capacity;
			set => Resize(value);
		}

		public void Clear()
		{
			_firstIndex = 0;
			_lastIndex = 0;
			Count = 0;
		}

		// ----------------------------------------------------------------------------
		// IIterable

		public T this[Iterator iterator]
		{
			get => _contents[ConvertIndex(iterator.Index)];
			set => _contents[ConvertIndex(iterator.Index)] = value;
		}

		public Iterator First()
		{
			return new Iterator(0);
		}

		public Iterator Previous(Iterator iterator)
		{
			return --iterator;
		}

		public Iterator Next(Iterator iterator)
		{
			return ++iterator;
		}

		public Iterator Last()
		{
			return new Iterator(Count - 1);
		}

		// ----------------------------------------------------------------------------
		// IList

		public T this[int index]
		{
			get => _contents[ConvertIndex(index)];
			set => _contents[ConvertIndex(index)] = value;
		}

		public void Add(T value)
		{
			if (Count == Capacity)
			{
				Resize(Count + 4);
			}

			++Count;
			_lastIndex = (_lastIndex + 1) % Capacity;
			_contents[_lastIndex] = value;
		}

		public void AddRange(IIterable<T> values)
		{
			int addCount = values.Count;
			if (Capacity < Count + addCount)
			{
				Capacity = Count + addCount;
			}

			for (Iterator i = values.First(); i <= values.Last(); ++i)
			{
				Add(values[i]);
			}
		}

		public bool Insert(T value, int index)
		{
			if (Count == Capacity)
			{
				Resize(Count + 4);
			}

			return false;
		}

		public bool Remove(T value)
		{
			for (Iterator i = First(); i <= Last(); ++i)
			{
				if (_contents[ConvertIndex(i.Index)].CompareTo(value) == 0)
				{
					RemoveAt(i.Index);
					return true;
				}
			}

			return false;
		}

		public int RemoveAll(T value)
		{
			int countRemoved = 0;
			for (Iterator i = Last(); i >= Last(); --i)
			{
				if (_contents[ConvertIndex(i.Index)].CompareTo(value) == 0)
				{
					RemoveAt(i.Index);
					++countRemoved;
				}
			}

			return countRemoved;
		}

		public T RemoveAt(int index)
		{
			int convertedIndex = ConvertIndex(index);
			T removed = _contents[convertedIndex];

			if (index == First().Index)
			{
				IncrementFirstIndex();
			}
			else if (index == Last().Index)
			{
				DecrementLastIndex();
			}
			else if ((_preferences & FastListPreferences.PreserveOrder) == FastListPreferences.PreserveOrder)
			{
				for (int i = convertedIndex; i != _lastIndex; ++i)
				{
					_contents[i % Capacity] = _contents[(i + 1) % Capacity];
				}

				DecrementLastIndex();
			}
			else
			{
				_contents[convertedIndex] = _contents[_lastIndex];
				DecrementLastIndex();
			}

			return removed;
		}
	}
}