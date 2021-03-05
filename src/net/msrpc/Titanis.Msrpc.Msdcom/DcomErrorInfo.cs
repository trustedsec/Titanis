using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Msdcom;
using Titanis.PduStruct;

namespace Titanis.DceRpc.Client
{
	// [MS-DCOM] § 2.2.21.3
	[PduStruct]
	partial struct DcomErrorInfoString
	{
		internal int max;
		internal int offset;
		internal int actual;

		private int ByteCount => (this.max - 1) * 2;
		[PduString(System.Runtime.InteropServices.CharSet.Unicode, nameof(ByteCount))]
		internal string str;

		private ushort nullTerm;
	}

	// [MS-DCOM] § 2.2.21.2
	[PduStruct]
	partial struct DcomErrorInfoStruct
	{
		internal int version;
		internal uint helpContext;
		internal Guid iid;

		internal int sourceSignature;
		internal bool HasSource => (this.sourceSignature == -1);
		[PduConditional(nameof(HasSource))]
		internal DcomErrorInfoString source;

		internal int descriptionSignature;
		internal bool HasDescription => (this.descriptionSignature == -1);
		[PduConditional(nameof(HasDescription))]
		internal DcomErrorInfoString description;

		internal int helpFileSignature;
		internal bool HasHelpFile => (this.helpFileSignature == -1);
		[PduConditional(nameof(HasHelpFile))]
		internal DcomErrorInfoString helpFile;

	}

	/// <summary>
	/// Describes a DCOM error.
	/// </summary>
	public sealed class DcomErrorInfo : Objref_Custom, IRpcObject
	{
		private readonly DcomErrorInfoStruct errorInfo;

		internal DcomErrorInfo(DcomErrorInfoStruct errorInfo)
		{
			this.errorInfo = errorInfo;
		}

		/// <summary>
		/// Gets a textual description of the error.
		/// </summary>
		public string? Description => this.errorInfo.description.str;

		/// <summary>
		/// Gets the help context ID.
		/// </summary>
		public uint HelpContext => this.errorInfo.helpContext;

		/// <summary>
		/// Gets the help file that describes the error.
		/// </summary>
		public string? HelpFile => this.errorInfo.helpFile.str;

		/// <summary>
		/// Gets the programmatic source of the error.
		/// </summary>
		public string? Source => this.errorInfo.source.str;

		/// <inheritdoc/>
		public sealed override Guid MarshalClsid => DcomIds.CLSID_ErrorObject;

		/// <inheritdoc/>
		public sealed override Guid Iid => this.errorInfo.iid;

		/// <inheritdoc/>
		public sealed override object GetObject()
		{
			return this;
		}

		/// <inheritdoc/>
		protected sealed override void WriteObjectData(ByteWriter writer)
		{
			writer.WritePduStruct(this.errorInfo);
		}
	}

	internal class DcomErrorUnmarshaler : IUnmarshaler
	{
		public Objref Unmarshal(ByteMemoryReader reader)
		{
			var errorInfo = reader.ReadPduStruct<DcomErrorInfoStruct>();
			return new DcomErrorInfo(errorInfo);
		}
	}
}
