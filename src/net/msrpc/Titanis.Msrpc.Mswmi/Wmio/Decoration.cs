namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.7 - Decoration
	[PduStruct]
	partial struct Decoration
	{
		internal EncodedString serverName;
		internal EncodedString ns;

		public WmiDecoration ToDecoration()
			=> new WmiDecoration(this.serverName.value, this.ns.value);
	}
}
