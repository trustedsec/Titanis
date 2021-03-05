using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies an RPC version.
	/// </summary>
	[PduStruct]
	[TypeConverter(typeof(RpcVersionConverter))]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public partial struct RpcVersion : IEquatable<RpcVersion>
	{
		[PduField]
		public ushort Major { get; set; }
		[PduField]
		public ushort Minor { get; set; }

		public RpcVersion(ushort major, ushort minor)
		{
			this.Major = major;
			this.Minor = minor;
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.Major}.{this.Minor}";

		public override bool Equals(object? obj)
		{
			return obj is RpcVersion version && this.Equals(version);
		}

		public bool Equals(RpcVersion other)
		{
			return this.Major == other.Major &&
				   this.Minor == other.Minor;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.Major, this.Minor);
		}

		public static bool operator ==(RpcVersion left, RpcVersion right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RpcVersion left, RpcVersion right)
		{
			return !(left == right);
		}
	}

	public class RpcVersionConverter : TypeConverter
	{
		private static readonly Regex rgxVersion = new Regex(@"^(?<m>\d+)\.(?<n>\d+)$");
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				var m = rgxVersion.Match(str);
				if (!m.Success)
					throw new ArgumentException("Cannot parse version.  Version must be of the form <major>.<minor>.");

				return new RpcVersion(ushort.Parse(m.Groups["m"].Value), ushort.Parse(m.Groups["n"].Value));
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
