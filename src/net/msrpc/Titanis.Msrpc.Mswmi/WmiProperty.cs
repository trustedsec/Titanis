using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{
	public sealed class WmiProperty : WmiMetadataElement
	{
		internal WmiProperty(
			string name,
			CimType propertyTypeInheritedFlag,
			int classOfOriginIndex,
			string classOfOrigin,
			int declarationOrder,
			WmiQualifier[] qualifiers,
			uint valueOffset,
			object? defaultValue,
			NdFlags ndFlags
			)
			: base(name, classOfOrigin, qualifiers)
		{
			this._propertyTypeInheritedFlag = propertyTypeInheritedFlag;
			this.ClassOfOriginIndex = classOfOriginIndex;
			this.DeclarationOrder = declarationOrder;
			this.ValueTableOffset = valueOffset;
			this.NdFlags = ndFlags;

			if (defaultValue != null)
			{
				if (!CheckValue(defaultValue))
					throw new ArgumentException("The default value does not match the property type.", nameof(DefaultValue));
			}
			this.DefaultValue = defaultValue;
		}

		// [MS-WMIO] § 2.2.32 - Inherited
		internal const CimType PropertyInheritedFlag = (CimType)0x4000;

		internal PropertyInfo Encode(ByteWriter heapWriter)
		{
			return new PropertyInfo
			{
				propertyType = this.PropertyType,
				declarationOrder = (ushort)this.DeclarationOrder,
				valueTableOffset = this.ValueTableOffset,
				classOfOrigin = this.ClassOfOriginIndex,
				qualifierSet = new QualifierSet(this.Qualifiers.ConvertAll(r => r.Encode(heapWriter)))
			};
		}

		[Browsable(false)]
		public int ClassOfOriginIndex { get; }

		private readonly CimType _propertyTypeInheritedFlag;
		public CimType PropertyType => _propertyTypeInheritedFlag & ~PropertyInheritedFlag;
		[Browsable(false)]
		public CimType PropertyTypeInheritedFlag => _propertyTypeInheritedFlag;

		[Browsable(false)]
		public int DeclarationOrder { get; }

		[Browsable(false)]
		public uint ValueTableOffset { get; }
		public object? DefaultValue
		{
			get;
			// TODO: Verify the default value
			internal set;
		}
		[Browsable(false)]
		public NdFlags NdFlags { get; internal set; }

		public bool CheckValue(object value)
			=> CheckValue(this.PropertyType, this.SubtypeCode, value);


		public static Type GetRuntimeTypeFor(CimType propType, CimSubtype subtype)
		{
			return propType switch
			{
				CimType.SInt8 => typeof(sbyte),
				CimType.UInt8 => typeof(byte),
				CimType.SInt16 => typeof(short),
				CimType.UInt16 => typeof(ushort),
				CimType.SInt32 => typeof(int),
				CimType.UInt32 => typeof(uint),
				CimType.SInt64 => typeof(long),
				CimType.UInt64 => typeof(ulong),
				CimType.Real32 => typeof(float),
				CimType.Real64 => typeof(double),
				CimType.Boolean => typeof(bool),
				CimType.String => typeof(string),
				CimType.DateTime => subtype switch
				{
					CimSubtype.Interval => typeof(WmiInterval),
					_ => typeof(WmiDateTime)
				},
				CimType.Reference => typeof(WmiReference),
				CimType.Char16 => typeof(char),
				CimType.Object => typeof(WmiObject),
				_ => throw new ArgumentException($"The CimType {propType}/{subtype} does not have a corresponding runtime type."),
			};
		}

		public static bool CheckValue(CimType propType, CimSubtype subtype, object? value)
		{
			if ((propType == CimType.None) != (value == null))
				throw new ArgumentException("CimType.None may only be used with value == null.", nameof(value));

			// Just to be nice
			propType &= ~PropertyInheritedFlag;

			if (0 != (propType & CimType.Array))
			{
				if (value is Array arr)
				{
					var elemType = propType & CimType.BaseTypeMask;
					for (int i = 0; i < arr.Length; i++)
					{
						var elem = arr.GetValue(i);
						if (elem != null)
						{
							if (!CheckValue(elemType, subtype, elem))
								return false;
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return propType switch
				{
					CimType.SInt8 => (value is sbyte),
					CimType.UInt8 => (value is byte),
					CimType.SInt16 => (value is short),
					CimType.UInt16 => (value is ushort),
					CimType.SInt32 => (value is int),
					CimType.UInt32 => (value is uint),
					CimType.SInt64 => (value is long),
					CimType.UInt64 => (value is ulong),
					CimType.Real32 => (value is float),
					CimType.Real64 => (value is double),
					CimType.Boolean => (value is bool),
					CimType.String => (value is string),
					CimType.DateTime => subtype switch
					{
						CimSubtype.Interval => (value is WmiInterval),
						_ => (value is WmiDateTime)
					},
					CimType.Reference => (value is WmiReference),
					CimType.Char16 => (value is char),
					CimType.Object => (value is WmiObject),
					_ => false,
				};
			}
		}

		#region MOF
		private string? _str;

		public sealed override string ToString() => (this._str ??= this.BuildString());

		private string? BuildString()
		{
			StringBuilder sb = new StringBuilder();
			this.ToMof(sb, null);
			return sb.ToString();
		}

		internal void ToMof(StringBuilder sb, string? indent)
		{
			foreach (var qual in this.Qualifiers)
			{
				sb.Append(indent);
				qual.ToMof(sb);
				sb.AppendLine();
			}

			sb
				.Append(indent)
				.AppendCimType(this.PropertyType)
				.Append(' ')
				.Append(this.Name)
				.Append(';')
				.AppendLine();
		}

		internal void WriteValue(byte[] valueTable, object? value, ByteWriter heapWriter)
		{
			valueTable.WriteEncodedValue((int)this.ValueTableOffset, this.PropertyType, this.SubtypeCode, value, heapWriter);
		}
		#endregion
	}
}