using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axon.Collections
{
	public class PriorityValuePair<TValue>
	{
		#region Instance members

		/// <summary>
		/// The floating-point value indicating the priority value of this pair. Typically this 
		/// affects how it will be sorted in a binary heap or priority queue.
		/// </summary>
		public float Priority { get; set; }


		/// <summary>
		/// A generically-typed value that may contain any kind of data.
		/// </summary>
		public TValue Value { get; set; }

		#endregion


		#region Constructors

		/// <summary>
		/// Create a new default priority-value pair.
		/// </summary>
		public PriorityValuePair()
		{
			Priority = 0f;
			Value = default( TValue );
		}


		/// <summary>
		/// Create a new priority-value pair by specifying its initial priority and value.
		/// </summary>
		/// <param name="priority">The floating-point value indicating the priority value of this 
		/// pair. Typically this affects how it will be sorted in a binary heap or priority queue.
		/// </param>
		/// <param name="value">A generically-typed value that may contain any kind of data.
		/// </param>
		public PriorityValuePair( float priority, TValue value )
		{
			Priority = priority;
			Value = value;
		}

		#endregion
	}
}
