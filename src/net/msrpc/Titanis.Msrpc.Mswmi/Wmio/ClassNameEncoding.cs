using System.Diagnostics;
using Titanis.IO;
using Titanis.PduStruct;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.18 - ClassNameEncoding
	[PduStruct]
	partial struct ClassNameEncoding
	{
		public ClassNameEncoding(string name)
		{
			this.name = new EncodedString(name);
		}

		[PduPosition]
		private long position;

		internal EncodedString name;

		partial void OnAfterReadPdu(IByteSource source)
		{
			var offEnd = source.Position;
			var length = source.ReadUInt32LE();
			Debug.Assert(length == (offEnd - this.position));
		}
		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var offEnd = writer.Position;
			writer.WriteUInt32LE((uint)(offEnd - this.position));
		}
	}
}
