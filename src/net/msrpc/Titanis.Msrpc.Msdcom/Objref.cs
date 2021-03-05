using dceidl;
using ms_dcom;
using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.IO;

namespace Titanis.Msrpc.Msdcom
{

	public enum ObjrefType
	{
		Standard = 1,
		Handler = 2,
		Custom = 4,
		Extended = 8,
	}

	// [MS-DCOM] § 2.2.18
	[PduStruct]
	partial struct OBJREF
	{
		internal const uint ValidSignature = 0x574f454dU;

		internal uint signature;
		internal ObjrefType flags;
		internal Guid iid;
	}

	// [MS-DCOM] § 2.2.18.6 - OBJREF_CUSTOM
	[PduStruct]
	partial struct OBJREF_CUSTOM
	{
		internal Guid clsid;
		internal int cbExtension;
		internal int reserved;
	}

	/// <summary>
	/// References a remote DCOM object.
	/// </summary>
	// [MS-DCOM] § 2.2.18
	public abstract class Objref
	{
		/// <summary>
		/// Encodes the OBJREF to a byte array.
		/// </summary>
		/// <returns>A byte array representing the OBJREF</returns>
		public byte[] ToByteArray()
		{
			if (!(this.TypeFlag is ObjrefType.Standard or ObjrefType.Handler or ObjrefType.Extended or ObjrefType.Custom))
				throw new InvalidOperationException($"The OBJREF does not have a valid TypeFlag");

			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(new OBJREF
			{
				signature = OBJREF.ValidSignature,
				flags = this.TypeFlag,
				iid = this.Iid
			});

			this.WriteTo(writer);

			var marshalData = writer.GetData().ToArray();
			return marshalData;
		}

		/// <summary>
		/// Writes the OBJREF to a <see cref="ByteWriter"/>.
		/// </summary>
		/// <param name="writer">Writer to write to</param>
		protected abstract void WriteTo(ByteWriter writer);
		/// <summary>
		/// Gets the <see cref="ObjrefType"/> specifying the type of OBJREF.
		/// </summary>
		protected abstract ObjrefType TypeFlag { get; }
		/// <summary>
		/// Gets the IID of the marshaled interface.
		/// </summary>
		public abstract Guid Iid { get; }
		/// <summary>
		/// Gets the public reference count.
		/// </summary>
		public abstract int PublicRefCount { get; set; }
		/// <summary>
		/// Gets the object exporter ID.
		/// </summary>
		public abstract ulong Oxid { get; set; }
		/// <summary>
		/// Gets the object ID.
		/// </summary>
		public abstract ulong Oid { get; set; }
		/// <summary>
		/// Gets the interface pointer ID.
		/// </summary>
		public abstract Guid Ipid { get; set; }

		internal static Objref Decode(ms_dcom.MInterfacePointer mptr)
		{
			byte[] marshalData = mptr.abData;
			if (marshalData is null)
				throw new ArgumentException("The MinterfacePointer does not contain any data.", nameof(mptr));
			return Decode(marshalData);
		}

		public static Objref Decode(byte[] marshalData)
		{
			ArgumentNullException.ThrowIfNull(marshalData);
			if (marshalData.Length < OBJREF.PduStructSize)
				throw new ArgumentException("marshalData is too small to contain a valid OBJREF", nameof(marshalData));

			ByteMemoryReader reader = new ByteMemoryReader(marshalData);
			var struc = reader.ReadPduStruct<OBJREF>();
			if (struc.signature != OBJREF.ValidSignature)
				throw new InvalidDataException("Marshal data is not valid.");

			if (struc.flags == ObjrefType.Custom)
			{
				var custom = reader.ReadPduStruct<OBJREF_CUSTOM>();
				var unmarshal = GetUnmarshalFor(custom.clsid);
				var objref = unmarshal.Unmarshal(reader);
				return objref;
			}
			else
			{
				StdobjrefBase objref = struc.flags switch
				{
					ObjrefType.Standard => new Objref_Standard(),
					ObjrefType.Handler => new Objref_Handler(),
					ObjrefType.Extended => new Objref_Extended(),
					ObjrefType.Custom => throw new NotImplementedException(),
					_ => throw new InvalidDataException("The marshal data indicated an unsupported OBJREF type.")
				};
				objref.SetIid(struc.iid);
				objref.ReadFrom(reader);

				return objref;
			}
		}

		private static readonly ConcurrentDictionary<Guid, IUnmarshaler> _unmarshals = new ConcurrentDictionary<Guid, IUnmarshaler>();
		private static IUnmarshaler GetUnmarshalFor(Guid clsid)
		{
			if (!_unmarshals.TryGetValue(clsid, out var unmarshal))
				throw new ArgumentException($"No unmarshal handler registered for CLSID '{clsid}'.", nameof(clsid));
			return unmarshal;
		}
		public static void RegisterUnmarshaler(Guid clsid, IUnmarshaler unmarshal)
		{
			if (unmarshal is null) throw new ArgumentNullException(nameof(unmarshal));

			if (!_unmarshals.TryAdd(clsid, unmarshal))
				throw new ArgumentException($"An unmarshal object is already registered for CLSID '{clsid}'.", nameof(unmarshal));
		}

		protected abstract void ReadFrom(ByteMemoryReader reader);
	}

	// [MS-DCOM] § 2.2.18.1 STDOBJREF
	[Flags]
	public enum StdobjrefFlags
	{
		None = 0,
		NoPing = 0x00001000
	}

	// [MS-DCOM] § 2.2.19.4 - SECURITYBINDING
	public sealed class SecurityBinding
	{
		public SecurityBinding(RpcAuthType authenticationService, string? principalName)
		{
			AuthenticationService = authenticationService;
			PrincipalName = principalName;
		}

		public sealed override string ToString() => this.PrincipalName;

		public RpcAuthType AuthenticationService { get; }
		public string? PrincipalName { get; }
	}

	// [MS-DCOM] § 2.2.19 DUALSTRINGARRAY
	public sealed class DualStringArray
	{
		public StringBinding[]? StringBindings { get; set; }
		public SecurityBinding[]? SecurityBindings { get; set; }

		public static DualStringArray FromIdl(DUALSTRINGARRAY struc)
		{
			// TODO: Starting size?
			StringBuilder sb = new StringBuilder();

			// Read string bindings
			List<StringBinding> bindings = new List<StringBinding>();
			int i;
			for (i = 0; 0 != (struc.aStringArray[i]); i++)
			{
				char c;
				while ((c = (char)struc.aStringArray[++i]) != 0)
				{
					sb.Append(c);
				}

				StringBinding binding = new StringBinding(struc.aStringArray[i], sb.ToString());
				sb.Clear();
				bindings.Add(binding);
			}

			// Read security bindings
			List<SecurityBinding> secBindings = new List<SecurityBinding>();
			for (i++; 0 != (struc.aStringArray[i]); i++)
			{
				RpcAuthType authenticationService = (RpcAuthType)struc.aStringArray[i];

				// Skip reserved
				++i;

				char c;
				while ((c = (char)struc.aStringArray[++i]) != 0)
				{
					sb.Append(c);
				}

				string principal = sb.ToString();
				sb.Clear();
				secBindings.Add(new SecurityBinding(authenticationService, principal));
			}

			return new DualStringArray
			{
				StringBindings = bindings.ToArray(),
				SecurityBindings = secBindings.ToArray()
			};
		}
	}

	/// <summary>
	/// Base class for OBJREF classes based on STDOBJREF.
	/// </summary>
	abstract class StdobjrefBase : Objref
	{
		private ms_dcom.STDOBJREF _std;

		public ms_dcom.STDOBJREF Standard { get => _std; set => _std = value; }

		private Guid _iid;
		public sealed override Guid Iid => this._iid;
		public void SetIid(Guid iid) => this._iid = iid;

		public sealed override int PublicRefCount { get => (int)this._std.cPublicRefs; set => this._std.cPublicRefs = (uint)value; }
		public sealed override ulong Oxid { get => this._std.oxid; set => this._std.oxid = value; }
		public sealed override ulong Oid { get => this._std.oid; set => this._std.oid = value; }
		public sealed override Guid Ipid { get => this._std.ipid; set => this._std.ipid = value; }
	}

	// [MS-DCOM] § 2.2.18.4 - OBJREF_STD
	sealed class Objref_Standard : StdobjrefBase
	{
		internal Objref_Standard() { }
		internal Objref_Standard(Guid iid, STDOBJREF std, DualStringArray bindings)
		{
			this.SetIid(iid);
			this.Standard = std;
			this.Bindings = bindings;
		}

		protected override ObjrefType TypeFlag => ObjrefType.Standard;

		public DualStringArray? Bindings { get; set; }

		protected sealed override void WriteTo(ByteWriter writer)
		{
			if (this.Bindings == null)
				throw new InvalidOperationException("The object does not have bindings set.");

			writer.Write(this.Standard);
			writer.Write(this.Bindings);
		}

		protected sealed override void ReadFrom(ByteMemoryReader reader)
		{
			this.Standard = reader.ReadPduStruct<STDOBJREF_pdu>().std;
			this.Bindings = reader.ReadDualStringArray();
		}
	}

	/// <summary>
	/// Wraps a <see cref="STDOBJREF"/>
	/// </summary>
	/// <remarks>
	/// The goal is to implement STDOBJREF as a [PduStruct] directly.
	/// However, it is generated by Animus, which doesn't support [PduStruct].
	/// </remarks>
	[PduStruct]
	partial struct STDOBJREF_pdu
	{
		public STDOBJREF_pdu(STDOBJREF std)
		{
			this.std = std;
		}

		[PduField(ReadMethod = nameof(ReadStd), WriteMethod = nameof(WriteStd))]
		internal STDOBJREF std;

		private STDOBJREF ReadStd(IByteSource source, PduByteOrder byteOrder)
			=> source.ReadStdObjref();
		private void WriteStd(ByteWriter writer, STDOBJREF value, PduByteOrder byteOrder)
			=> throw new NotImplementedException();
	}

	// [MS-DCOM] § 2.2.18.5 - OBJREF_HANDLER
	[PduStruct]
	partial struct OBJREF_HANDLER
	{
		internal STDOBJREF_pdu std;
		internal Guid clsid;
	}

	// [MS-DCOM] § 2.2.18.5 - OBJREF_HANDLER
	sealed class Objref_Handler : StdobjrefBase
	{
		protected override ObjrefType TypeFlag => ObjrefType.Standard;

		public Guid Clsid { get; set; }
		public DualStringArray? Bindings { get; set; }

		protected sealed override void WriteTo(ByteWriter writer)
		{
			if (this.Bindings == null)
				throw new InvalidOperationException("The object does not have bindings set.");

			writer.WritePduStruct(new OBJREF_HANDLER
			{
				std = new STDOBJREF_pdu(this.Standard),
				clsid = this.Clsid
			});
			writer.Write(this.Bindings);
		}

		protected sealed override void ReadFrom(ByteMemoryReader reader)
		{
			var struc = reader.ReadPduStruct<OBJREF_HANDLER>();
			this.Standard = struc.std.std;
			this.Clsid = struc.clsid;
			this.Bindings = reader.ReadDualStringArray();
		}
	}

	// [MS-DCOM] § 2.2.18.6 - OBJREF_CUSTOM
	public abstract class Objref_Custom : Objref
	{
		protected override ObjrefType TypeFlag => ObjrefType.Custom;

		public abstract Guid MarshalClsid { get; }
		//public byte[]? ObjectData { get; set; }
		// TODO: OBJREF_CUSTOM
		public sealed override Guid Ipid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override int PublicRefCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override ulong Oxid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public override ulong Oid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public abstract object GetObject();

		protected abstract void WriteObjectData(ByteWriter writer);
		protected sealed override void WriteTo(ByteWriter writer)
		{
			writer.WritePduStruct(new OBJREF_CUSTOM
			{
				clsid = this.MarshalClsid,
				cbExtension = 0,
				reserved = 752,
			});

			// HACK: mofcomp sends 0xDB for this byte
			// reserved

			this.WriteObjectData(writer);
		}

		protected sealed override void ReadFrom(ByteMemoryReader reader)
			=> throw new NotImplementedException();
	}

	abstract class EnvoyContext
	{

	}

	// [MS-DCOM] § 2.2.18.7 - OBJREF_EXTENDED
	sealed class Objref_Extended : StdobjrefBase
	{
		protected override ObjrefType TypeFlag => ObjrefType.Standard;

		public DataElement[]? Elements { get; set; }

		protected sealed override void WriteTo(ByteWriter writer)
		{
			throw new NotImplementedException();
		}
		protected sealed override void ReadFrom(ByteMemoryReader reader)
		{
			// TODO: Envoy context stuff
			throw new NotImplementedException();
		}
	}

	// [MS-DCOM] § 2.2.18.8 - DATAELEMENT
	sealed class DataElement
	{
		public Guid DataId { get; set; }
		public byte[] Data { get; set; }
	}

	[Flags]
	enum MarshaledContextFlags : int
	{
		None = 0,
		ByValue = 2,
	}


	// [MS-DCOM] § 2.2.20 - Context
	[PduStruct]
	partial struct MarshaledContext
	{
		public ushort MajorVersion;
		public ushort MinorVersion;
		public Guid ContextId;
		public MarshaledContextFlags Flags;
		private int Reserved;
		public int numExtents;
		public int cbExtents;
		public int MarshalFlags;
		public int count;
		public int IsFrozen;
	}



	static class DcomWriter
	{
		// [MS-DCOM] § 2.2.19.3 - STRINGBINDING
		internal static void Write(this ByteWriter writer, StringBinding binding)
		{
			if (binding is null) throw new ArgumentNullException(nameof(binding));

			writer.WriteUInt16LE(binding.TowerId);
			if (binding.NetworkAddress is not null)
				writer.WriteStringUni(binding.NetworkAddress);
			writer.WriteUInt16LE(0);    // Null terminator
		}
		// [MS-DCOM] § 2.2.19.4 - SECURITYBINDING
		internal static void Write(this ByteWriter writer, SecurityBinding binding)
		{
			if (binding is null) throw new ArgumentNullException(nameof(binding));

			writer.WriteUInt16LE((ushort)binding.AuthenticationService);
			writer.WriteUInt16LE(0xFFFF);
			if (binding.PrincipalName is not null)
				writer.WriteStringUni(binding.PrincipalName);
			writer.WriteUInt16LE(0);    // Null terminator
		}
		internal static void Write(this ByteWriter writer, DualStringArray ary)
		{
			if (ary is null) throw new ArgumentNullException(nameof(ary));
			if (ary.StringBindings == null || ary.StringBindings.Length == 0)
				throw new ArgumentException("The array does not contain any string bindings.", nameof(ary));
			if (ary.SecurityBindings == null || ary.SecurityBindings.Length == 0)
				throw new ArgumentException("The array does not contain any security bindings.", nameof(ary));
			if (ary.StringBindings.Contains(null))
				throw new ArgumentException("The array contains a null string binding.", nameof(ary));
			if (ary.SecurityBindings.Contains(null))
				throw new ArgumentException("The array contains a null security binding.", nameof(ary));

			var header = writer.Consume(4);

			var offBindings = writer.Position;
			foreach (var str in ary.StringBindings)
			{
				writer.Write(str);
			}
			// nullterm1
			writer.WriteUInt16LE(0);

			var offSecurityBindings = writer.Position;
			foreach (var str in ary.StringBindings)
			{
				writer.Write(str);
			}
			// nullterm2
			writer.WriteUInt16LE(0);

			var offEnd = writer.Position;

			// Header
			BinaryPrimitives.WriteUInt16LittleEndian(header, (ushort)((offEnd - offBindings) / 2));
			BinaryPrimitives.WriteUInt16LittleEndian(header.Slice(2, 2), (ushort)((offSecurityBindings - offBindings) / 2));

			writer.SetPosition(offEnd);
		}

		// [MS-DCOM] § 2.2.18.1 STDOBJREF
		internal static void Write(this ByteWriter writer, STDOBJREF objref)
		{
			writer.WriteUInt32LE(objref.flags);
			writer.WriteUInt32LE(objref.cPublicRefs);
			writer.WriteUInt64LE(objref.oxid);
			writer.WriteUInt64LE(objref.oid);
			writer.WriteGuid(objref.ipid);
		}
	}
}
