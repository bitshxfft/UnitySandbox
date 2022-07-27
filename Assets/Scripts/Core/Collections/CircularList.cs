using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bitwise.Core.Collections
{
	[Serializable]
	public class CircularList<T> : IList<T>
	{
		[SerializeField, HideInInspector] private List<T> _contents;
		[SerializeField, HideInInspector] private int _firstIndex;
		[SerializeField, HideInInspector] private int _lastIndex;

		// ----------------------------------------------------------------------------

		// #SD: TODO
		public int Count => 0;

		public int Capacity
		{
			get => _contents.Capacity;
			set => _contents.Capacity = value;
		}

		// ----------------------------------------------------------------------------

		public CircularList(int capacity = 4)
		{
			_contents = new List<T>(4);
		}

		public T this[int index]
		{
			get => _contents[ConvertIndex(index)];
			set => _contents[ConvertIndex(index)] = value;
		}

		// #SD: TODO
		// Add

		// #SD: TODO
		// Insert

		// #SD: TODO
		// RemoveAt

		// #SD: TODO
		// Clear

		private int ConvertIndex(int index)
		{
			// #SD: TODO
			return index;
		}
	}
}