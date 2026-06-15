namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct DRS_EXTENSIONS_INT2
	{
		internal DRS_EXTENSIONS_INT ext1;

		public int ExtCaps { get; set; }
	}
	// [MS-DRSR] § 5.39 DRS_EXTENSIONS_INT
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct DRS_EXTENSIONS_INT
	{
		public DrsBindFlags BindFlags { get; set; }
		public Guid SiteObjGuid { get; set; }
		public int Pid { get; set; }
		public int ReplEpoch { get; set; }
		public DrsBindMoreFlags MoreFlags { get; set; }
		public Guid ConfigObjGuid { get; set; }
	}
}
