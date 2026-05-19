using System.Collections.Generic;
using System.Diagnostics;
using Titanis.IO;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.17 - DerivationList
	partial struct DerivationList : IPduStruct
	{
		public DerivationList(string[] classNames)
		{
			this.classNames = classNames;
		}

		public string[] classNames;

		public void ReadFrom<TSource>(TSource reader) where TSource : class, IByteSource
		{
			List<string> derivationList = new List<string>();
			var offDerList = reader.Position;
			var cbDerList = reader.ReadEncodingLength();
			Debug.Assert(cbDerList >= 4);
			var offDerListEnd = offDerList + cbDerList;
			// TODO: Perform a stricter length check
			while (reader.Position < offDerListEnd)
			{
				var className = reader.ReadPduStruct<ClassNameEncoding>().name.value;
				derivationList.Add(className);
			}
			this.classNames = derivationList.ToArray();

			Debug.Assert(reader.Position == offDerListEnd);
		}

		public void WriteTo(ByteWriter writer)
		{
			var offDerList = writer.ReserveEncodingLength();

			if (this.classNames != null)
			{
				foreach (var name in this.classNames)
				{
					writer.WriteClassName(name);
				}
			}

			writer.WriteEncodingLengthAt(offDerList);
		}
	}
}
