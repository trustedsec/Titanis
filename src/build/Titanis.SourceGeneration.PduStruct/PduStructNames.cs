using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.SourceGen
{
	internal static class PduStructNames
	{
		internal const string IPduStructName = "Titanis.IO.IPduStruct";
		internal const string PduStructSizeName = "PduStructSize";
		internal const string ByteSourceName = "Titanis.IO.IByteSource";
		internal const string ByteWriterName = "Titanis.IO.ByteWriter";
		internal const string ReadFromName = "ReadFrom";
		internal const string ReaderParamName = "reader";
		internal const string WriteToName = "WriteTo";
		internal const string WriterParamName = "writer";
		internal const string ReadPduStructName = "ReadPduStruct";
		internal const string WritePduStructName = "WritePduStruct";
		internal const string ByteOrderParamName = "byteOrder";
		internal const string LimitVarName = "_PduStruct_count";
		internal const string ElementVarName = "_PduStruct_elem";
		internal const string LoopVarName = "i";
		internal const string ArrayVarName = "_PduStruct_array";

		internal const string Align = "Align";
		internal const string ReadString = "ReadString";
		internal const string OnBeforeReadPdu = nameof(OnBeforeReadPdu);
		internal const string OnAfterReadPdu = nameof(OnAfterReadPdu);
		internal const string OnBeforeWritePdu = nameof(OnBeforeWritePdu);
		internal const string OnAfterWritePdu = nameof(OnAfterWritePdu);
		internal const string SourceParamName = "source";
		internal const string LE_Suffix = "_LE";
		internal const string BE_Suffix = "_BE";
		internal const string Titanis_IO = "Titanis.IO";
		internal const string PositionName = "Position";
		internal const string WriteBytesName = "WriteBytes";
		internal const string ReadBytesName = "ReadBytes";
	}
}
