using System;

namespace Titanis.Smb2
{
	[Serializable]
	public class PartialWriteException : Exception
	{
		public PartialWriteException(int bytesWritten)
			: base($"Not all of the data was written to the stream. (bytesWritten = {bytesWritten})")
		{
			this.BytesWritten = bytesWritten;
		}

		public int BytesWritten { get; }

		public PartialWriteException(string? message) : base(message)
		{
		}

		public PartialWriteException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}