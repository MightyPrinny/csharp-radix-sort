Just an experiment based on code found in geeksforgeeks. That version seemed to be a bit too slow and it wasn't even beating Array.Sort with over 1000000 elements where it's supposed be much better, so I changed the code into this and now it performs much better.

Here's the output of the simple benchmark included Program.cs on a Ryzen 5 1500x in release mode

Preallocated radix sort (add, sort, reset), size: 696969, iterations: 69 tMillis: 4820
Preallocated normal sort (add, sort, reset), size: 696969, iterations: 69 tMillis: 8944