using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public class ResumeKey
	{
		public const int KeySize = 24;

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct ResumeKeyData
		{
			Guid guid;
			int n1;
			int n2;
		}

		internal readonly ResumeKeyData keyData;

		internal ResumeKey(ref readonly ResumeKeyData data)
		{
			this.keyData = data;
		}
	}
}
