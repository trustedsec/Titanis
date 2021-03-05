using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.DceRpc
{
	public struct RpcInterfaceId
	{
		public readonly SyntaxId syntaxId;

		public RpcInterfaceId(SyntaxId syntaxId)
		{
			this.syntaxId = syntaxId;
		}
		public RpcInterfaceId(Guid uuid, RpcVersion version)
		{
			this.syntaxId = new SyntaxId(uuid, version);
		}

		public override string ToString()
			=> this.syntaxId.ToString();

		public static RpcInterfaceId GetForType(Type interfaceType)
		{
			if (interfaceType is null) throw new ArgumentNullException(nameof(interfaceType));
			if (!interfaceType.IsInterface)
				throw new ArgumentException("interfaceType must be an interface.", nameof(interfaceType));

			var uuid = interfaceType.GUID;
			var versionAttribute = interfaceType.GetCustomAttribute<RpcVersionAttribute>();
			if (versionAttribute == null)
				throw new ArgumentException("The type does not have RpcVersionAttribute.", nameof(interfaceType));

			return new RpcInterfaceId(uuid, versionAttribute.Version);
		}
	}
}
