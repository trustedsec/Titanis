using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Titanis.Winterop.Security.Conditions
{
	public class Condition
	{
		public Condition(ConditionExpression expression)
		{
			if (expression is null) throw new ArgumentNullException(nameof(expression));
			Expression = expression;
		}

		public ConditionExpression Expression { get; }

		public int BinaryLength => SecurityDescriptor.Align4(4 + this.Expression.BinaryLength);
		public byte[] ToBytes()
		{
			var buffer = new byte[this.BinaryLength];

			var length = this.GetBytes(buffer);
			length = SecurityDescriptor.Align4(length);
			Debug.Assert(length == this.BinaryLength);
			return buffer;
		}
		public int GetBytes(Span<byte> buffer)
		{
			if (buffer.Length < 4)
				throw new ArgumentException($"Condition buffer too small.", nameof(buffer));

			// [MS-DTYP] § 2.4.4.17.4 - Conditional ACE Binary Formats
			buffer[0] = 0x61;
			buffer[1] = 0x72;
			buffer[2] = 0x74;
			buffer[3] = 0x78;

			int length = this.Expression.GetBytes(buffer.Slice(4));
			return 4 + length;
		}
	}
	public abstract class ConditionExpression
	{
		public abstract int BinaryLength { get; }
		public abstract int GetBytes(Span<byte> buffer);
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public enum ConditionUnaryOperator : byte
	{
		MemberOf = 0x89,
		DeviceMemberOf = 0x8a,
		MemberOfAny = 0x8b,
		DeviceMemberOfAny = 0x8c,
		NotMemberOf = 0x90,
		NotDeviceMemberOf = 0x91,
		NotMemberOfAny = 0x82,
		NotDeviceMemberOfAny = 0x93,

		// [MS-DTYP] 2.4.4.17.7 Logical Operator Tokens
		Exists = 0x87,
		NotExists = 0x8d,
		LogicalNot = 0xa2,
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public class ConditionUnaryOperation : ConditionExpression
	{
		public ConditionUnaryOperation(ConditionUnaryOperator op, ConditionExpression operand)
		{
			Operator = op;
			Operand = operand;
		}

		public ConditionUnaryOperator Operator { get; }
		public ConditionExpression Operand { get; }

		public sealed override int BinaryLength => 1 + this.Operand.BinaryLength;

		public sealed override int GetBytes(Span<byte> buffer)
		{
			int length = this.Operand.GetBytes(buffer);
			buffer[length] = (byte)this.Operator;
			return length + 1;
		}
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public enum ConditionBinaryOperator
	{
		EqualsTo = 0x80,
		NotEqualTo = 0x81,
		LessThan = 0x82,
		LessOrEqual = 0x83,
		GreaterThan = 0x84,
		GreaterOrEqual = 0x85,
		Contains = 0x86,
		AnyOf = 0x88,
		NotContains = 0x8e,
		NotAnyOf = 0x8f,

		// [MS-DTYP] 2.4.4.17.7 Logical Operator Tokens
		LogicalAnd = 0xA0,
		LogicalOr = 0xA1,
	}

	public class ConditionBinaryOperation : ConditionExpression
	{
		public ConditionBinaryOperation(ConditionBinaryOperator op, ConditionExpression left, ConditionExpression right)
		{
			Operator = op;
			Left = left;
			Right = right;
		}

		public ConditionBinaryOperator Operator { get; }
		public ConditionExpression Left { get; }
		public ConditionExpression Right { get; }

		public sealed override int BinaryLength => 1 + this.Left.BinaryLength + this.Right.BinaryLength;

		public sealed override int GetBytes(Span<byte> buffer)
		{
			int length = this.Left.GetBytes(buffer);
			length += this.Right.GetBytes(buffer.Slice(length));
			buffer[length] = (byte)this.Operator;
			return length + 1;
		}
	}

	// [MS-DTYP] 2.4.4.17.8 - Attribute Tokens
	public enum ConditionAttributeKind
	{
		Local = 0xF8,
		User = 0xF9,
		Resource = 0xFA,
		Device = 0xFB,
	}

	public sealed class ConditionAttribute : ConditionExpression
	{
		public ConditionAttribute(ConditionAttributeKind kind, string name)
		{
			Kind = kind;
			Name = name;
		}

		public ConditionAttributeKind Kind { get; }
		public string Name { get; }

		public sealed override int BinaryLength => 1 + 4 + Encoding.Unicode.GetByteCount(this.Name);
		public sealed override int GetBytes(Span<byte> buffer)
		{
			buffer[0] = (byte)this.Kind;
			int length = 1 + ConditionStringLiteral.EncodeString(buffer.Slice(1), this.Name);
			return length;
		}
	}

	// [MS-DTYP] § 2.4.4.17.5 - Literal Tokens
	public enum ConditionLiteralToken : byte
	{
		Invalid = 0,
		Byte = 1,
		Int16 = 2,
		Int32 = 3,
		Int64 = 4,
		String = 0x10,
		OctetString = 0x18,
		Composite = 0x50,
		Sid = 51
	}
	// [MS-DTYP] § 2.4.4.17.5 - Literal Tokens
	public enum ConditionBaseToken : byte
	{
		Octal = 1,
		Decimal = 2,
		Hex = 3,
	}
	// [MS-DTYP] § 2.4.4.17.5 - Literal Tokens
	public enum ConditionSignToken : byte
	{
		Plus = 1,
		Minus = 2,
		None = 3
	}

	public abstract class ConditionLiteralValue : ConditionExpression
	{
		public abstract ConditionLiteralToken LiteralToken { get; }

		protected abstract int GetLiteralBytes(Span<byte> buffer);

		public abstract int LiteralBinaryLength { get; }
		public sealed override int BinaryLength => 1 + /* token */ this.LiteralBinaryLength;

		public sealed override int GetBytes(Span<byte> buffer)
		{
			buffer[0] = (byte)this.LiteralToken;
			int valueLength = this.GetLiteralBytes(buffer.Slice(1));

			return 1 + valueLength;
		}
	}

	public sealed class ConditionSidLiteral : ConditionLiteralValue
	{
		public ConditionSidLiteral(SecurityIdentifier sid)
		{
			Sid = sid;
		}

		public sealed override int LiteralBinaryLength => 1 /* length */ + this.Sid.BinaryLength;

		public override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Sid;

		public SecurityIdentifier Sid { get; }

		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			buffer[0] = (byte)this.Sid.BinaryLength;
			int length = this.Sid.GetBytes(buffer.Slice(1));
			return 1 + length;
		}

		internal static int EncodeString(Span<byte> buffer, string str)
		{
			int cb = Encoding.Unicode.GetBytes(str, buffer.Slice(4));
			BinaryPrimitives.WriteInt32LittleEndian(buffer, cb);

			return 4 + cb;
		}
	}

	public sealed class ConditionStringLiteral : ConditionLiteralValue
	{
		public ConditionStringLiteral(string value)
		{
			Value = value;
		}

		public string Value { get; }

		public sealed override int LiteralBinaryLength => 4 /* length */ + Encoding.Unicode.GetByteCount(this.Value);

		public override ConditionLiteralToken LiteralToken => ConditionLiteralToken.String;

		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			return EncodeString(buffer, this.Value);
		}

		internal static int EncodeString(Span<byte> buffer, string str)
		{
			int cb = Encoding.Unicode.GetBytes(str, buffer.Slice(4));
			BinaryPrimitives.WriteInt32LittleEndian(buffer, cb);

			return 4 + cb;
		}
	}

	public abstract class ConditionNumericLiteral : ConditionLiteralValue
	{
		protected abstract int ValueBinaryLength { get; }
		public sealed override int LiteralBinaryLength => this.ValueBinaryLength + 1 /* sign */ + 1 /* base */;

		public ConditionSignToken Sign => ConditionSignToken.None;
		public ConditionBaseToken Base => ConditionBaseToken.Decimal;

		protected abstract int GetValueBytes(Span<byte> buffer);
		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			int valueLength = this.GetValueBytes(buffer);
			buffer[valueLength] = (byte)this.Sign;
			buffer[valueLength + 1] = (byte)this.Base;

			return valueLength + 2;
		}
	}

	public sealed class ConditionByteLiteral : ConditionNumericLiteral
	{
		public ConditionByteLiteral(byte value)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Byte;
		public byte Value { get; }
		/// <inheritdoc/>
		protected sealed override int ValueBinaryLength => 1;

		/// <inheritdoc/>
		protected sealed override int GetValueBytes(Span<byte> buffer)
		{
			buffer[0] = this.Value;
			return 1;
		}
	}

	public sealed class ConditionInt16Literal : ConditionNumericLiteral
	{
		public ConditionInt16Literal(short value)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int16;
		public short Value { get; }
		/// <inheritdoc/>
		protected sealed override int ValueBinaryLength => 2;

		/// <inheritdoc/>
		protected sealed override int GetValueBytes(Span<byte> buffer)
		{
			BinaryPrimitives.WriteInt16LittleEndian(buffer, this.Value);
			return 2;
		}
	}

	public sealed class ConditionInt32Literal : ConditionNumericLiteral
	{
		public ConditionInt32Literal(int value)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int32;
		public int Value { get; }
		/// <inheritdoc/>
		protected sealed override int ValueBinaryLength => 4;

		/// <inheritdoc/>
		protected sealed override int GetValueBytes(Span<byte> buffer)
		{
			BinaryPrimitives.WriteInt32LittleEndian(buffer, this.Value);
			return 4;
		}
	}

	public sealed class ConditionInt64Literal : ConditionNumericLiteral
	{
		public ConditionInt64Literal(long value)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int64;
		public long Value { get; }
		/// <inheritdoc/>
		protected sealed override int ValueBinaryLength => 8;

		/// <inheritdoc/>
		protected sealed override int GetValueBytes(Span<byte> buffer)
		{
			BinaryPrimitives.WriteInt64LittleEndian(buffer, this.Value);
			return 8;
		}
	}
}
