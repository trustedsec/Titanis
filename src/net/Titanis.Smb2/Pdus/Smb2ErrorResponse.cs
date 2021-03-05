using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.2 - SMB2 ERROR Response
	sealed class Smb2ErrorResponse : Smb2Pdu<Smb2ErrorResponseBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => 0;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 9;

		internal Smb2ErrorContext[] contexts;

		private Smb2LinkErrorContext? _symlink;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2ErrorResponseBody body = ref reader.ReadErrorRespBody();
			this.body = body;

			{
				if (body.errorContextCount > 0)
				{
					var contexts = new Smb2ErrorContext[body.errorContextCount];
					for (int i = 0; i < body.errorContextCount; i++)
					{
						reader.Align(8, offPdu);
						var ctxhdr = reader.ReadErrorCtxHdr();
						int offContextData = reader.Position;

						Smb2ErrorContext ctx;
						switch (ctxhdr.errorId)
						{
							case Smb2ErrorContextType.Default:
							default:
								// TODO: Process unknown contexts with a generic structure
								ctx = new Smb2DefaultErrorContext();
								break;
								// TODO: Handle share redirect
								//case Smb2ErrorContextType.ShareRedirect:
								//	ctx = new Smb2LinkErrorContext();
								//break;
						}

						ctx.hdr = ctxhdr;
						ctx.ReadFrom(reader, pduhdr.status);
						reader.Position = (int)(offContextData + ctxhdr.length);

						contexts[i] = ctx;
					}
					this.contexts = contexts;
				}
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2ErrorResponseBody body)
		{
			writer.WriteErrorRespHdr(body);
			throw new NotImplementedException();
		}

		internal Exception CreateException(Ntstatus status)
		{
			Smb2ErrorInfo errorInfo = new Smb2ErrorInfo
			{
				status = status
			};
			if (this._symlink != null)
				this._symlink.ApplyTo(ref errorInfo);

			if (this.contexts != null)
			{
				foreach (var ctx in this.contexts)
				{
					ctx.ApplyTo(ref errorInfo);
				}
			}
			var ex = errorInfo.CreateException();
			return ex;
		}
	}

	// [MS-SMB2] § 2.2.2 - SMB2 ERROR Response
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ErrorResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2ErrorResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal byte errorContextCount;
		internal byte reserved;
		internal uint byteCount;
	}

	// [MS-SMB2] § 2.2.2.1 - SMB2 ERROR Context Response
	public enum Smb2ErrorContextType : uint
	{
		Default = 0,
		ShareRedirect = 0x72645253,
		Symlink = 0x4c4d5953
	}

	// [MS-SMB2] § 2.2.2.1 - SMB2 ERROR Context Response
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ErrorContextHeader
	{
		public unsafe static int StructSize => sizeof(Smb2ErrorContextHeader);

		internal uint length;
		internal Smb2ErrorContextType errorId;
	}

	// [MS-SMB2] § 2.2.2.2 - ErrorData format
	struct Smb2ErrorInfo
	{
		internal Ntstatus status;
		internal int requiredBufferSize;

		internal SymbolicLinkInfo? symlink;

		internal Exception CreateException()
		{
			if (this.status == Ntstatus.STATUS_BUFFER_TOO_SMALL)
			{
				return new Smb2BufferTooSmallException(this.requiredBufferSize);
			}
			else if (this.status == Ntstatus.STATUS_STOPPED_ON_SYMLINK)
			{
				return new Smb2SymlinkException(this);
			}
			else if (this.status == Ntstatus.STATUS_END_OF_FILE)
			{
				return new EndOfStreamException();
			}
			else if (this.status == Ntstatus.STATUS_CANCELLED)
			{
				return new OperationCanceledException();
			}
			else
				return new NtstatusException(this.status);
		}
	}

	// [MS-SMB2] § 2.2.2.1 - SMB2 ERROR Context Response
	abstract class Smb2ErrorContext
	{
		internal Smb2ErrorContextHeader hdr;

		internal abstract Smb2ErrorContextType ContextType { get; }

		internal abstract void ReadFrom(ByteMemoryReader reader, Ntstatus status);
		internal abstract void ApplyTo(ref Smb2ErrorInfo errorInfo);
	}

	class Smb2DefaultErrorContext : Smb2ErrorContext
	{
		internal override Smb2ErrorContextType ContextType => Smb2ErrorContextType.Default;
		internal byte[] errorData;

		// [MS-SMB2] § 2.2.2.2 - ErrorData format
		internal override void ApplyTo(ref Smb2ErrorInfo errorInfo)
		{
			if (errorInfo.status == Ntstatus.STATUS_BUFFER_TOO_SMALL
				&& this.errorData != null
				&& this.errorData.Length >= 4)
			{
				errorInfo.requiredBufferSize = BitConverter.ToInt32(this.errorData, 0);
			}
			else if (this._symlink != null)
			{
				this._symlink.ApplyTo(ref errorInfo);
			}
			else
			{
				// TODO: How to handle generic errorData?
			}
		}

		private Smb2LinkErrorContext? _symlink;
		internal override void ReadFrom(ByteMemoryReader reader, Ntstatus status)
		{
			// [MS-SMB2] § 2.2.2.2.1
			if (status == Ntstatus.STATUS_STOPPED_ON_SYMLINK)
			{
				var symlink = TryReadLinkError(status, this.hdr.length, reader);
				this._symlink = symlink;
			}
			else
			{
				this.errorData = reader.ReadBytes((int)this.hdr.length);
			}
		}


		// [MS-SMB2] § 2.2.2.2 - ErrorData format
		// [MS-SMB2] § 2.2.2.2.1 - Symbolic Link Error Response
		private static unsafe Smb2LinkErrorContext TryReadLinkError(Ntstatus status, uint length, ByteMemoryReader reader)
		{
			if (status == Ntstatus.STATUS_STOPPED_ON_SYMLINK)
			{
				if (length >= Smb2LinkErrorHeader.StructSize)
				{
					fixed (byte* pData = reader.Remaining.Span)
					{
						Smb2LinkErrorHeader linkData = new Smb2LinkErrorHeader
						{
							symlinkLength = reader.ReadInt32LE(),
							symlinkErrorTag = reader.ReadUInt32LE()
						};
						if (
							(linkData.symlinkErrorTag == Smb2LinkErrorContext.SymlinkErrorTag)
							)
						{
							var linkInfo = (SymbolicLinkInfo)ReparseInfo.Parse(reader.Remaining.Span.Slice(0, linkData.symlinkLength - 4));
							return new Smb2LinkErrorContext(linkInfo);
						}
					}
				}
			}
			return null;
		}
	}

	// [MS-SMB2] § 2.2.2.2.1 - Symbolic Link Error Response
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2LinkErrorHeader
	{
		public unsafe static int StructSize => sizeof(Smb2LinkErrorHeader);

		internal int symlinkLength;
		internal uint symlinkErrorTag;
		// Reparse info follows
	}

	// [MS-SMB2] § 2.2.2.2.1 - Symbolic Link Error Response
	class Smb2LinkErrorContext
	{
		internal Smb2LinkErrorContext() { }

		public Smb2LinkErrorContext(SymbolicLinkInfo linkInfo)
		{
			this.LinkInfo = linkInfo;
		}

		public SymbolicLinkInfo LinkInfo { get; }

		internal const uint SymlinkErrorTag = 0x4C4D5953;
		internal const uint SymlinkReparseTag = 0xA000000C;

		internal Smb2LinkErrorHeader linkErrorHeader;
		internal string path;
		internal string printName;

		internal void ApplyTo(ref Smb2ErrorInfo errorInfo)
		{
			errorInfo.symlink = this.LinkInfo;
		}
	}
}
