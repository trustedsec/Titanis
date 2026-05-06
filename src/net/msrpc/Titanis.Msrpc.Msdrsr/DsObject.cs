using Titanis.Ldap;
using Titanis.Winterop.SamServer;

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

		public LdapEntry ToLdapEntry()
		{
			var obj = this;

			byte[]? suppBytes = null;
			int? kvno = null;
			List<LdapAttribute> returnedAttrs = new List<LdapAttribute>(obj.Attributes.Length);
			for (int i = 0; i < obj.Attributes.Length; i++)
			{
				DsAttribute? attr = obj.Attributes[i];
				var attrType = LdapAttributeTypes.TryGetByNameOrOid(attr.Oid);

				var name = attrType?.Name;
				object[] values;
				if (attrType != null && attrType.Syntax != null)
				{
					values = Array.ConvertAll(attr.Values, r => attrType.Syntax.DecodeDsrep(r.Bytes));

					if (attrType.Oid == LdapAttributeTypes.SupplementalCredentials.Oid && attr.Values.Length > 0)
						suppBytes = attr.Values[0]?.Bytes;
					else if (attrType.Oid == LdapAttributeTypes.MsDSKeyVersionNumber.Oid && attr.Values.Length > 0)
						kvno = values[0] as int?;
				}
				else
				{
					values = Array.ConvertAll(attr.Values, r => r.Bytes);
				}

				returnedAttrs.Add(new LdapAttribute(attrType, values));
			}

			if (suppBytes != null)
			{
				try
				{
					var suppCreds = SamServer.DecodeSupplementalCredential(kvno, suppBytes);
					if (suppCreds.KerberosKeys.Length > 0)
						returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "kerberosKeys"), suppCreds.KerberosKeys));
					if (suppCreds.KerberosOldKeys.Length > 0)
						returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "kerberosOldKeys"), suppCreds.KerberosOldKeys));
					if (suppCreds.CleartextPassword != null)
						returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "cleartextPassword"), [suppCreds.CleartextPassword]));
				}
				catch
				{
				}
			}

			LdapEntry entry = new LdapEntry(obj.Name.Name, returnedAttrs.ToArray());
			return entry;
		}
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
