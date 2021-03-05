using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	public class WmiReference
	{
		public WmiReference(string path)
		{
			this.Path = path;
		}

		public string Path { get; }

		public sealed override string ToString()
			=> this.Path;
	}
}
