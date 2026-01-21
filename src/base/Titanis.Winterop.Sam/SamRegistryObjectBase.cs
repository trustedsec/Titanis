using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Titanis.Winterop.Sam;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <summary>
	/// Base class for SAM objects.
	/// </summary>
	public abstract class SamRegistryObjectBase
	{
		protected SamRegistryObjectBase(
			SamStore store,
			ImmutableArray<byte> fixedData,
			ImmutableArray<byte> variableData,
			int variableAttributeCount)
		{
			if (!variableData.IsDefaultOrEmpty)
				this._variableInfo = new SamRegistryObjInfo(variableData, variableAttributeCount);

			this.Store = store;
		}

		private readonly SamRegistryObjInfo _variableInfo;
		public SamStore Store { get; }

		public bool HasVariableAttributeData => !this._variableInfo.IsEmpty;

		protected ReadOnlySpan<byte> GetVariableAttribute(int attributeIndex)
		{
			if (this._variableInfo.IsEmpty)
				throw new InvalidOperationException("This object does not contain data for variable-length attributes.");

			if ((uint)attributeIndex >= this._variableInfo.TotalAttributeCount)
				throw new ArgumentOutOfRangeException(nameof(attributeIndex));

			return this._variableInfo.GetAttr(attributeIndex);
		}
	}

	internal readonly struct SamRegistryObjInfo
	{
		internal SamRegistryObjInfo(ImmutableArray<byte> bytes, int totalAttrCount)
		{
			Debug.Assert(totalAttrCount > 0);
			Debug.Assert(!bytes.IsDefaultOrEmpty);
			Debug.Assert(bytes.Length > AttrInfo.StructSize * totalAttrCount);

			this.Bytes = bytes;
			this.TotalAttributeCount = totalAttrCount;
		}

		public bool IsEmpty => this.Bytes.IsDefaultOrEmpty;
		public readonly ImmutableArray<byte> Bytes { get; }
		public readonly int TotalAttributeCount { get; }
		public readonly ReadOnlySpan<byte> GetAttr(int attributeIndex)
		{
			if ((uint)attributeIndex >= (uint)this.TotalAttributeCount)
				throw new ArgumentOutOfRangeException(nameof(attributeIndex));

			ref readonly AttrInfo attrInfo = ref MemoryMarshal.AsRef<AttrInfo>(this.Bytes.AsSpan(attributeIndex * AttrInfo.StructSize, 12));
			var attrBytes = this.Bytes.AsSpan(this.TotalAttributeCount * AttrInfo.StructSize + attrInfo.offset, attrInfo.length);
			return attrBytes;
		}
	}

	struct AttrInfo
	{
		internal const int StructSize = 12;

		public int offset;
		public int length;
		public int extra;
	}
}
