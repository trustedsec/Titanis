using System;
using System.Diagnostics;
using System.Text;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{
	[Flags]
	// [MS-WMIO] § 2.2.62 - QualifierFlavor
	public enum WmiQualifierFlavor : byte
	{
		None = 0,
		PropagateToInstance = 1,
		PropagateToDerivedClass = 2,
		NotOverridable = 0x10,
		OriginPropagated = 0x20,
		OriginSystem = 0x40,
		Amended = 0x80,
	}

	public sealed class WmiQualifier
	{
		public WmiQualifier(
			string name,
			WmiQualifierFlavor flavor,
			CimType type,
			object? value)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

			WmiProperty.CheckValue(type, CimSubtype.Unspecified, value);

			this.Name = name;
			this.Flavor = flavor;
			this.QualifierType = type;
			this.Value = value;
		}

		public WmiQualifier(
			string name,
			WmiQualifierFlavor flavor
			)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

			this.Name = name;
			this.Flavor = flavor;
		}

		public sealed override string ToString()
			=> (this.Value == null)
			? $"[{this.Name}]"
			: $"[{this.Name}({this.Value})]";

		public string Name { get; }
		public WmiQualifierFlavor Flavor { get; }
		public bool IsInherited => 0 != (this.Flavor & WmiQualifierFlavor.OriginPropagated);
		public bool NotOverridable => 0 != (this.Flavor & WmiQualifierFlavor.NotOverridable);
		public CimType QualifierType { get; }
		public object? Value { get; }

		internal void ToMof(StringBuilder sb)
		{
			sb.Append('[')
				.Append(this.Name)
				;
			if (this.Value != null)
			{
				sb.Append('(');
				// TODO: Proper MOF encode value
				if (this.Value is object[] values)
				{
					bool first = true;
					foreach (var value in values)
					{
						if (first)
							first = false;
						else
							sb.Append(", ");

						sb.AppendMofValue(value);
					}
				}
				else
					sb.AppendMofValue(this.Value);
				sb.Append(')');
			}
			sb.Append(']');
		}

		// [MS-WMIO] § 2.2.60 - Qualifier
		internal Qualifier Encode(ByteWriter heapWriter)
		{
			Qualifier qual = new Qualifier
			{
				name = heapWriter.WriteHeapString(this.Name),
				flavor = this.Flavor,
				qualifierType = this.QualifierType,
				value = EncodedValue.EncodeValue(this.QualifierType, CimSubtype.Unspecified, this.Value, heapWriter)
			};
			return qual;
		}
	}
}