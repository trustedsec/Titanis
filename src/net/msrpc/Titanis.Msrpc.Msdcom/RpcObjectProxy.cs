using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Msrpc.Msdcom;

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Implements an object proxy.
	/// </summary>
	public abstract class RpcObjectProxy : RpcClientProxy, IRpcObject, IRpcObjectProxy
	{
		protected RpcObjectProxy()
		{
		}

		public Guid Ipid { get; private set; }
		public COMVERSION ComVersion { get; private set; }
		internal IObjrefMarshal? Dcom { get; private set; }

		/// <summary>
		/// Gets the IID associated with an object proxy.
		/// </summary>
		/// <typeparam name="TProxy">Type derived from <see cref="RpcObjectProxy"/>.</typeparam>
		/// <returns>A <see cref="Guid"/> set as the IID on <typeparamref name="TProxy"/>.</returns>
		/// <exception cref="InvalidOperationException"><typeparamref name="TProxy"/> does not have <see cref="IidAttribute"/>.</exception>
		public static Guid GetProxyIid<TProxy>() where TProxy : IRpcObjectProxy, new()
		{
			var attr = typeof(TProxy).GetCustomAttribute<IidAttribute>();
			if (attr == null)
				throw new InvalidOperationException($"The type '{typeof(TProxy).FullName} does nat have the IidAttribute attribute.");

			return attr.Iid;
		}

		internal void SetObjectInfo(
			Guid ipid,
			COMVERSION comVersion,
			IObjrefMarshal dcom
			)
		{
			this.Ipid = ipid;
			this.ComVersion = comVersion;
			this.Dcom = dcom;
		}

		public Task<TTarget> QueryInterface<TTarget>(CancellationToken cancellationToken)
			where TTarget : class, IRpcObject
			=> this.Dcom.QueryInterface<TTarget>(this, cancellationToken);

		protected sealed override RpcRequestBuilder CreateRequest(ushort opnum)
		{
			this.EnsureBound();
			var req = new RpcRequestBuilder(opnum, this._bindContext.encoding, new RpcCallContext(this.Dcom), this.Ipid);

			// [MS-DCOM] § 2.2.13.3
			var orpcThis = new ms_dcom.ORPCTHIS()
			{
				version = this.ComVersion,
				cid = Guid.NewGuid(),
			};
			req.StubData.WriteFixedStruct(orpcThis, NdrAlignment.NativePtr);
			req.StubData.WriteStructDeferral(orpcThis);

			return req;
		}

		private static readonly Guid clsidErrorExtension = new Guid("0000031c-0000-0000-c000-000000000046");
		private static readonly Guid clsidErrorInfo = new Guid("0000031b-0000-0000-c000-000000000046");
		protected sealed override async Task<RpcDecoder> SendRequestAsync(IRpcRequestBuilder stubData, CancellationToken cancellationToken)
		{
			var req = await base.SendRequestAsync(stubData, cancellationToken).ConfigureAwait(false);
			var that = req.ReadFixedStruct<ms_dcom.ORPCTHAT>(NdrAlignment.NativePtr);
			that.DecodeDeferrals(req);

			DcomClient.ClearLastError();

			var exts = that.extensions?.value.extent?.value;
			if (exts != null)
			{
				foreach (var pExt in exts)
				{
					if (pExt != null)
					{
						if (pExt.value.id == clsidErrorExtension)
						{
							var bytes = pExt.value.data;
							var unwrapped = await new TypedObjref<IRpcObject>(bytes).Unwrap(this.Dcom, cancellationToken).ConfigureAwait(false);
							DcomClient.SetLastError(unwrapped);
						}
						else
						{
							;
						}
					}
				}
			}

			return req;
		}

		internal static Type GetProxyTypeFor<TInterface>() where TInterface : class, IRpcObject
		{
			// TODO: Do this with an attribute
			var itfType = typeof(TInterface);
			var proxyTypeName = itfType.FullName + "ClientProxy";
			var proxyType = itfType.Assembly.GetType(proxyTypeName, true);
			return proxyType;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	public struct ORPC_EXTENT : Titanis.DceRpc.IRpcConformantStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.id);
			encoder.WriteValue(this.size);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.id = decoder.ReadUuid();
			this.size = decoder.ReadUInt32();
		}
		public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.data);
		}
		public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.data = decoder.ReadArrayHeader<byte>();
		}
		public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.data.Length); i++
			)
			{
				byte elem_0 = this.data[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder)
		{
			for (int i = 0; (i < this.data.Length); i++
			)
			{
				byte elem_0 = this.data[i];
				elem_0 = decoder.ReadByte();
				this.data[i] = elem_0;
			}
		}
		public System.Guid id;
		public uint size;
		public byte[] data;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	public struct ORPC_EXTENT_ARRAY : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.size);
			encoder.WriteValue(this.reserved);
			encoder.WritePointer(this.extent);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.size = decoder.ReadUInt32();
			this.reserved = decoder.ReadUInt32();
			this.extent = decoder.ReadPointer<RpcPointer<ORPC_EXTENT>[]>();
		}
		public uint size;
		public uint reserved;
		public RpcPointer<RpcPointer<ORPC_EXTENT>[]> extent;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.extent))
			{
				encoder.WriteArrayHeader(this.extent.value);
				for (int i = 0; (i < this.extent.value.Length); i++
				)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					encoder.WritePointer(elem_0);
				}
				for (int i = 0; (i < this.extent.value.Length); i++
				)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					if ((null != elem_0))
					{
						encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
						encoder.WriteStructDeferral(elem_0.value);
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.extent))
			{
				this.extent.value = decoder.ReadArrayHeader<RpcPointer<ORPC_EXTENT>>();
				for (int i = 0; (i < this.extent.value.Length); i++
				)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					elem_0 = decoder.ReadPointer<ORPC_EXTENT>();
					this.extent.value[i] = elem_0;
				}
				for (int i = 0; (i < this.extent.value.Length); i++
				)
				{
					RpcPointer<ORPC_EXTENT> elem_0 = this.extent.value[i];
					if ((null != elem_0))
					{
						elem_0.value = decoder.ReadConformantStruct<ORPC_EXTENT>(Titanis.DceRpc.NdrAlignment._4Byte);
						decoder.ReadStructDeferral<ORPC_EXTENT>(ref elem_0.value);
					}
					this.extent.value[i] = elem_0;
				}
			}
		}
	}
}
