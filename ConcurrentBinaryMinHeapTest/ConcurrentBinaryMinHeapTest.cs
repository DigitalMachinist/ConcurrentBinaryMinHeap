using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Axon.Collections
{
    [TestFixture]
    public class ConcurrentBinaryMinHeapTest
    {
        #region Instance members

        [Test]
        public void PropertyCapacity()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>( 15 );

            // Ensure that Capacity reports 15.
            Assert.That( heap.Capacity, Is.EqualTo( 15 ) );

            // Intentionally over-fill the queue to force it to resize.
            for ( int i = 0; i < 16; i++ )
            {
                heap.Push( 1f, 1 );
            }

            // Ensure that Capacity is now greater than 15.
            Assert.That( heap.Capacity, Is.GreaterThan( 15 ) );
        }


        [Test]
        public void PropertyCount()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that Count reports 0.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Enqueue 3 elements in the queue.
            heap.Push( 1f, 1 );
            heap.Push( 3f, 3 );
            heap.Push( 2f, 2 );

            // Ensure that Count now reports 3.
            Assert.That( heap.Count, Is.EqualTo( 3 ) );
        }


        [Test]
        public void PropertyIsEmpty()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that IsEmpty reports TRUE.
            Assert.That( heap.IsEmpty, Is.True );

            // Enqueue an element in the queue.
            heap.Push( 1f, 1 );

            // Ensure that IsEmpty now reports FALSE.
            Assert.That( heap.IsEmpty, Is.False );
        }


        [Test]
        public void PropertyIsReadOnly()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that IsReadOnly always reports FALSE.
            Assert.That( heap.IsReadOnly, Is.False );
        }

        #endregion


        #region Constructors

        [Test]
        public void ConstructorParameterless()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Nothing to test here. The following explicitly passes this test:
            Assert.That( true, Is.True );
        }


        [Test]
        public void ConstructorInitialSize()
        {
            // Try to create a heap with a negative initial size and expect an 
			// ArgumentOutOfRangeException to be thrown.
			Assert.Throws<ArgumentOutOfRangeException>( () => {
				new ConcurrentBinaryMinHeap<int>( -10 );
			} );

            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>( 15 );

            // Ensure that Capacity reports 15.
            Assert.That( heap.Capacity, Is.EqualTo( 15 ) );
        }

        #endregion


        #region Public API

        [Test]
        public void Add() {

            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Call Add() to insert a new element to the queue as a PriorityValuePair.
            heap.Add( new PriorityValuePair<int>( 1f, 2 ) );

            // Expect a value of 2 on the first item to be removed after adding it.
            Assert.That( heap.PopValue(), Is.EqualTo( 2 ) );
        }


        [Test]
        public void Clear() {

            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Push 3 elements onto the heap.
            heap.Push( 1f, 2 );
            heap.Push( 3f, 6 );
            heap.Push( 2f, 4 );

            // Ensure that 3 elements have been added to the heap.
            Assert.That( heap.Count, Is.EqualTo( 3 ) );

            // Clear the heap.
            heap.Clear();

            // Ensure that all of the elements have been removed.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );
        }


        [Test]
        public void Contains() {

            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Create and store a new element.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );

            // Ensure the queue contains the element.
            Assert.That( heap.Contains( elem ), Is.False );

            // Push it onto the heap.
            heap.Push( elem );

            // Ensure the queue now contains the element.
            Assert.That( heap.Contains( elem ), Is.True );
        }


        [Test]
        public void CopyTo() 
		{
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Create a new array of size 5.
            PriorityValuePair<int>[] arrayCopy = new PriorityValuePair<int>[ 5 ];

            // Push 3 elements onto the queue.
			PriorityValuePair<int> elem = new PriorityValuePair<int>( 3f, 6 );
            heap.Push( 1f, 2 );
            heap.Push( elem );
            heap.Push( 2f, 4 );

            // Copy the heap data to the array, starting from index 1 (not 0).
            heap.CopyTo( arrayCopy, 1 );

            // Expect the first array index to be unset, but all the rest to be set.
			// Note: The order of elements after the first can't be guaranteed, because the heap 
			// doesn't store things in an exact linear order, but we can be sure that the elements 
			// aren't going to be equal to null because we set them.
            Assert.That( arrayCopy[ 0 ], Is.EqualTo( null ) );
            Assert.That( arrayCopy[ 1 ], Is.EqualTo( elem ) );
            Assert.That( arrayCopy[ 2 ], Is.Not.EqualTo( null ) );
            Assert.That( arrayCopy[ 3 ], Is.Not.EqualTo( null ) );
            Assert.That( arrayCopy[ 4 ], Is.EqualTo( null ) );
        }


        [Test]
        public void GetEnumerator() 
		{
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Enqueue a few elements in the queue.
            heap.Push( 1f, 2 );
            heap.Push( 3f, 6 );
            heap.Push( 2f, 4 );

            // Use the enumerator of heap (using disposes it when we're finished).
            using ( IEnumerator< PriorityValuePair<int> > enumerator = heap.GetEnumerator() )
            {
                // Expect the first element to have the highest priority, and expect MoveNext() to 
				// return true until the last element. After the end of the heap is reached, it 
				// then returns false.
				// Note: Since the heap doesn't guarantee the order of elements after the first, we 
				// can only be certain of the root element and after that we really can't be sure 
				// of the order -- just the length.
                Assert.That( enumerator.MoveNext(), Is.True );
                Assert.That( enumerator.Current.Value, Is.EqualTo( 6 ) );
                Assert.That( enumerator.MoveNext(), Is.True );
                Assert.That( enumerator.MoveNext(), Is.True );
                Assert.That( enumerator.MoveNext(), Is.False );
            }
        }


        [Test]
        public void Peek()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Expect Peek() to return null for an empty heap.
			Assert.That( heap.Peek(), Is.EqualTo( null ) );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem1 = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem1 );

            // Ensure that the element was inserted into the heap as the root element.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
            Assert.That( heap.Peek(), Is.EqualTo( elem1 ) );

            // Ensure that the element was not removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );

            // Insert another element with higher priority than the last.
            PriorityValuePair<int> elem2 = new PriorityValuePair<int>( 2f, 4 );
            heap.Push( elem2 );

            // Ensure that Peak() returns the new root element.
            Assert.That( heap.Peek(), Is.EqualTo( elem2 ) );
        }


        [Test]
        public void PeekPriority()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Try to PeekPriority() and expect an NullReferenceException to be thrown.
			Assert.Throws<NullReferenceException>( () => {
				heap.PeekPriority();
			} );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

            // Ensure that the priority of the pushed element is returned.
            Assert.That( heap.PeekPriority(), Is.EqualTo( 1f ) );

            // Ensure that the element was not removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
        }


        [Test]
        public void PeekValue()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Try to PeekValue() and expect an NullReferenceException to be thrown.
			Assert.Throws<NullReferenceException>( () => {
				heap.PeekValue();
			} );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

            // Ensure that the priority of the pushed element is returned.
            Assert.That( heap.PeekValue(), Is.EqualTo( 2 ) );

            // Ensure that the element was not removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
        }


        [Test]
        public void Pop()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Expect Pop() to return null for an empty heap.
			Assert.That( heap.Pop(), Is.EqualTo( null ) );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Count, Is.EqualTo( 1 ) );
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

            // Ensure that the returned element points to the same object we stored earlier.
            Assert.That( heap.Pop(), Is.EqualTo( elem ) );

            // Ensure that the element was removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );
        }


		[Test]
        public void PopPriority()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Try to PopPriority() and expect an NullReferenceException to be thrown.
			Assert.Throws<NullReferenceException>( () => {
				heap.PopPriority();
			} );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

            // Ensure that the priority of the pushed element is returned.
            Assert.That( heap.PopPriority(), Is.EqualTo( 1f ) );

            // Ensure that the element was removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );
        }


		[Test]
        public void PopValue()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Try to PopPriority() and expect an NullReferenceException to be thrown.
			Assert.Throws<NullReferenceException>( () => {
				heap.PopValue();
			} );

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

            // Ensure that the value of the pushed element is returned.
            Assert.That( heap.PopValue(), Is.EqualTo( 2 ) );

            // Ensure that the element was removed from the heap.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );
        }


        [Test]
        public void PushElement()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that the heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            PriorityValuePair<int> elem = new PriorityValuePair<int>( 1f, 2 );
            heap.Push( elem );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );

			// Store another element with higher priority and insert it as well.
			elem = new PriorityValuePair<int>( 2f, 4 );
            heap.Push( elem );
			
            // Ensure that the element was inserted into the queue and is at the root.
            Assert.That( heap.Peek(), Is.EqualTo( elem ) );
        }


        [Test]
        public void PushPriorityValue()
        {
            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Ensure that heap is empty.
            Assert.That( heap.Count, Is.EqualTo( 0 ) );

            // Store an element and insert it into the heap.
            heap.Push( 1f, 2 );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.PeekValue(), Is.EqualTo( 2 ) );

			// Store another element with higher priority and insert it as well.
            heap.Push( 2f, 4 );

            // Ensure that the element was inserted into the heap.
            Assert.That( heap.PeekValue(), Is.EqualTo( 4 ) );
        }


        [Test]
        public void Remove() {

            // Create a new heap.
            ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

            // Create and store a few elements.
            PriorityValuePair<int> elem1 = new PriorityValuePair<int>( 1f, 2 );
            PriorityValuePair<int> elem2 = new PriorityValuePair<int>( 2f, 4 );
            PriorityValuePair<int> elem3 = new PriorityValuePair<int>( 3f, 6 );

            // Expect Remove() to return null for an empty heap.
			Assert.That( heap.Remove( elem1 ), Is.EqualTo( false ) );

            // Insert 2 of the elements into the heap.
            heap.Push( elem2 );
            heap.Push( elem3 );

            // Expect Remove() to return false for elem1, indicating the element was removed
			// (since it doesn't belong to the heap and can't be found). This tests the if-else 
			// case for when the provided element isn't found in the heap.
            Assert.That( heap.Remove( elem1 ), Is.False );

            // Expect Remove() to return true for elem2, indicating the element was removed
			// (since it belongs to the heap and can be found). This tests the if-else case for 
			// when Count is 2 or greater.
            Assert.That( heap.Remove( elem2 ), Is.True );

            // Expect Remove() to return true for elem3, indicating the element was removed
			// (since it belongs to the heap and can be found). This tests the if-else case for 
			// when Count equals 1.
            Assert.That( heap.Remove( elem3 ), Is.True );
        }

        #endregion

		
		#if DEBUG
        #region Private methods (these are testable as public methods in DEBUG builds)

		[Test]
		public void SwapElements()
		{
			// Create a new heap.
			ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

			// Enqueue an element into the queue.
			var elem1 = new PriorityValuePair<int>( 2f, 4 );
			heap.Push( elem1 );

			// Ensure that the element was inserted.
			Assert.That( heap.Count, Is.EqualTo( 1 ) );
			Assert.That( heap.Peek(), Is.EqualTo( elem1 ) );

			// Try to HeapSwapElements() while the queue only contains 1 element and expect an
			// InvalidOperationException to be thrown.
			Assert.Throws<InvalidOperationException>( () => {
				heap.SwapElements( 0, 1 );
			} );

			// Enqueue another element with higher priority than the last.
			var elem2 = new PriorityValuePair<int>( 1f, 2 );
			heap.Push( elem2 );

			// Ensure that the element was inserted and that the 1st (higher priority) element is 
			// still at the root of the heap.
			Assert.That( heap.Count, Is.EqualTo( 2 ) );
			Assert.That( heap.Peek(), Is.EqualTo( elem1 ) );

			// Try to HeapSwapElements() with an invalid index1 and expect an
			// ArgumentOutOfRangeException to be thrown.
			Assert.Throws<ArgumentOutOfRangeException>( () => {
				heap.SwapElements( -1, 1 );
			} );

			// Try to HeapSwapElements() with an invalid index2 and expect an
			// ArgumentOutOfRangeException to be thrown.
			Assert.Throws<ArgumentOutOfRangeException>( () => {
				heap.SwapElements( 0, -1 );
			} );

			// Actually swap elements now.
			heap.SwapElements( 0, 1 );

			// Ensure that the elements were swapped.
			Assert.That( heap.Count, Is.EqualTo( 2 ) );
			Assert.That( heap.Peek(), Is.EqualTo( elem2 ) );
			Assert.That( heap.Contains( elem1 ), Is.True );
		}


		[Test]
		[Ignore]
		public void HeapifyBottomUp()
		{
			// TODO The HeapifyBottomUp() test is incomplete.

			// Create a new heap.
			ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

			// Execute several HeapifyBottomUp()s to test different tree operations on the heap.
			var index = 0;
			heap.HeapifyBottomUp( index );
		}


		[Test]
		[Ignore]
		public void HeapifyTopDown()
		{
			// TODO The HeapifyTopDown() test is incomplete.

			// Create a new heap.
			ConcurrentBinaryMinHeap<int> heap = new ConcurrentBinaryMinHeap<int>();

			// Execute several HeapifyBottomUp()s to test different tree operations on the heap.
			var index = 0;
			heap.HeapifyTopDown( index );
		}

        #endregion
		#endif
	}
}