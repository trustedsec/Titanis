using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Winterop.Registry
{
	public interface IRegistryStore
	{
		Task<IRegistryKey> OpenLocalMachine(RegistryAccessRights access, CancellationToken cancellationToken);
	}
}
