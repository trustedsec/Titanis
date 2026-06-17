using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Registry
{
	public class RegistryKeyGroup
	{
		public readonly RegistryKeySpec key;

		public RegistryAccessRights access;

		public RegistryKeyGroup(RegistryKeySpec key)
		{
			this.key = key;
		}

		public readonly List<RegistryValueSpec> values = new List<RegistryValueSpec>();
	}
}
