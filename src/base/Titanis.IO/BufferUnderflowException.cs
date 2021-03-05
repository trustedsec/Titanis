using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public class BufferUnderflowException : Exception
	{
		public BufferUnderflowException() { }
		public BufferUnderflowException(string message) : base(message) { }
		public BufferUnderflowException(string message, Exception inner) : base(message, inner) { }
		protected BufferUnderflowException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}


}
