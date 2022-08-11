using System;
using Bitwise.Core.Collections;
using NUnit.Framework;
using Random = System.Random;

namespace Bitwise.Tests.Core.Collections
{
	public class FastListTests
	{
		[Test]
		public void FastListCountTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			Assert.AreEqual(0, fastList.Count);

			fastList.Add(1);
			Assert.AreEqual(1, fastList.Count);

			fastList.Add(2);
			Assert.AreEqual(2, fastList.Count);

			fastList.Add(3);
			Assert.AreEqual(3, fastList.Count);

			fastList.Add(4);
			Assert.AreEqual(4, fastList.Count);

			fastList.Add(5);
			Assert.AreEqual(5, fastList.Count);

			fastList.Add(6);
			Assert.AreEqual(6, fastList.Count);
		}

		[Test]
		public void FastListCapacityTest()
		{
			int initialCapacity = 4;
			int growCapacity = 6;
			int shrinkCapacity = 2;

			var fastList = new FastList<int>(initialCapacity);
			Assert.AreEqual(initialCapacity, fastList.Capacity);

			fastList.Capacity = growCapacity;
			Assert.AreEqual(growCapacity, fastList.Capacity);

			fastList.Capacity = shrinkCapacity;
			Assert.AreEqual(shrinkCapacity, fastList.Capacity);
		}

		[Test]
		public void FastListSetCapacityTest()
		{
			int initialCapacity = 3;
			int growCapacity = 9;

			var fastList = new FastList<int>(initialCapacity);
			Assert.AreEqual(initialCapacity, fastList.Capacity);

			fastList.Capacity = growCapacity;
			Assert.AreEqual(growCapacity, fastList.Capacity);
		}

		[Test]
		public void FastListSetInvalidCapacityThrowsTest()
		{
			int initialCapacity = 4;
			int shrinkCapacity = 3;

			var fastList = new FastList<int>(initialCapacity);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);

			Assert.Throws<ArgumentOutOfRangeException>(() => fastList.Capacity = shrinkCapacity);
			Assert.AreEqual(initialCapacity, fastList.Capacity);
		}

		[Test]
		public void FastListClearTest()
		{
			int initialCapacity = 4;

			var fastList = new FastList<int>(initialCapacity);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Clear();

			Assert.AreEqual(initialCapacity + 4, fastList.Capacity);
			Assert.AreEqual(0, fastList.Count);
		}

		[Test]
		public void FastListIteratorTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			Iterator iterator = fastList.First();
			Assert.AreEqual(0, iterator.Index);
			Assert.AreEqual(1, fastList[iterator]);

			++iterator;
			Assert.AreEqual(1, iterator.Index);
			Assert.AreEqual(2, fastList[iterator]);

			++iterator;
			Assert.AreEqual(2, iterator.Index);
			Assert.AreEqual(3, fastList[iterator]);

			++iterator;
			Assert.AreEqual(3, iterator.Index);
			Assert.AreEqual(4, fastList[iterator]);

			++iterator;
			Assert.AreEqual(4, iterator.Index);
			Assert.AreEqual(5, fastList[iterator]);
			Assert.AreEqual(fastList.Last(), iterator);
		}

		[Test]
		public void FastListIteratorThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(0);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			Assert.Throws<IndexOutOfRangeException>(() => _ = fastList[fastList.Previous(fastList.First())]);
			Assert.Throws<IndexOutOfRangeException>(() => _ = fastList[fastList.Next(fastList.Last())]);
		}

		[Test]
		public void FastListFirstIteratorTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);

			Assert.AreEqual(0, fastList.First().Index);
			Assert.AreEqual(1, fastList[fastList.First()]);
		}

		[Test]
		public void FastListPreviousIteratorTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			Iterator iterator = fastList.Last();
			iterator = fastList.Previous(iterator);
			Assert.AreEqual(3, iterator.Index);
			Assert.AreEqual(4, fastList[iterator]);

			iterator = fastList.Previous(iterator);
			Assert.AreEqual(2, iterator.Index);
			Assert.AreEqual(3, fastList[iterator]);

			iterator = fastList.Previous(iterator);
			Assert.AreEqual(1, iterator.Index);
			Assert.AreEqual(2, fastList[iterator]);

			iterator = fastList.Previous(iterator);
			Assert.AreEqual(0, iterator.Index);
			Assert.AreEqual(1, fastList[iterator]);
			Assert.AreEqual(fastList[fastList.First()], fastList[iterator]);
			Assert.AreEqual(fastList.First().Index, iterator.Index);
		}

		[Test]
		public void FastListNextIteratorTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			Iterator iterator = fastList.First();
			iterator = fastList.Next(iterator);
			Assert.AreEqual(1, iterator.Index);
			Assert.AreEqual(2, fastList[iterator]);

			iterator = fastList.Next(iterator);
			Assert.AreEqual(2, iterator.Index);
			Assert.AreEqual(3, fastList[iterator]);

			iterator = fastList.Next(iterator);
			Assert.AreEqual(3, iterator.Index);
			Assert.AreEqual(4, fastList[iterator]);

			iterator = fastList.Next(iterator);
			Assert.AreEqual(4, iterator.Index);
			Assert.AreEqual(5, fastList[iterator]);
			Assert.AreEqual(fastList[fastList.Last()], fastList[iterator]);
			Assert.AreEqual(fastList.Last().Index, iterator.Index);
		}

		[Test]
		public void FastListLastIteratorTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);

			Assert.AreEqual(2, fastList.Last().Index);
			Assert.AreEqual(3, fastList[fastList.Last()]);
		}

		[Test]
		public void FastListGetAtIndexTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
			Assert.AreEqual(5, fastList[4]);
			Assert.AreEqual(6, fastList[5]);
		}

		[Test]
		public void FastListGetAtIndexThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			Assert.Throws<IndexOutOfRangeException>(() => _ = fastList[-1]);
			Assert.DoesNotThrow(() => _ = fastList[0]);
			Assert.DoesNotThrow(() => _ = fastList[1]);
			Assert.DoesNotThrow(() => _ = fastList[2]);
			Assert.DoesNotThrow(() => _ = fastList[3]);
			Assert.DoesNotThrow(() => _ = fastList[4]);
			Assert.DoesNotThrow(() => _ = fastList[5]);
			Assert.Throws<IndexOutOfRangeException>(() => _ = fastList[6]);
		}

		[Test]
		public void FastListSetAtIndexTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(0);
			fastList.Add(0);
			fastList.Add(0);
			fastList.Add(0);
			fastList.Add(0);
			fastList.Add(0);

			fastList[0] = 1;
			fastList[1] = 2;
			fastList[2] = 3;
			fastList[3] = 4;
			fastList[4] = 5;
			fastList[5] = 6;

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
			Assert.AreEqual(5, fastList[4]);
			Assert.AreEqual(6, fastList[5]);
		}

		[Test]
		public void FastListGetInvalidIndexThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);

			Assert.Throws<IndexOutOfRangeException>(() => _ = fastList[3]);
		}

		[Test]
		public void FastListSetInvalidIndexThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);

			Assert.Throws<IndexOutOfRangeException>(() => fastList[3] = 4);
		}

		[Test]
		public void FastListAddRemoveIndexPreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			fastList.RemoveAt(0);
			fastList.RemoveAt(0);
			fastList.RemoveAt(0);

			// 1, 2, 3, 4, 5, 6
			// 2, 3, 4, 5, 6
			// 3, 4, 5, 6
			// 4, 5, 6

			fastList.Add(7);
			fastList.Add(8);
			fastList.Add(9);

			// 4, 5, 6, 7, 8, 9

			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(5, fastList[1]);
			Assert.AreEqual(6, fastList[2]);
			Assert.AreEqual(7, fastList[3]);
			Assert.AreEqual(8, fastList[4]);
			Assert.AreEqual(9, fastList[5]);

			fastList.RemoveAt(3);
			fastList.RemoveAt(2);
			fastList.RemoveAt(1);

			// 4, 5, 6, 7, 8, 9
			// 4, 5, 6, 8, 9
			// 4, 5, 8, 9
			// 4, 8, 9

			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(8, fastList[1]);
			Assert.AreEqual(9, fastList[2]);
		}

		[Test]
		public void FastListAddRemoveIndexAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			// 1, 2, 3, 4, 5, 6
			// 6, 2, 3, 4, 5
			// 5, 2, 3, 4
			// 4, 2, 3

			fastList.RemoveAt(0);
			fastList.RemoveAt(0);
			fastList.RemoveAt(0);

			fastList.Add(7);
			fastList.Add(8);
			fastList.Add(9);

			// 4, 2, 3, 7, 8, 9

			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(7, fastList[3]);
			Assert.AreEqual(8, fastList[4]);
			Assert.AreEqual(9, fastList[5]);

			fastList.RemoveAt(3);
			fastList.RemoveAt(2);
			fastList.RemoveAt(1);

			// 4, 2, 3, 7, 8, 9
			// 4, 2, 3, 9, 8
			// 4, 2, 8, 9
			// 4, 9, 8

			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(9, fastList[1]);
			Assert.AreEqual(8, fastList[2]);
		}

		[Test]
		public void FastListAddTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			Assert.AreEqual(1, fastList.Count);

			fastList.Add(2);
			Assert.AreEqual(2, fastList.Count);

			fastList.Add(3);
			Assert.AreEqual(3, fastList.Count);

			fastList.Add(4);
			Assert.AreEqual(4, fastList.Count);
		}

		[Test]
		public void FastListAddGrowCapacityTest()
		{
			int initialCapacity = 2;
			int growBy = 4;

			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			Assert.AreEqual(1, fastList.Count);
			Assert.AreEqual(initialCapacity, fastList.Capacity);

			fastList.Add(2);
			Assert.AreEqual(2, fastList.Count);
			Assert.AreEqual(initialCapacity, fastList.Capacity);

			fastList.Add(3);
			Assert.AreEqual(3, fastList.Count);
			Assert.AreEqual(initialCapacity + growBy, fastList.Capacity);

			fastList.Add(4);
			Assert.AreEqual(4, fastList.Count);
			Assert.AreEqual(initialCapacity + growBy, fastList.Capacity);

			fastList.Add(5);
			Assert.AreEqual(5, fastList.Count);
			Assert.AreEqual(initialCapacity + growBy, fastList.Capacity);

			fastList.Add(6);
			Assert.AreEqual(6, fastList.Count);
			Assert.AreEqual(initialCapacity + growBy, fastList.Capacity);

			fastList.Add(7);
			Assert.AreEqual(7, fastList.Count);
			Assert.AreEqual(initialCapacity + (2 * growBy), fastList.Capacity);

			fastList.Add(8);
			Assert.AreEqual(8, fastList.Count);
			Assert.AreEqual(initialCapacity + (2 * growBy), fastList.Capacity);
		}

		[Test]
		public void FastListAddRangeTest()
		{
			int initialCapacity = 3;
			var originList = new FastList<int>(initialCapacity);
			originList.Add(1);
			originList.Add(2);
			originList.Add(3);

			var additionList = new FastList<int>(initialCapacity);
			additionList.Add(6);
			additionList.Add(5);
			additionList.Add(4);

			originList.AddRange(additionList);

			Assert.AreEqual(6, originList.Count);
			Assert.AreEqual(6, originList.Capacity);

			Assert.AreEqual(1, originList[0]);
			Assert.AreEqual(2, originList[1]);
			Assert.AreEqual(3, originList[2]);
			Assert.AreEqual(6, originList[3]);
			Assert.AreEqual(5, originList[4]);
			Assert.AreEqual(4, originList[5]);
		}

		[Test]
		public void FastListInsertPreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);
			fastList.Add(2);
			fastList.Add(4);
			fastList.Add(6);

			// [2, 4, 6]

			fastList.Insert(1, 0);

			// [1, 2, 4, 6]

			fastList.Insert(3, 2);

			// [1, 2, 3, 4, 6]

			fastList.Insert(5, 4);

			// [1, 2, 3, 4, 5, 6]

			fastList.Insert(7, 6);

			// [1, 2, 3, 4, 5, 6, 7]

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
			Assert.AreEqual(5, fastList[4]);
			Assert.AreEqual(6, fastList[5]);
			Assert.AreEqual(7, fastList[6]);
		}

		[Test]
		public void FastListInsertBeginningPreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			// [4, 5, 6]

			fastList.Insert(3, 0);

			// [3, 4, 5, 6]

			fastList.Insert(2, 0);

			// [2, 3, 4, 5, 6]

			fastList.Insert(1, 0);

			// [1, 2, 3, 4, 5, 6]

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
			Assert.AreEqual(5, fastList[4]);
			Assert.AreEqual(6, fastList[5]);
		}

		[Test]
		public void FastListInsertBeginningAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			// [4, 5, 6]

			fastList.Insert(3, 0);

			// [3, 5, 6, 4]

			fastList.Insert(2, 0);

			// [2, 5, 6, 4, 3]

			fastList.Insert(1, 0);

			// [1, 5, 6, 4, 3, 2]

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(5, fastList[1]);
			Assert.AreEqual(6, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
			Assert.AreEqual(3, fastList[4]);
			Assert.AreEqual(2, fastList[5]);
		}

		[Test]
		public void FastListInsertAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);
			fastList.Add(2);
			fastList.Add(4);
			fastList.Add(6);

			// [2, 4, 6]

			fastList.Insert(1, 0);
			fastList.Insert(3, 2);
			fastList.Insert(5, 4);

			// [1, 4, 6, 2]
			// [1, 4, 3, 2, 6]
			// [1, 4, 3, 2, 5, 6]

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(4, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(2, fastList[3]);
			Assert.AreEqual(5, fastList[4]);
			Assert.AreEqual(6, fastList[5]);
		}

		[Test]
		public void FastListInsertInvalidIndexThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			Assert.DoesNotThrow(() => fastList.Insert(1, 0));

			fastList.Add(2);
			fastList.Add(3);

			// [1, 2, 3]

			Assert.DoesNotThrow(() => fastList.Insert(1, 0));

			// [1, 1, 2, 3]

			Assert.DoesNotThrow(() => fastList.Insert(4, 4));

			// [1, 1, 2, 3, 4]

			Assert.DoesNotThrow(() => fastList.Insert(2, 2));

			// [1, 1, 2, 2, 3, 4]

			Assert.Throws<IndexOutOfRangeException>(() => fastList.Insert(0, -1));
			Assert.Throws<IndexOutOfRangeException>(() => fastList.Insert(11, 10));
			Assert.Throws<IndexOutOfRangeException>(() => fastList.Insert(int.MaxValue, 911));
		}

		[Test]
		public void FastListRemovePreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Remove(3);

			Assert.AreEqual(4, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.True(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.False(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.False(fastList.Contains(6));
		}

		[Test]
		public void FastListRemoveAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			// [1, 2, 3, 4, 5]

			fastList.Remove(3);

			// [1, 2, 5, 4]

			Assert.AreEqual(4, fastList.Count);
			Assert.True(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.False(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.True(fastList.Contains(5));

			fastList.Remove(1);

			// [4, 2, 5]

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.False(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.True(fastList.Contains(5));

			fastList.Remove(6);

			// [4, 2, 5]

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.False(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
		}

		[Test]
		public void FastListAddRemovePreservedOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			Assert.False(fastList.Remove(2));
			fastList.Add(2);
			Assert.True(fastList.Remove(2));
			fastList.Add(3);
			Assert.False(fastList.Remove(4));
			fastList.Add(4);
			Assert.True(fastList.Remove(4));
			fastList.Add(5);
			Assert.False(fastList.Remove(6));
			fastList.Add(6);
			Assert.True(fastList.Remove(6));
			Assert.False(fastList.Remove(7));

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.True(fastList.Contains(1));
			Assert.False(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.False(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.False(fastList.Contains(6));
			Assert.False(fastList.Contains(7));
			Assert.False(fastList.Contains(8));
			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
			Assert.AreEqual(5, fastList[2]);
		}

		[Test]
		public void FastListAddRemoveAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);

			// [1] 

			Assert.False(fastList.Remove(2));

			// [1]

			fastList.Add(2);

			// [1, 2]

			Assert.True(fastList.Remove(2));

			// [1]

			fastList.Add(3);

			// [1, 3]

			Assert.False(fastList.Remove(4));

			// [1, 3]

			fastList.Add(4);

			// [1, 3, 4]

			Assert.True(fastList.Remove(4));

			// [1, 3]

			fastList.Add(5);

			// [1, 3, 5]

			Assert.False(fastList.Remove(6));

			// [1, 3, 5]

			fastList.Add(6);

			// [1, 3, 5, 6]

			Assert.True(fastList.Remove(6));

			// [1, 3, 5]

			Assert.False(fastList.Remove(7));

			// [1, 3, 5]

			Assert.True(fastList.Remove(1));

			// [5, 3]

			Assert.False(fastList.Remove(1));

			// [5, 3]

			Assert.AreEqual(2, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.False(fastList.Contains(1));
			Assert.False(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.False(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.AreEqual(5, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
		}

		[Test]
		public void FastListRemoveSinglePreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			Assert.True(fastList.Remove(1));

			Assert.AreEqual(4, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.True(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(2, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
			Assert.AreEqual(4, fastList[3]);
		}

		[Test]
		public void FastListRemoveSingleAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);

			// [1, 1, 2, 3, 4]

			Assert.True(fastList.Remove(1));

			// [4, 1, 2, 3]

			Assert.AreEqual(4, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.True(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(1, fastList[1]);
			Assert.AreEqual(2, fastList[2]);
			Assert.AreEqual(3, fastList[3]);
		}

		[Test]
		public void FastListRemoveMultiplePreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			Assert.True(fastList.Remove(1));
			Assert.True(fastList.Remove(1));
			Assert.False(fastList.Remove(1));

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(2, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
			Assert.AreEqual(4, fastList[2]);
		}

		[Test]
		public void FastListRemoveMultipleAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);

			// [1, 1, 2, 3, 4]

			Assert.True(fastList.Remove(1));

			// [4, 1, 2, 3]

			Assert.True(fastList.Remove(1));

			// [4, 3, 2]

			Assert.False(fastList.Remove(1));

			// [4, 3, 2]

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(4, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
			Assert.AreEqual(2, fastList[2]);
		}

		[Test]
		public void FastListRemoveAllPreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.RemoveAll(1);

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(2, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
			Assert.AreEqual(4, fastList[2]);
		}

		[Test]
		public void FastListRemoveAllAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);

			// [1, 1, 2, 3, 4]

			fastList.RemoveAll(1);

			// [1, 4, 2, 3]
			// [3, 4, 2]

			Assert.AreEqual(3, fastList.Count);
			Assert.False(fastList.Contains(0));
			Assert.False(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.False(fastList.Contains(5));
			Assert.AreEqual(3, fastList[0]);
			Assert.AreEqual(4, fastList[1]);
			Assert.AreEqual(2, fastList[2]);
		}

		[Test]
		public void FastListRemoveAtPreserveOrderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			fastList.RemoveAt(5);
			fastList.RemoveAt(3);
			fastList.RemoveAt(1);

			Assert.True(fastList.Contains(1));
			Assert.False(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.False(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.False(fastList.Contains(6));

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(3, fastList[1]);
			Assert.AreEqual(5, fastList[2]);
		}

		[Test]
		public void FastListRemoveAtAllowReorderTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);
			fastList.Add(6);

			// [1, 2, 3, 4, 5, 6]

			fastList.RemoveAt(5);

			// [1, 2, 3, 4, 5]

			fastList.RemoveAt(3);

			// [1, 2, 3, 5]

			fastList.RemoveAt(1);

			// [1, 5, 3]

			Assert.True(fastList.Contains(1));
			Assert.False(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.False(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.False(fastList.Contains(6));

			Assert.AreEqual(1, fastList[0]);
			Assert.AreEqual(5, fastList[1]);
			Assert.AreEqual(3, fastList[2]);
		}

		[Test]
		public void FastListRemoveAtInvalidIndexThrowsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);

			Assert.Throws<IndexOutOfRangeException>(() => fastList.RemoveAt(3));
		}

		[Test]
		public void FastListContainsTest()
		{
			int initialCapacity = 4;
			var fastList = new FastList<int>(initialCapacity);

			fastList.Add(1);
			fastList.Add(2);
			fastList.Add(3);
			fastList.Add(4);
			fastList.Add(5);

			Assert.False(fastList.Contains(0));
			Assert.True(fastList.Contains(1));
			Assert.True(fastList.Contains(2));
			Assert.True(fastList.Contains(3));
			Assert.True(fastList.Contains(4));
			Assert.True(fastList.Contains(5));
			Assert.False(fastList.Contains(6));
		}

		[Test]
		public void FastListPreserveOrderStressTest()
		{
			int initialCapacity = 65_536;
			var fastList = new FastList<int>(initialCapacity, FastListPreferences.PreserveOrder);
			var random = new Random(123456789);

			for (int i = 0; i < 1_000_000; ++i)
			{
				int previousCount = fastList.Count;

				switch (random.Next(3))
				{
					case 0:
					{
						fastList.Add(i);
						Assert.AreEqual(previousCount + 1, fastList.Count);
						Assert.AreEqual(i, fastList[previousCount]);
						Assert.AreEqual(i, fastList[fastList.Last()]);
						Assert.True(fastList.Contains(i));
						break;
					}

					case 1:
					{
						if (fastList.Count > 0)
						{
							int removeIndex = random.Next(fastList.Count - 1);
							int removeValue = fastList[removeIndex];

							fastList.RemoveAt(removeIndex);
							Assert.AreEqual(previousCount - 1, fastList.Count);
							Assert.False(fastList.Contains(removeValue));
						}

						break;
					}

					case 2:
					{
						if (fastList.Count > 0)
						{
							int removeValue = fastList[random.Next(fastList.Count - 1)];

							fastList.Remove(removeValue);
							Assert.AreEqual(previousCount - 1, fastList.Count);
							Assert.False(fastList.Contains(removeValue));
						}

						break;
					}

					case 3:
					{
						if (fastList.Count > 0)
						{
							int insertIndex = random.Next(fastList.Count - 1);

							fastList.Insert(i, insertIndex);
							Assert.AreEqual(previousCount + 1, fastList.Count);
							Assert.AreEqual(i, fastList[insertIndex]);
							Assert.True(fastList.Contains(i));
						}

						break;
					}
				}
			}
		}

		[Test]
		public void FastListAllowReorderStressTest()
		{
			int initialCapacity = 65_536;
			var fastList = new FastList<int>(initialCapacity);
			var random = new Random(123456789);

			for (int i = 0; i < 1_000_000; ++i)
			{
				int previousCount = fastList.Count;

				switch (random.Next(3))
				{
					case 0:
					{
						fastList.Add(i);
						Assert.AreEqual(previousCount + 1, fastList.Count);
						Assert.AreEqual(i, fastList[previousCount]);
						Assert.AreEqual(i, fastList[fastList.Last()]);
						Assert.True(fastList.Contains(i));
						break;
					}

					case 1:
					{
						if (fastList.Count > 0)
						{
							int removeIndex = random.Next(fastList.Count - 1);
							int removeValue = fastList[removeIndex];

							fastList.RemoveAt(removeIndex);
							Assert.AreEqual(previousCount - 1, fastList.Count);
							Assert.False(fastList.Contains(removeValue));
						}

						break;
					}

					case 2:
					{
						if (fastList.Count > 0)
						{
							int removeValue = fastList[random.Next(fastList.Count - 1)];

							fastList.Remove(removeValue);
							Assert.AreEqual(previousCount - 1, fastList.Count);
							Assert.False(fastList.Contains(removeValue));
						}

						break;
					}

					case 3:
					{
						if (fastList.Count > 0)
						{
							int insertIndex = random.Next(fastList.Count - 1);

							fastList.Insert(i, insertIndex);
							Assert.AreEqual(previousCount + 1, fastList.Count);
							Assert.AreEqual(i, fastList[insertIndex]);
							Assert.True(fastList.Contains(i));
						}

						break;
					}
				}
			}
		}
	}
}