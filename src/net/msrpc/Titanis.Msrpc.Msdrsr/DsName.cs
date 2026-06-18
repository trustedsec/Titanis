using ms_drsr;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Titanis.DceRpc;
using Titanis.Ldap;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msdrsr
{
	/// <summary>
	/// Names an object within Directory Services
	/// </summary>
	[TypeConverter(typeof(DsNameConverter))]
	public sealed class DsName
	{
		public DsName(
			Guid guid,
			SecurityIdentifier? sid,
			LdapDistinguishedName? name)
		{
			if (sid != null)
			{
				if (sid.BinaryLength > 28)
					throw new ArgumentException($"SID '{sid.ToSddlString()}' is not a valid domain SID.", nameof(sid));
			}

			this.Sid = sid;
			this.Guid = guid;
			this.Name = name;
		}

		internal DsName(DSNAME dsname)
		{
			if (dsname.SidLen > 0)
				this.Sid = new SecurityIdentifier(dsname.Sid.Data);
			this.Guid = dsname.Guid;
			if (dsname.NameLen > 0)
				this.Name = new LdapDistinguishedName(new string(dsname.StringName.Slice(0, (int)dsname.NameLen)));
		}

		public sealed override string ToString()
		{
			return this.Name?.ToString() ?? this.Sid?.ToString() ?? this.Guid.ToString();
		}

		public SecurityIdentifier? Sid { get; }
		public Guid Guid { get; set; }
		public LdapDistinguishedName? Name { get; set; }

		public static implicit operator DsName(SecurityIdentifier sid) => new DsName(Guid.Empty, sid, null);

		internal DceRpc.RpcPointer<ms_drsr.DSNAME> ToRpcDsName()
		{
			int sidLength;
			byte[]? sidBytes;
			if (this.Sid is null)
			{
				sidLength = 0;
				sidBytes = new byte[28];
			}
			else
			{
				sidLength = this.Sid.BinaryLength;
				sidBytes = this.Sid.GetBytes();
				if (sidBytes.Length < 28)
					Array.Resize(ref sidBytes, 28);
			}

			var name = this.Name?.Text;
			var dsname = new RpcPointer<ms_drsr.DSNAME>(new DSNAME
			{
				structLen = 62,
				SidLen = (uint)sidLength,
				Guid = this.Guid,
				Sid = new ms_drsr.NT4SID { Data = sidBytes },
				NameLen = (uint)(name?.Length ?? 0),
				StringName = (this.Name is null) ? new char[] { '\0' } : (name + '\0').ToCharArray()
			});

			return dsname;
		}

		public static bool TryParse(string str, [NotNullWhen(true)] out DsName? dsName)
		{
			if (str.StartsWith("S-1-5-"))
			{
				dsName = new DsName(default, SecurityIdentifier.Parse(str), null);
				return true;
			}
			else if (Guid.TryParse(str, out var guid))
			{
				dsName = new DsName(guid, null, null);
				return true;
			}
			else if (LdapDistinguishedName.TryParse(str, out var dn, out _, out _) && dn.Rdns.LastOrDefault()?.Type == "DC")
			{
				dsName = new DsName(default, null, dn);
				return true;
			}

			dsName = null;
			return false;
		}
		public static DsName Parse(string str)
		{
			if (!TryParse(str, out var dsName))
				throw new ArgumentException($"The string must be either a security identifier, an object GUID, or a distinguished name.", nameof(str));

			return dsName;
		}
	}

	public class DsNameConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return
				(sourceType == typeof(string))
				|| (sourceType == typeof(SecurityIdentifier))
				|| (sourceType == typeof(Guid))
				|| base.CanConvertFrom(context, sourceType);
		}

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				return DsName.Parse(str);
			}
			else if (value is Guid guid)
			{
				return new DsName(guid, null, null);
			}
			else if (value is SecurityIdentifier sid)
			{
				return new DsName(default, sid, null);
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}
