using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

public static class RadixSort {
	public static int[] countList;
	public const int Radix = 16;

	static RadixSort()
	{
		countList = new int[Radix];
	}

	//Structure for storing the input and output structures
	//for radix sorts, the indices array can be used to point
	//to elements in some other array so it can be indexed in
	//sorted order
	public struct RadixSortList
	{
		public int[] keys;
		public int[] outKeys;
		public int[] inIndices;
		public int[] outIndices;

		public RadixSortList(int capacity)
		{
			keys = new int[capacity];
			outKeys = new int[capacity];
			inIndices = new int[capacity];
			outIndices = new int[capacity];
		}

		public void Grow(int growSize)
		{
			int count = keys.Length;
			int capacity = count + growSize;
			
			var newList = new int[capacity];
			Array.Copy(keys, newList, count);
			keys = newList;

			newList = new int[capacity];
			Array.Copy(inIndices, newList, count);
			inIndices = newList;
			
			outKeys = new int[capacity];
			outIndices = new int[capacity];
		}
		
		[MethodImpl(AggressiveInlining)]
		public void SetIndex(int atIndex, int key, int eIndex)
		{
			keys[atIndex] = key;
			inIndices[atIndex] = eIndex;
		}

		[MethodImpl(AggressiveInlining)]
		public void InputOutputSwap()
		{
			var prev = keys;
			keys = outKeys;
			outKeys = prev;

			prev = inIndices;
			inIndices = outIndices;
			outIndices = prev;
		}
	}

	public static int GetMax(int[] arr, int n)
	{
		int mx = arr[0];
		for(int i = 1; i < n; i++)
			if(arr[i] > mx)
				mx = arr[i];
		return mx;
	}

	// A function to do counting sort of arr[] according to
	// the digit represented by exp.
	public static void CountSort(ref RadixSortList list, int n, int exp)
	{
		int i;
		// initializing all elements of count to 0
		System.Array.Clear(countList, 0, Radix);

		//Count digits
		for(i = 0; i < n; ++i)
			++countList[(list.keys[i] / exp) % Radix];

		//Prefix sum
		for(i = 1; i < Radix; ++i)
			countList[i] += countList[i - 1];

		// Build the output array
		for(i = n-1; i>=0; --i)
		{			
			int outIndex = --countList[(list.keys[i] / exp) % Radix];
			list.outKeys[outIndex] = list.keys[i];
			list.outIndices[outIndex] = list.inIndices[i];
		}

		list.InputOutputSwap();
	}

	//Main method to do RadixSort
	public static void Sort(ref RadixSortList list, int n)
	{
		// Find the maximum number to know number of digits
		int m = GetMax(list.keys, n);

		// Do counting sort for every digit. Note that
		// instead of passing digit number, exp is passed.
		// exp is Radix^i where i is current digit number
		for(int exp = 1; m / exp > 0; exp *= Radix) {
			CountSort(ref list, n, exp);
		}
	}
}
