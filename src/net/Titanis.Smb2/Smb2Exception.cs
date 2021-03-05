using System;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	/// <summary>
	/// Thrown when the provided buffer is too small.
	/// </summary>
	/// <remarks>
	/// Check <see cref="RequiredBufferSize"/> for the required buffer size.
	/// </remarks>
	/// <seealso cref="Ntstatus.STATUS_BUFFER_TOO_SMALL"/>
	public class Smb2BufferTooSmallException : NtstatusException
	{
		internal Smb2BufferTooSmallException(int requiredBufferSize)
			: base(Ntstatus.STATUS_BUFFER_TOO_SMALL)
		{
			this.RequiredBufferSize = requiredBufferSize;
		}

		/// <summary>
		/// Gets the required buffer size.
		/// </summary>
		public int RequiredBufferSize { get; }
	}

	/// <summary>
	/// Thrown when a symlink is encountered.
	/// </summary>
	/// <remarks>
	/// <see cref="Smb2Client"/> handles this exception internally to follow a symlink.
	/// </remarks>
	/// <seealso cref="Ntstatus.STATUS_STOPPED_ON_SYMLINK"/>
	public class Smb2SymlinkException : NtstatusException
	{
		internal Smb2SymlinkException(in Pdus.Smb2ErrorInfo errorInfo)
			: base(Ntstatus.STATUS_STOPPED_ON_SYMLINK)
		{
			this.SymlinkInfo = errorInfo.symlink;
		}

		/// <summary>
		/// Gets additional information about the symlink.
		/// </summary>
		public SymbolicLinkInfo? SymlinkInfo { get; }
	}
}