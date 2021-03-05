using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.59 - QualifierSet
	internal partial struct QualifierSet : IPduStruct
	{
		public QualifierSet(Qualifier[] qualifiers)
		{
			this.qualifiers = qualifiers;
		}

		internal Qualifier[] qualifiers;

		public void ReadFrom(IByteSource reader)
		{
			var offStart = reader.Position;
			var cbSet = reader.ReadEncodingLength();
			Debug.Assert(cbSet >= 4);

			var offEnd = offStart + cbSet;

			List<Qualifier> qualifiers = new List<Qualifier>();
			while (reader.Position < offEnd)
			{
				var qual = reader.ReadPduStruct<Qualifier>();
				qualifiers.Add(qual);
			}

			Debug.Assert(reader.Position == offEnd);

			this.qualifiers = qualifiers.ToArray();
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public void WriteTo(ByteWriter writer)
		{
			if (this.qualifiers.IsNullOrEmpty())
			{
				writer.WriteInt32LE(4);
			}
			else
			{
				int offStart = writer.ReserveEncodingLength();
				foreach (var qual in this.qualifiers)
				{
					writer.WritePduStruct(qual);

				}
				writer.WriteEncodingLengthAt(offStart);
			}
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> this.WriteTo(writer);

		public static QualifierSet Encode(WmiQualifier[]? qualifiers, ByteWriter heapWriter)
		{
			return new QualifierSet(
				(qualifiers == null) ? Array.Empty<Qualifier>()
				: qualifiers.ConvertAll(r => r.Encode(heapWriter))
				);
		}
	}
}
