using ms_drsr;
using System.ComponentModel;
using System.Globalization;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msdrsr
{
	[TypeConverter(typeof(DsNameConverter))]
	public class DsName
	{
		public DsName(
			Guid guid,
			SecurityIdentifier? sid,
			string? name)
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
			this.Sid = new SecurityIdentifier(dsname.Sid.Data);
			this.Guid = dsname.Guid;
			if (dsname.NameLen > 0)
				this.Name = new string(dsname.StringName.Slice(0, (int)dsname.NameLen));
		}

		public SecurityIdentifier? Sid { get; }
		public Guid Guid { get; set; }
		public string? Name { get; set; }

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

			var dsname = new RpcPointer<ms_drsr.DSNAME>(new DSNAME
			{
				structLen = 62,
				SidLen = (uint)sidLength,
				Guid = this.Guid,
				Sid = new ms_drsr.NT4SID { Data = sidBytes },
				NameLen = (uint)(this.Name?.Length ?? 0),
				StringName = (this.Name is null) ? new char[] { '\0' } : (this.Name + '\0').ToCharArray()
			});

			return dsname;
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
				if (str.StartsWith("S-"))
				{
					return new DsName(default, SecurityIdentifier.Parse(str), null);
				}
				else if (Guid.TryParse(str, out var guid))
				{
					return new DsName(guid, null, null);
				}
				else if (str.Contains('='))
				{
					return new DsName(default, null, str);
				}
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
