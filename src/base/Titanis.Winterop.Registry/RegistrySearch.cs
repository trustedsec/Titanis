using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Registry
{
	public static class RegistrySearch
	{
		public static void Search(IRegistryKey key)
		{
			ArgumentNullException.ThrowIfNull(key);
		}
	}
}
