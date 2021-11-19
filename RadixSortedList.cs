using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
public class RadixSortedList<T> 
{
	public const int GrowSize = 32;
	
	public RadixSort.RadixSortList list;
	public T[] elements;
	public int capacity;
	public int count;

	public RadixSortedList(int pCapacity)
	{
		capacity = pCapacity;
		list = new RadixSort.RadixSortList(pCapacity);
		elements = new T[pCapacity];
		count = 0;
	}

	public T this[int i]
	{
		[MethodImpl(AggressiveInlining)]
		get => elements[list.inIndices[i]];
	}

	public void AddElement(T element, int key)
	{
		if(count == capacity)
		{
			list.Grow(GrowSize);
			capacity += GrowSize;
			var newList = new T[capacity];
			Array.Copy(elements, newList, count);
			elements = newList;
		}
		elements[count] = element;
		list.SetIndex(count, key, count);
		//Console.WriteLine($"Added at {count} with key {key}");
		++count;
	}

	public void Reset()
	{
		Array.Clear(elements,0, count);
		count = 0;
	}

	public void Commit()
	{
		RadixSort.Sort(ref list, count);

	}
}

public class NormalSortedList<T> 
{
	public const int GrowSize = 32;
	public int capacity;
	public int count;
	public int[] keys;
	public T[] list;

	public NormalSortedList(int pCapacity)
	{
		capacity = pCapacity;
		list = new T[capacity];
		keys = new int[capacity];
	}


	public void AddElement(T element, int key)
	{
		if(count == capacity)
		{
			capacity += GrowSize;

			var newList = new T[capacity];
			Array.Copy(list, newList, count);
			list = newList;

			var newKeys = new int[capacity];
			Array.Copy(keys, newKeys, count);
			keys = newKeys;

		}

		var index = count++;
		keys[index] = key;
		list[index] = element;
	}

	public void Reset()
	{
		Array.Clear(list, 0, count);
		count = 0;
	}

	public void Commit()
	{
		Array.Sort(keys, list, 0, count);	
	}
}