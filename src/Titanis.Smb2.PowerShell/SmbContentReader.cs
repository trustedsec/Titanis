using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.PowerShell
{
	internal class SmbContentReader : IContentReader
	{
		private readonly Smb2FileStream _stream;
		private readonly int _chunkSize;

		internal SmbContentReader(Smb2FileStream stream, int chunkSize)
		{
			Debug.Assert(stream != null);
			Debug.Assert(stream.CanRead);

			this._stream = stream;
			this._chunkSize = chunkSize;
		}

		public void Close()
		{
			this._stream.Close();
		}

		public void Dispose()
		{
			this._stream.Dispose();
		}

		public IList Read(long readCount)
		{
			throw new NotImplementedException();
		}

		public void Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
	}
}
