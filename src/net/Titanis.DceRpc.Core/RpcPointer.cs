using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Represents a pointer value as an argument to an RPC operation.
	/// </summary>
	public class RpcPointer
	{
		public long referentId;
		public static RpcPointer<T> Create<T>(T value)
			=> new RpcPointer<T>(value);
	}
	/// <summary>
	/// Represents a pointer value as an argument to an RPC operation.
	/// </summary>
	public sealed class RpcPointer<T> : RpcPointer
	{
		public T? value;

		public RpcPointer()
		{

		}
		public RpcPointer(T value)
		{
			this.value = value;
		}
	}
}
