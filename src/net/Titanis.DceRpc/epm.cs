#pragma warning disable

namespace epm
{
	using System;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	public struct ept_entry_t : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.@object);
			encoder.WritePointer(this.tower);
			for (int i = 0; (i < this.annotation.Count); i++
			)
			{
				byte elem_0 = this.annotation.Item(i);
				encoder.WriteValue(elem_0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.@object = decoder.ReadUuid();
			this.tower = decoder.ReadPointer<dceidl.twr_t>();
			this.annotation = decoder.ReadArraySegmentHeader<byte>(64);
			for (int i = 0; (i < this.annotation.Count); i++
			)
			{
				byte elem_0 = this.annotation.Item(i);
				elem_0 = decoder.ReadUnsignedChar();
				this.annotation.Item(i) = elem_0;
			}
		}
		public System.Guid @object;
		public RpcPointer<dceidl.twr_t> tower;
		public ArraySegment<byte> annotation;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.tower))
			{
				encoder.WriteConformantStruct(this.tower.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(this.tower.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.tower))
			{
				this.tower.value = decoder.ReadConformantStruct<dceidl.twr_t>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<dceidl.twr_t>(ref this.tower.value);
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	[System.Runtime.InteropServices.GuidAttribute("e1af8308-5d1f-11c9-91a4-08002b14a0fa")]
	[Titanis.DceRpc.RpcVersionAttribute(3, 0)]
	public interface ept
	{
		Task ept_insert(uint num_ents, ept_entry_t[] entries, uint replace, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		Task ept_delete(uint num_ents, ept_entry_t[] entries, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		Task ept_lookup(uint inquiry_type, RpcPointer<System.Guid> @object, RpcPointer<Titanis.DceRpc.RpcInterfaceId> Ifid, uint vers_option, RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, uint max_ents, RpcPointer<uint> num_ents, RpcPointer<ArraySegment<ept_entry_t>> entries, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		[Titanis.DceRpc.IdempotentAttribute()]
		Task ept_map(RpcPointer<System.Guid> @object, RpcPointer<dceidl.twr_t> map_tower, RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, uint max_towers, RpcPointer<uint> num_towers, RpcPointer<ArraySegment<RpcPointer<dceidl.twr_t>>> towers, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		Task ept_lookup_handle_free(RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		[Titanis.DceRpc.IdempotentAttribute()]
		Task ept_inq_object(RpcPointer<System.Guid> ept_object, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
		Task ept_mgmt_delete(uint object_speced, RpcPointer<System.Guid> @object, RpcPointer<dceidl.twr_t> tower, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	[Titanis.DceRpc.IidAttribute("e1af8308-5d1f-11c9-91a4-08002b14a0fa")]
	public class eptClientProxy : Titanis.DceRpc.Client.RpcClientProxy, ept, Titanis.DceRpc.IRpcClientProxy
	{
		private static System.Guid _interfaceUuid = new System.Guid("e1af8308-5d1f-11c9-91a4-08002b14a0fa");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(3, 0);
			}
		}
		public virtual async Task ept_insert(uint num_ents, ept_entry_t[] entries, uint replace, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(num_ents);
			if ((entries != null))
			{
				encoder.WriteArrayHeader(entries);
				for (int i = 0; (i < entries.Length); i++
				)
				{
					ept_entry_t elem_0 = entries[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
			}
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				encoder.WriteStructDeferral(elem_0);
			}
			encoder.WriteValue(replace);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			status.value = decoder.ReadInt32();
		}
		public virtual async Task ept_delete(uint num_ents, ept_entry_t[] entries, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(num_ents);
			if ((entries != null))
			{
				encoder.WriteArrayHeader(entries);
				for (int i = 0; (i < entries.Length); i++
				)
				{
					ept_entry_t elem_0 = entries[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
			}
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				encoder.WriteStructDeferral(elem_0);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			status.value = decoder.ReadInt32();
		}
		public virtual async Task ept_lookup(uint inquiry_type, RpcPointer<System.Guid> @object, RpcPointer<Titanis.DceRpc.RpcInterfaceId> Ifid, uint vers_option, RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, uint max_ents, RpcPointer<uint> num_ents, RpcPointer<ArraySegment<ept_entry_t>> entries, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(inquiry_type);
			encoder.WritePointer(@object);
			if ((null != @object))
			{
				encoder.WriteValue(@object.value);
			}
			encoder.WritePointer(Ifid);
			if ((null != Ifid))
			{
				encoder.WriteValue(Ifid.value);
			}
			encoder.WriteValue(vers_option);
			encoder.WriteContextHandle(entry_handle.value);
			encoder.WriteValue(max_ents);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			entry_handle.value = decoder.ReadContextHandle();
			num_ents.value = decoder.ReadUInt32();
			entries.value = decoder.ReadArraySegmentHeader<ept_entry_t>();
			for (int i = 0; (i < entries.value.Count); i++
			)
			{
				ept_entry_t elem_0 = entries.value.Item(i);
				elem_0 = decoder.ReadFixedStruct<ept_entry_t>(Titanis.DceRpc.NdrAlignment.NativePtr);
				entries.value.Item(i) = elem_0;
			}
			for (int i = 0; (i < entries.value.Count); i++
			)
			{
				ept_entry_t elem_0 = entries.value.Item(i);
				decoder.ReadStructDeferral<ept_entry_t>(ref elem_0);
				entries.value.Item(i) = elem_0;
			}
			status.value = decoder.ReadInt32();
		}
		[Titanis.DceRpc.IdempotentAttribute()]
		public virtual async Task ept_map(RpcPointer<System.Guid> @object, RpcPointer<dceidl.twr_t> map_tower, RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, uint max_towers, RpcPointer<uint> num_towers, RpcPointer<ArraySegment<RpcPointer<dceidl.twr_t>>> towers, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(@object);
			if ((null != @object))
			{
				encoder.WriteValue(@object.value);
			}
			encoder.WritePointer(map_tower);
			if ((null != map_tower))
			{
				encoder.WriteConformantStruct(map_tower.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(map_tower.value);
			}
			encoder.WriteContextHandle(entry_handle.value);
			encoder.WriteValue(max_towers);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			entry_handle.value = decoder.ReadContextHandle();
			num_towers.value = decoder.ReadUInt32();
			towers.value = decoder.ReadArraySegmentHeader<RpcPointer<dceidl.twr_t>>();
			for (int i = 0; (i < towers.value.Count); i++
			)
			{
				RpcPointer<dceidl.twr_t> elem_0 = towers.value.Item(i);
				elem_0 = decoder.ReadPointer<dceidl.twr_t>();
				towers.value.Item(i) = elem_0;
			}
			for (int i = 0; (i < towers.value.Count); i++
			)
			{
				RpcPointer<dceidl.twr_t> elem_0 = towers.value.Item(i);
				if ((null != elem_0))
				{
					elem_0.value = decoder.ReadConformantStruct<dceidl.twr_t>(Titanis.DceRpc.NdrAlignment._4Byte);
					decoder.ReadStructDeferral<dceidl.twr_t>(ref elem_0.value);
				}
				towers.value.Item(i) = elem_0;
			}
			status.value = decoder.ReadInt32();
		}
		public virtual async Task ept_lookup_handle_free(RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(entry_handle.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			entry_handle.value = decoder.ReadContextHandle();
			status.value = decoder.ReadInt32();
		}
		[Titanis.DceRpc.IdempotentAttribute()]
		public virtual async Task ept_inq_object(RpcPointer<System.Guid> ept_object, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ept_object.value = decoder.ReadUuid();
			status.value = decoder.ReadInt32();
		}
		public virtual async Task ept_mgmt_delete(uint object_speced, RpcPointer<System.Guid> @object, RpcPointer<dceidl.twr_t> tower, RpcPointer<int> status, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(object_speced);
			encoder.WriteValue(@object.value);
			encoder.WriteConformantStruct(tower.value, Titanis.DceRpc.NdrAlignment._4Byte);
			encoder.WriteStructDeferral(tower.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			status.value = decoder.ReadInt32();
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
	public class eptStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("e1af8308-5d1f-11c9-91a4-08002b14a0fa");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(3, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private ept _obj;
		public eptStub(ept obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_ept_insert,
					this.Invoke_ept_delete,
					this.Invoke_ept_lookup,
					this.Invoke_ept_map,
					this.Invoke_ept_lookup_handle_free,
					this.Invoke_ept_inq_object,
					this.Invoke_ept_mgmt_delete};
		}
		private async Task Invoke_ept_insert(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			uint num_ents;
			ept_entry_t[] entries;
			uint replace;
			RpcPointer<int> status = new RpcPointer<int>();
			num_ents = decoder.ReadUInt32();
			entries = decoder.ReadArrayHeader<ept_entry_t>();
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				elem_0 = decoder.ReadFixedStruct<ept_entry_t>(Titanis.DceRpc.NdrAlignment.NativePtr);
				entries[i] = elem_0;
			}
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				decoder.ReadStructDeferral<ept_entry_t>(ref elem_0);
				entries[i] = elem_0;
			}
			replace = decoder.ReadUInt32();
			var invokeTask = this._obj.ept_insert(num_ents, entries, replace, status, cancellationToken);
			await invokeTask;
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_delete(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			uint num_ents;
			ept_entry_t[] entries;
			RpcPointer<int> status = new RpcPointer<int>();
			num_ents = decoder.ReadUInt32();
			entries = decoder.ReadArrayHeader<ept_entry_t>();
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				elem_0 = decoder.ReadFixedStruct<ept_entry_t>(Titanis.DceRpc.NdrAlignment.NativePtr);
				entries[i] = elem_0;
			}
			for (int i = 0; (i < entries.Length); i++
			)
			{
				ept_entry_t elem_0 = entries[i];
				decoder.ReadStructDeferral<ept_entry_t>(ref elem_0);
				entries[i] = elem_0;
			}
			var invokeTask = this._obj.ept_delete(num_ents, entries, status, cancellationToken);
			await invokeTask;
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_lookup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			uint inquiry_type;
			RpcPointer<System.Guid> @object;
			RpcPointer<Titanis.DceRpc.RpcInterfaceId> Ifid;
			uint vers_option;
			RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle;
			uint max_ents;
			RpcPointer<uint> num_ents = new RpcPointer<uint>();
			RpcPointer<ArraySegment<ept_entry_t>> entries = new RpcPointer<ArraySegment<ept_entry_t>>();
			RpcPointer<int> status = new RpcPointer<int>();
			inquiry_type = decoder.ReadUInt32();
			@object = decoder.ReadPointer<System.Guid>();
			if ((null != @object))
			{
				@object.value = decoder.ReadUuid();
			}
			Ifid = decoder.ReadPointer<Titanis.DceRpc.RpcInterfaceId>();
			if ((null != Ifid))
			{
				Ifid.value = decoder.ReadRpcInterfaceId();
			}
			vers_option = decoder.ReadUInt32();
			entry_handle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
			entry_handle.value = decoder.ReadContextHandle();
			max_ents = decoder.ReadUInt32();
			var invokeTask = this._obj.ept_lookup(inquiry_type, @object, Ifid, vers_option, entry_handle, max_ents, num_ents, entries, status, cancellationToken);
			await invokeTask;
			encoder.WriteContextHandle(entry_handle.value);
			encoder.WriteValue(num_ents.value);
			encoder.WriteArrayHeader(entries.value, true);
			for (int i = 0; (i < entries.value.Count); i++
			)
			{
				ept_entry_t elem_0 = entries.value.Item(i);
				encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
			}
			for (int i = 0; (i < entries.value.Count); i++
			)
			{
				ept_entry_t elem_0 = entries.value.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_map(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<System.Guid> @object;
			RpcPointer<dceidl.twr_t> map_tower;
			RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle;
			uint max_towers;
			RpcPointer<uint> num_towers = new RpcPointer<uint>();
			RpcPointer<ArraySegment<RpcPointer<dceidl.twr_t>>> towers = new RpcPointer<ArraySegment<RpcPointer<dceidl.twr_t>>>();
			RpcPointer<int> status = new RpcPointer<int>();
			@object = decoder.ReadPointer<System.Guid>();
			if ((null != @object))
			{
				@object.value = decoder.ReadUuid();
			}
			map_tower = decoder.ReadPointer<dceidl.twr_t>();
			if ((null != map_tower))
			{
				map_tower.value = decoder.ReadConformantStruct<dceidl.twr_t>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<dceidl.twr_t>(ref map_tower.value);
			}
			entry_handle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
			entry_handle.value = decoder.ReadContextHandle();
			max_towers = decoder.ReadUInt32();
			var invokeTask = this._obj.ept_map(@object, map_tower, entry_handle, max_towers, num_towers, towers, status, cancellationToken);
			await invokeTask;
			encoder.WriteContextHandle(entry_handle.value);
			encoder.WriteValue(num_towers.value);
			encoder.WriteArrayHeader(towers.value, true);
			for (int i = 0; (i < towers.value.Count); i++
			)
			{
				RpcPointer<dceidl.twr_t> elem_0 = towers.value.Item(i);
				encoder.WritePointer(elem_0);
			}
			for (int i = 0; (i < towers.value.Count); i++
			)
			{
				RpcPointer<dceidl.twr_t> elem_0 = towers.value.Item(i);
				if ((null != elem_0))
				{
					encoder.WriteConformantStruct(elem_0.value, Titanis.DceRpc.NdrAlignment._4Byte);
					encoder.WriteStructDeferral(elem_0.value);
				}
			}
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_lookup_handle_free(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<Titanis.DceRpc.RpcContextHandle> entry_handle;
			RpcPointer<int> status = new RpcPointer<int>();
			entry_handle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
			entry_handle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.ept_lookup_handle_free(entry_handle, status, cancellationToken);
			await invokeTask;
			encoder.WriteContextHandle(entry_handle.value);
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_inq_object(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<System.Guid> ept_object = new RpcPointer<System.Guid>();
			RpcPointer<int> status = new RpcPointer<int>();
			var invokeTask = this._obj.ept_inq_object(ept_object, status, cancellationToken);
			await invokeTask;
			encoder.WriteValue(ept_object.value);
			encoder.WriteValue(status.value);
		}
		private async Task Invoke_ept_mgmt_delete(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			uint object_speced;
			RpcPointer<System.Guid> @object;
			RpcPointer<dceidl.twr_t> tower;
			RpcPointer<int> status = new RpcPointer<int>();
			object_speced = decoder.ReadUInt32();
			@object = new RpcPointer<System.Guid>();
			@object.value = decoder.ReadUuid();
			tower = new RpcPointer<dceidl.twr_t>();
			tower.value = decoder.ReadConformantStruct<dceidl.twr_t>(Titanis.DceRpc.NdrAlignment._4Byte);
			decoder.ReadStructDeferral<dceidl.twr_t>(ref tower.value);
			var invokeTask = this._obj.ept_mgmt_delete(object_speced, @object, tower, status, cancellationToken);
			await invokeTask;
			encoder.WriteValue(status.value);
		}
	}
}
