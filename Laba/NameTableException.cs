﻿namespace Laba
{
	[System.Serializable]
	public class NameTableException : System.Exception
	{
		/// <summary>
		/// Создаёт объект типа <see cref="T:NameTableException"/> class
		/// </summary>
		public NameTableException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NameTableException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public NameTableException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NameTableException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public NameTableException(string message, System.Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NameTableException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected NameTableException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}

}