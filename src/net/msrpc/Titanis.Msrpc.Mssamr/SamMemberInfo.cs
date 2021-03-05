namespace Titanis.Msrpc.Mssamr
{
	public class SamMemberInfo
	{
		public uint ObjectId { get; }
		public uint Attributes { get; }

		public SamMemberInfo(uint relativeId, uint attributes)
		{
			this.ObjectId = relativeId;
			this.Attributes = attributes;
		}
	}
}