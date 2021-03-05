using Titanis.IO;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.21 - PropertyLookupTable
	[PduStruct]
	partial struct PropertyLookupTable
	{
		public PropertyLookupTable(PropertyLookup[] properties)
		{
			this.properties = properties;
		}

		private int count;
		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			if (this.properties.IsNullOrEmpty())
				this.count = 0;
			else
				this.count = this.properties.Length;
		}

		[PduArraySize(nameof(count))]
		internal PropertyLookup[] properties;
	}
}
