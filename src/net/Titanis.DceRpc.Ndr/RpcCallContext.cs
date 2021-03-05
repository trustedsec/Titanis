using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Titanis.DceRpc.Client;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Tracks pointers and referents during a call.
	/// </summary>
	public class RpcCallContext
	{
		internal readonly IObjrefMarshal? dcom;
		public RpcCallContext(IObjrefMarshal? dcom)
		{
			this.dcom = dcom;
		}

		private int _lastReferentId = 0;

		private Dictionary<long, RpcPointer> _pointersByRefId = new Dictionary<long, RpcPointer>();
		private Dictionary<RpcPointer, long> _pointers = new Dictionary<RpcPointer, long>();
		private Dictionary<long, object> _objrefsByRefid = new Dictionary<long, object>();

		internal int GetUniqueReferentId()
		{
			return this.AllocReferentId();
		}
		internal long GetReferentIdFor(RpcPointer? ptr)
		{
			long refId;

			if (ptr == null)
				return 0;
			else if (!this._pointers.TryGetValue(ptr, out refId))
			{
				refId = AllocReferentId();
				ptr.referentId = refId;
				this._pointers.Add(ptr, refId);
				this._pointersByRefId.Add(refId, ptr);
			}
			return refId;
		}

		private int AllocReferentId()
		{
			return Interlocked.Increment(ref this._lastReferentId);
		}

		internal RpcPointer Resolve(long refId)
		{
			return this._pointersByRefId.TryGetValue(refId);
		}
		public void AddPointer(long refId, RpcPointer ptr)
		{
			this._pointers.Add(ptr, refId);
			this._pointersByRefId.Add(refId, ptr);
		}


		internal TypedObjref<T>? ResolveObjref<T>(int refId)
			where T : class, IRpcObject
		{
			return (TypedObjref<T>)this._objrefsByRefid.TryGetValue(refId);
		}

		internal void AddObjref(long refId, object obj)
		{
			this._objrefsByRefid.Add(refId, obj);
		}
	}
}
