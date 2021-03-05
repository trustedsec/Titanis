using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.IO
{
	/// <summary>
	/// Thrown on an attempt to read from a stream when the stream does not contain enough data.
	/// </summary>
	/// <seealso cref="StreamExtensions.ReadAtLeast(System.IO.Stream, byte[], int, int, int)"/>
	/// <remarks>
	/// The caller can obtain the actual number of bytes read from <see cref="BytesRead"/>.
	/// </remarks>
	[Serializable]
	public class StreamUnderflowException : Exception
	{
		private const int STATUS_END_OF_MEDIA = unchecked((int)0x8000001E);

		/// <summary>
		/// Initializes a new <see cref="StreamUnderflowException"/>.
		/// </summary>
		/// <param name="bytesRead">Number of bytes read</param>
		public StreamUnderflowException(int bytesRead) : base(Messages.StreamUnderflowMessage)
		{
			this.HResult = STATUS_END_OF_MEDIA;
			this.BytesRead = bytesRead;
		}

		/// <summary>
		/// Initializes a new <see cref="StreamUnderflowException"/>.
		/// </summary>
		/// <param name="bytesRead">Number of bytes read</param>
		/// <param name="message">Message describing the error</param>
		public StreamUnderflowException(int bytesRead, string message) : base(message)
		{
			this.HResult = STATUS_END_OF_MEDIA;
			this.BytesRead = bytesRead;
		}
		/// <summary>
		/// Initializes a new <see cref="StreamUnderflowException"/>.
		/// </summary>
		/// <param name="bytesRead">Number of bytes read</param>
		/// <param name="message">Message describing the error</param>
		/// <param name="inner">Exception that caused the current exception</param>
		public StreamUnderflowException(int bytesRead, string message, Exception inner) : base(message, inner)
		{
			this.HResult = STATUS_END_OF_MEDIA;
			this.BytesRead = bytesRead;
		}
		/// <summary>
		/// Initializes a new <see cref="StreamUnderflowException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected StreamUnderflowException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.BytesRead = info.GetInt32(nameof(BytesRead));
		}

		/// <summary>
		/// Gets the number of bytes read from the stream.
		/// </summary>
		public int BytesRead { get; }

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(BytesRead), this.BytesRead);
			base.GetObjectData(info, context);
		}
	}
}
