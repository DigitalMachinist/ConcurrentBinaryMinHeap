ConcurrentBinaryHeap
====================

A .NET 3.5 C# implementation of a thread-safe binary min-heap, suitable for implementation of a priority queue and compatible with MonoGame/Unity3D.

## Overview

This is simply a thread-safe generic binary min-heap implemented in C#. It was designed to be compatible with .NET Framework 3.5 primarily for use in Unity3D game projects where pathfinding and/or priority queues might require a binary min-heap to operate efficiently.

*This heap does not guarantee FIFO ordering of elements with the same priority value.*

Thread safety is achieved by using System.Monitor to lock critical sections whenever appropriate, since more sophisticated options such as SpinLock/SpinWait are not available without .NET Framework 4.0 support.

This binary min-heap implementation is inspired by the work of Alexey Kurakin on [codeproject.org](http://www.codeproject.com/Articles/126751/Priority-queue-in-C-with-the-help-of-heap-data-str), so partial credit for this project goes to him.

## Computational Complexity

As should be the case for a binary heap data structure, this implementation has logarithmic time-complexity for push and pop operations. Peeking at the root element requires only constant time. Less common operations are not guaranteed to be so efficient, but no operation should be worse than O(n) in the worst case.

| Operation | Average   | Worst Case   |
|-----------|-----------|--------------|
| Push      | O(log(n)) | O(log(n))    |
| Pop       | O(log(n)) | O(log(n))    |
| Peek      | O(1)      | O(1)         |

## Documentation

Doxygen HTML docs are provided and are relatively complete.

## Tests

A NUnit test project is included with full unit test coverage, excluding a couple of low-level functions that are private in release build anyway.

As of the time of writing, I have not yet written any tests to validate concurrency to be 100% sure that this code is thread-safe.

If I missed anything, please let me know or submit a PR.

## Developer

Written by Jeff Rose (jrose0@gmail.com)

## License

Licensed under the MIT License. See the LICENCE file for more information.
