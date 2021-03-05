using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies the interface ID of an interface type.
	/// </summary>
	/// <remarks>
	/// This is used when querying an interface for an object proxy.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class IidAttribute : Attribute
	{
		public IidAttribute(Guid iid)
		{
			this.Iid = iid;
		}
		public IidAttribute(string iid)
		{
			this.Iid = Guid.Parse(iid);
		}

		public Guid Iid { get; }

		/// <inheritdoc/>
		public sealed override bool Match(object? obj)
			=> (obj is IidAttribute iid) && (iid.Iid == this.Iid);
	}
}
