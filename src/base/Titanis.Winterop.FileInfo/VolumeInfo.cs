using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VolumeInfo
	{
		public long volumeCreationTime;
		public int volumeSerialNumber;
		public int volumeLabelLength;
		public byte supportsObjects;
		public byte reserved;
	}
}
