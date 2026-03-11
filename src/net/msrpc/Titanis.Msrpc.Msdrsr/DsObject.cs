namespace Titanis.Msrpc.Msdrsr
{
	public class DsObject
	{
		internal DsObject(DsName name, DsAttribute[] attributes)
		{
			Name = name;
			Attributes = attributes;
		}

		public DsName Name { get; }
		public DsAttribute[] Attributes { get; }
	}

	public class DsAttribute
	{
		internal DsAttribute(string oid, DsAttributeValue[] values)
		{
			Oid = oid;
			Values = values;
		}

		public string Oid { get; }
		public DsAttributeValue[] Values { get; }
	}

	public class DsAttributeValue
	{
		internal DsAttributeValue(byte[] bytes)
		{
			Bytes = bytes;
		}

		public byte[] Bytes { get; }
	}
}
