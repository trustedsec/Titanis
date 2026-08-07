using Microsoft.Data.Sqlite;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Winterop.Security;

namespace Titanis.Info
{
	static class DataHelpers
	{
		public static async Task ExecuteCommandText(this SqliteTransaction? txact, string commandText, CancellationToken cancellationToken)
		{
			using (SqliteCommand cmd = new SqliteCommand(commandText, txact.Connection, txact))
			{
				await cmd.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		private readonly static ImmutableArray<Type> typesByCode = [
			typeof(SecurityDescriptor),

			// 0
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			null,
			typeof(string),
			];

		public static Type GetTypeFromCode(TypeCode tc)
		{
			Type type = typesByCode[(int)tc + 1];
			type ??= typeof(object);
			return type;
		}

		internal static bool IsIntegral(this TypeCode tc) =>
			tc is TypeCode.Byte
			or TypeCode.SByte
			or TypeCode.Int16
			or TypeCode.Int32
			or TypeCode.Int64
			or TypeCode.UInt16
			or TypeCode.UInt32
			or TypeCode.UInt64
			;


		internal static SqliteType SqliteTypeFromType(Type type) => SqliteTypeFromType(type, false);
		private static SqliteType SqliteTypeFromType(Type type, bool nested) => Type.GetTypeCode(type) switch
		{
			TypeCode.Boolean => SqliteType.Integer,
			TypeCode.Byte => SqliteType.Integer,
			TypeCode.Char => SqliteType.Text,
			TypeCode.DateTime => SqliteType.Real,
			TypeCode.Decimal => SqliteType.Real,
			TypeCode.Double => SqliteType.Real,
			TypeCode.Int16 => SqliteType.Integer,
			TypeCode.Int32 => SqliteType.Integer,
			TypeCode.Int64 => SqliteType.Integer,
			TypeCode.Object => GetObjectType(type, nested),
			TypeCode.SByte => SqliteType.Integer,
			TypeCode.Single => SqliteType.Real,
			TypeCode.String => SqliteType.Text,
			TypeCode.UInt16 => SqliteType.Integer,
			TypeCode.UInt32 => SqliteType.Integer,
			TypeCode.UInt64 => SqliteType.Integer,
		};

		private static SqliteType GetObjectType(Type type, bool nested)
		{
			if (!nested)
			{
				var attr = type.GetCustomAttribute<InfoValueAttribute>();
				if (attr != null)
					return SqliteTypeFromType(attr.DataType, true);
			}
			return (type == typeof(byte[])) ? SqliteType.Blob : SqliteType.Text;
		}
		internal static object ToDataValue(object? value) => ToDataValue(value, false);
		private static object ToDataValue(object? value, bool nested)
		{
			if (value is byte[] bytes)
				return bytes;

			switch (Convert.GetTypeCode(value))
			{
				case TypeCode.Boolean:
					return ((bool)value ? 1 : 0);
				case TypeCode.Byte:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.Single:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
					return Convert.ToInt64(value);
				case TypeCode.UInt64:
					return Convert.ToUInt64(value);
				case TypeCode.Char:
					return value.ToString();
				case TypeCode.DateTime:
					return value;
				case TypeCode.DBNull:
				case TypeCode.Empty:
					return DBNull.Value;
				case TypeCode.String:
					return value;
				case TypeCode.Object:
				default:
					if (!nested && value is IInfoValue info)
						return ToDataValue(info.GetValue(), true);
					else
						return (object?)(value?.ToString()) ?? DBNull.Value;
			}
		}

		private static byte GetNullableContext(Type type)
		{
			var attrData = type.GetCustomAttributesData();
			foreach (var attr in attrData)
			{
				if (attr.AttributeType.Name == "NullableContextAttribute")
				{
					if (attr.ConstructorArguments.Count == 1
						&& attr.ConstructorArguments[0].Value is byte b
						)
						return b;
					else
						return 1;
				}
			}

			return 1;
		}
		private static bool AllowsNull(MemberInfo property, byte context)
		{
			var attrData = property.GetCustomAttributesData();
			foreach (var attr in attrData)
			{
				if (attr.AttributeType.Name == "NullableAttribute")
				{
					if (attr.ConstructorArguments.Count == 1
						&& attr.ConstructorArguments[0].Value is byte b
						)
						return b == 2;
					else if (attr.ConstructorArguments.Count == 1
						&& attr.ConstructorArguments[0].Value is byte[] bs
						)
						return Array.IndexOf(bs, 2) >= 0;
				}
			}

			return (context == 2);
		}
		internal static string GenerateCreateTableScript(Type rowType)
		{
			if (rowType is null) throw new ArgumentNullException(nameof(rowType));

			StringBuilder sb = new StringBuilder();
			sb.Append($"CREATE TABLE[{rowType.Name}](");
			var props = rowType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			PropertyInfo? pkProp = null;
			var nullContext = GetNullableContext(rowType);
			for (int i = 0; i < props.Length; i++)
			{
				if (i > 0)
					sb.Append(',');
				PropertyInfo? prop = props[i];
				var elemType = prop.PropertyType;
				bool nullable = false;
				{
					var nullType = Nullable.GetUnderlyingType(elemType);
					if (nullType != null)
					{
						nullable = true;
						elemType = nullType;
					}
					else if (prop.PropertyType.IsClass && AllowsNull(prop, nullContext))
					{
						nullable = true;
					}
				}
#if DEBUG
				if (prop.Name.StartsWith("ErrorDetails"))
					//if (prop.Name == "ErrorDetails_")
					;
#endif

				string coltypeKeyword = GetColTypeKeyword(elemType);
				bool isPk = prop.IsDefined(typeof(PrimaryKeyAttribute));
				if (isPk)
				{
					if (pkProp != null)
						throw new ArgumentException($"Property '{prop.Name}' cannot be marked as a primary key because '{pkProp.Name}' is already the primary key.");
					if (nullable)
						throw new ArgumentException($"Property '{prop.Name}' cannot be marked as both primary key and nullable.");

					pkProp = prop;
				}

				sb.Append($"[{prop.Name}] {coltypeKeyword}{(isPk ? " PRIMARY KEY" : nullable ? "" : " NOT NULL")}");
			}
			if (pkProp is null)
				throw new ArgumentException($"The type '{rowType.FullName}' lacks a primary key.", nameof(rowType));
			sb.Append(");");

			return sb.ToString();
		}

		internal static string GetColTypeKeyword(Type elemType)
		{
			var coltype = SqliteTypeFromType(elemType);
			var coltypeKeyword = coltype switch
			{
				SqliteType.Integer => "INTEGER",
				SqliteType.Real => "REAL",
				SqliteType.Text => "TEXT",
				SqliteType.Blob => "BLOB",
			};
			return coltypeKeyword;
		}
	}
}
