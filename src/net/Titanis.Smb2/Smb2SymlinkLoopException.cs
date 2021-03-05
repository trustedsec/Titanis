using System;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	internal class Smb2SymlinkLoopException : Exception
	{
		public Smb2SymlinkLoopException()
			: base(Messages.Smb2_RecursiveSymlink)
		{
			this.HResult = unchecked((int)Ntstatus.STATUS_STOPPED_ON_SYMLINK);
		}

		public Smb2SymlinkLoopException(string message) : base(message)
		{
			this.HResult = unchecked((int)Ntstatus.STATUS_STOPPED_ON_SYMLINK);
		}

		public Smb2SymlinkLoopException(string message, Exception innerException) : base(message, innerException)
		{
			this.HResult = unchecked((int)Ntstatus.STATUS_STOPPED_ON_SYMLINK);
		}
	}
}