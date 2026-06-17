using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Registry
{
	/// <summary>
	/// Signals RegistryItemSpec to collect values as a list of names only
	/// </summary>
	/// <remarks>
	/// This was initially introduced to use similar logic for registry key deletion as is used for additions.
	/// </remarks>
	public class ValueNameOnlyAttribute : Attribute
	{
	}
}
