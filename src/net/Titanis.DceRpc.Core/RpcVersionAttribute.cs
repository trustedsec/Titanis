using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies the version of an RPC interface.
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class RpcVersionAttribute : Attribute
	{
		public RpcVersionAttribute(ushort major, ushort minor)
		{
			this.Version = new RpcVersion(major, minor);
		}

		public RpcVersion Version { get; }

		/// <inheritdoc/>
		public sealed override bool Match(object? obj)
			=> (obj is RpcVersionAttribute other) && (other.Version == this.Version);
	}
}
