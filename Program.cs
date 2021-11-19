using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using System.Diagnostics;

public static class StopWatchExt
{
	[MethodImpl(AggressiveInlining)]
	public static void StopShowMillis(this Stopwatch sw, string msg)
	{
		sw.Stop();
		Console.WriteLine($"{msg} tMillis: {sw.ElapsedMilliseconds}");
	} 
	
	[MethodImpl(AggressiveInlining)]
	public static void ResetStart(this Stopwatch sw)
	{
		sw.Reset();
		sw.Start();
	} 
}
class Program {
	public static void Main()
	{
		RadixBench();
	}

	class Stuff
	{
		public int thing;
		public float otherThing;
	}

	public static void RadixTestIfWorks(int size = 20)
	{
		var rng = new System.Random();
		var list = new RadixSortedList<Stuff>(size);
		

		for(int i=0; i<size; ++i)
		{
			int key = rng.Next()%666;
			list.AddElement(new Stuff(){thing = 23, otherThing = key}, key);
		}

		//var sw = new Stopwatch();
		//sw.ResetStart();
		list.Commit();

		
		for(int i=0; i<list.count; ++i)
		{
			var element = list[i];
			Console.WriteLine($"Sorted element's key: {element.otherThing}, index{i}, key: {list.list.keys[i]}");
		}
	}

	public static void RadixBench(int size =696969, int its = 69)
	{
		var rng = new System.Random();
		var list = new RadixSortedList<Stuff>(size);
		
		var sw = new Stopwatch();

		sw.ResetStart();
		for(int it =its; it >= 0; --it)
		{
			for(int i=0; i<size; ++i)
			{
				int key = rng.Next()%666;
				list.AddElement(new Stuff(){thing = 23, otherThing = key}, key);
			}

			//var sw = new Stopwatch();
			//sw.ResetStart();
			list.Commit();
			list.Reset();
		}
		sw.StopShowMillis($"Cached radix sort (add, sort, reset), size: {size}, iterations: {its}");

		var normalList = new NormalSortedList<Stuff>(size);
		
		sw.ResetStart();
		for(int it =its; it >= 0; --it)
		{
			for(int i=0; i<size; ++i)
			{
				int key = rng.Next()%666;
				normalList.AddElement(new Stuff(){thing = 23, otherThing = key}, key);
			}

			//var sw = new Stopwatch();
			//sw.ResetStart();
			normalList.Commit();
			normalList.Reset();
		}
		sw.StopShowMillis($"Cached normal sort (add, sort, reset), size: {size}, iterations: {its}");
	}
}
