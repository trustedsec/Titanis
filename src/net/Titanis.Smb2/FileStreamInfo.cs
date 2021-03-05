using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public struct FileStreamInfo
	{
		public FileStreamInfo(string name, long size, long allocationSize)
		{
			this.Name = name;
			this.Size = size;
			this.AllocationSize = allocationSize;
		}

		public string Name { get; }
		public long Size { get; set; }
		public long AllocationSize { get; }
	}
}
