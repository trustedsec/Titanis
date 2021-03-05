using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	[PduStruct]
	internal partial struct CountedEncodingBlock<T>
		where T : IPduStruct, new()
	{
	}
}
