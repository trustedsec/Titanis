using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Titanis.Winterop.Security.Conditions
{
	public sealed class Condition
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

		public override string ToString() => this.Expression.ToString();
		public void ToSddlString(StringBuilder sb) => this.Expression.ToSddlString(sb);


		ref struct ConditionParseContext
		{
			public ConditionParseContext(ReadOnlySpan<byte> bytes)
			{
				this.bytes = bytes;
			}

			ReadOnlySpan<byte> bytes;
			int readIndex;

			private byte ReadByte() => this.bytes[this.readIndex++];
			private ReadOnlySpan<byte> Consume(int count)
			{
				var consumed = this.bytes.Slice(this.readIndex, count);
				this.readIndex += count;
				return consumed;
			}

			private ConditionLiteralValue ReadLiteral(Token t)
			{
				return t switch
				{
					Token.Invalid => throw new NotImplementedException(),
					Token.Int8 => new ConditionByteLiteral(this.ReadByte(), (ConditionSignToken)this.ReadByte(), (ConditionBaseToken)this.ReadByte()),
					Token.Int16 => new ConditionInt16Literal(BinaryPrimitives.ReadInt16LittleEndian(this.Consume(2)), (ConditionSignToken)this.ReadByte(), (ConditionBaseToken)this.ReadByte()),
					Token.Int32 => new ConditionInt32Literal(BinaryPrimitives.ReadInt32LittleEndian(this.Consume(4)), (ConditionSignToken)this.ReadByte(), (ConditionBaseToken)this.ReadByte()),
					Token.Int64 => new ConditionInt64Literal(BinaryPrimitives.ReadInt64LittleEndian(this.Consume(8)), (ConditionSignToken)this.ReadByte(), (ConditionBaseToken)this.ReadByte()),
					Token.Utf16String => new ConditionStringLiteral(this.ReadUtf16Literal()),
					Token.OctetString => this.ReadOctetLiteral(),
					Token.Composite => this.ReadComposite(),
					Token.Sid => this.ReadSid(),
					_ => throw new InvalidDataException($"Expected literal value but encountered token {t}.")
				};
			}
			internal ConditionExpression ReadExpression(int softEndIndex)
			{
				Stack<ConditionExpression> exprStack = new Stack<ConditionExpression>();
				while (this.readIndex < this.bytes.Length)
				{
					var b = this.ReadByte();
					if (b == 0 && exprStack.Count == 1 && this.readIndex >= softEndIndex)
						// This is padding at the end
						break;
					var expr = (Token)b switch
					{
						Token.Invalid => throw new NotImplementedException(),

						Token.Int8
						or Token.Int16
						or Token.Int32
						or Token.Int64
						or Token.Utf16String
						or Token.OctetString
						or Token.Composite
						or Token.Sid => (ConditionExpression)this.ReadLiteral((Token)b),

						// Unary
						Token.Exists
						or Token.NotExists
						or Token.LogicalNot
						or Token.MemberOf
						or Token.DeviceMemberOf
						or Token.MemberOfAny
						or Token.DeviceMemberOfAny
						or Token.NotMemberOf
						or Token.NotDeviceMemberOf
						or Token.NotMemberOfAny
						or Token.NotDeviceMemberOfAny => new ConditionUnaryOperation((ConditionUnaryOperator)b, exprStack.Pop()),

						// Binary
						Token.EqualsTo
						or Token.NotEqualTo
						or Token.LessThan
						or Token.LessOrEqual
						or Token.GreaterThan
						or Token.GreaterOrEqual
						or Token.Contains
						or Token.AnyOf
						or Token.NotContains
						or Token.NotAnyOf
						or Token.LogicalAnd
						or Token.LogicalOr => new ConditionBinaryOperation((ConditionBinaryOperator)b, exprStack.Pop(), exprStack.Pop(), true),

						// Attributes
						Token.LocalAttr
						or Token.UserAttr
						or Token.ResourceAttr
						or Token.DeviceAttr => new ConditionAttribute((ConditionAttributeKind)b, this.ReadUtf16Literal()),

						_ => throw new InvalidDataException($"Unsupported token {b} in conditional ACE.")
					};
					exprStack.Push(expr);
				}

				return exprStack.Pop();
			}

			private ConditionComposite ReadComposite()
			{
				int length = BinaryPrimitives.ReadInt32LittleEndian(this.Consume(4));
				int endIndex = this.readIndex + length;
				var values = ImmutableArray.CreateBuilder<ConditionLiteralValue>();
				while (this.readIndex < endIndex)
				{
					var value = this.ReadLiteral((Token)this.ReadByte());
					values.Add(value);
				}
				return new ConditionComposite(values.ToImmutable());
			}

			private string ReadUtf16Literal()
			{
				int length = BinaryPrimitives.ReadInt32LittleEndian(this.Consume(4));
				// TODO: This only uses .ToArray() because of compliance with older .NET
				string str = Encoding.Unicode.GetString(this.Consume(length).ToArray());
				return str;
			}

			private ConditionOctetStringLiteral ReadOctetLiteral()
			{
				int length = BinaryPrimitives.ReadInt32LittleEndian(this.Consume(4));
				return new ConditionOctetStringLiteral(this.Consume(length).ToArray());
			}

			private ConditionSidLiteral ReadSid()
			{
				int length = BinaryPrimitives.ReadInt32LittleEndian(this.Consume(4));
				return new ConditionSidLiteral(new SecurityIdentifier(this.Consume(length).ToArray()));
			}
		}
		// [MS-DTYP] § 2.4.4.17 Conditional ACEs
		private const int ConditionMagic = 0x78747261;
		// [MS-DTYP] § 2.4.4.17 Conditional ACEs
		public static bool IsCondition(ReadOnlySpan<byte> bytes) => bytes.Length > 4 && BinaryPrimitives.ReadUInt32LittleEndian(bytes) == ConditionMagic;
		// [MS-DTYP] § 2.4.4.17 Conditional ACEs
		public static Condition Parse(ReadOnlySpan<byte> bytes)
		{
			if (!IsCondition(bytes))
				throw new ArgumentException($"The bytes do not constitute a condition.", nameof(bytes));

			int endIndex = bytes.Length - 1;
			// Count potential padding bytes
			for (; endIndex > 0 && bytes[endIndex] == 0; endIndex--)
				;
			endIndex++;
			var ctx = new ConditionParseContext(bytes.Slice(4));
			return new Condition(ctx.ReadExpression(endIndex - 4));
		}
		public static Condition? TryParse(ReadOnlySpan<byte> bytes)
		{
			if (IsCondition(bytes))
				return Parse(bytes);
			else
				return null;
		}

		enum ExprPrecedence
		{
			None = 0,
			Exists,
			Relational,
			Not,
			And,
			Or,
		}
		struct ExprParseContext
		{
			internal ExprPrecedence prec;
			internal Token nextOp;
			internal ExprPrecedence nextPrec;
		}

		private static ConditionExpression ParseParen(ref SddlParseContext ctx, ref ExprParseContext parseCtx)
		{
			ctx.Expect('(');
			var inner = ParseExpression(ref ctx, ref parseCtx);
			ctx.Expect(')');
			return inner;
		}
		internal static Condition Parse(ref SddlParseContext ctx)
		{
			ExprParseContext parseCtx = new ExprParseContext();
			return new Condition(ParseExpression(ref ctx, ref parseCtx));
		}
		private static ConditionExpression ParsePrefixedAttribute(ref SddlParseContext ctx, ref ExprParseContext parseCtx)
		{
			ctx.Expect('@');
			int count = ctx.chars.CountMatching(r => char.IsLetter(r) || r is '_');
			if (count == 0)
				throw new InvalidDataException($"Expected prefix after @");

			var prefix = ctx.chars.Consume(count);
			var kind =
				prefix.Equals("User".AsSpan(), StringComparison.OrdinalIgnoreCase) ? ConditionAttributeKind.User
				: prefix.Equals("Device".AsSpan(), StringComparison.OrdinalIgnoreCase) ? ConditionAttributeKind.Device
				: prefix.Equals("Resource".AsSpan(), StringComparison.OrdinalIgnoreCase) ? ConditionAttributeKind.Resource
				: throw new InvalidDataException($"Unrecognized attribute prefix {prefix.ToString()}");
			ctx.Expect('.');

			string attrName = ReadAttrName(ref ctx);
			return new ConditionAttribute(kind, attrName);
		}

		private static string ReadAttrName(ref SddlParseContext ctx)
		{
			int cch = ctx.chars.CountMatching(r => char.IsLetterOrDigit(r) || r is ':' or '/' or '-' or '_');
			var name = ctx.chars.Consume(cch).ToString();
			return name;
		}

		record struct ConditionOpInfo(string Keyword, Token token, ExprPrecedence precedence);
		private static ConditionExpression ParseExpression(ref SddlParseContext ctx, ref ExprParseContext parseCtx, ConditionUnaryOperator unaryOp = 0)
		{
			var left = ParseOperand(ref ctx, ref parseCtx);
			ctx.chars.Skip(' ');

			var c = ctx.PeekChar();
			bool terminal = (c is ')' or -1 or ',');
			if (terminal)
			{
				parseCtx.nextOp = 0;
				parseCtx.nextPrec = 0;
				if (unaryOp != 0)
					left = new ConditionUnaryOperation(unaryOp, left);
				return left;
			}

			var binaryOp = ReadBinaryOp(ref ctx, ref parseCtx, out var prec);
			do
			{
				if (prec > parseCtx.prec)
				{
					var prev = parseCtx.prec;
					try
					{
						var right = ParseExpression(ref ctx, ref parseCtx);
						ConditionExpression expr = new ConditionBinaryOperation((ConditionBinaryOperator)binaryOp, left, right);
						if (unaryOp != 0)
							expr = new ConditionUnaryOperation(unaryOp, expr);

						return expr;
					}
					finally
					{
						parseCtx.prec = prev;
					}
				}
				else
				{
					parseCtx.nextOp = binaryOp;
					parseCtx.nextPrec = prec;
					return left;
				}
			} while (parseCtx.nextOp != 0);

			throw new NotImplementedException();
		}

		private static readonly ImmutableArray<ConditionOpInfo> binaryOperators = [
			new ConditionOpInfo("==", Token.EqualsTo, ExprPrecedence.Relational),
			new ConditionOpInfo("!=", Token.NotEqualTo, ExprPrecedence.Relational),
			new ConditionOpInfo("<", Token.LessThan, ExprPrecedence.Relational),
			new ConditionOpInfo("<=", Token.LessOrEqual, ExprPrecedence.Relational),
			new ConditionOpInfo(">", Token.GreaterThan, ExprPrecedence.Relational),
			new ConditionOpInfo(">=", Token.GreaterOrEqual, ExprPrecedence.Relational),
			new ConditionOpInfo("Contains", Token.Contains, ExprPrecedence.Relational),
			new ConditionOpInfo("Any_of", Token.AnyOf, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Contains", Token.NotContains, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Any_of", Token.NotAnyOf, ExprPrecedence.Relational),
			new ConditionOpInfo("&&", Token.LogicalAnd, ExprPrecedence.And),
			new ConditionOpInfo("||", Token.LogicalOr, ExprPrecedence.Or),
			];
		private static readonly ImmutableArray<ConditionOpInfo> unaryOperators = [
			new ConditionOpInfo("Member_of_Any", Token.MemberOfAny, ExprPrecedence.Relational),
			new ConditionOpInfo("Member_of", Token.MemberOf, ExprPrecedence.Relational),
			new ConditionOpInfo("Device_Member_of_Any", Token.DeviceMemberOfAny, ExprPrecedence.Relational),
			new ConditionOpInfo("Device_Member_of", Token.DeviceMemberOf, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Member_of_Any", Token.NotMemberOfAny, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Member_of", Token.NotMemberOf, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Device_Member_of_Any", Token.NotDeviceMemberOfAny, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Device_Member_of", Token.NotDeviceMemberOf, ExprPrecedence.Relational),
			new ConditionOpInfo("Exists", Token.Exists, ExprPrecedence.Relational),
			new ConditionOpInfo("Not_Exists", Token.NotExists, ExprPrecedence.Relational),
			new ConditionOpInfo("!", Token.LogicalNot, ExprPrecedence.Not),
			];
		private static Token ReadBinaryOp(ref SddlParseContext ctx, ref ExprParseContext parseCtx, out ExprPrecedence prec)
		{
			foreach (var op in binaryOperators)
			{
				if (ctx.chars.StartsWith(op.Keyword.AsSpan(), true))
				{
					ctx.chars.Consume(op.Keyword.Length);
					prec = op.precedence;
					return op.token;
				}
			}

			prec = ExprPrecedence.None;
			return 0;
		}

		private static ConditionExpression ParseOperand(ref SddlParseContext ctx, ref ExprParseContext parseCtx)
		{
			ctx.chars.Skip(' ');

			var n = ctx.PeekChar();
			if (n == '(')
				return ParseParen(ref ctx, ref parseCtx);
			else if (n == '@')
			{
				return ParsePrefixedAttribute(ref ctx, ref parseCtx);
			}
			else if (n == '!')
			{
				ctx.Advance(1);
				parseCtx.prec = ExprPrecedence.Not;
				return ParseUnary(ConditionUnaryOperator.LogicalNot, ref ctx, ref parseCtx);
			}
			else if (n == '"')
			{
				throw new NotImplementedException();
			}
			else if (char.IsNumber((char)n))
			{
				return ParseNumber(ref ctx, ConditionSignToken.None);
			}
			else if (n is '+' or '-')
			{
				ConditionSignToken sign = n switch
				{
					'+' => ConditionSignToken.Plus,
					'-' => ConditionSignToken.Minus,
				};
				ctx.Advance(1);
				return ParseNumber(ref ctx, sign);
			}
			else if (n is '{')
			{
				ctx.Expect('{');
				ctx.chars.Skip(' ');
				var values = ImmutableArray.CreateBuilder<ConditionLiteralValue>();
				while (ctx.chars.Peek() is not '}')
				{
					var operand = (ConditionLiteralValue)ParseOperand(ref ctx, ref parseCtx);
					values.Add(operand);
					ctx.chars.Skip(' ');
					ctx.chars.AdvanceIf(',');
				}
				ctx.chars.Skip(' ');
				ctx.chars.Expect('}');
				return new ConditionComposite(values.ToImmutable());
			}
			else if (n is 'S' && ctx.PeekChar(1) == '-')
			{
				var sid = SecurityIdentifier.Parse(ref ctx, null);
				return new ConditionSidLiteral(sid);
			}
			else if (char.IsLetter((char)n))
			{
				foreach (var op in unaryOperators)
				{
					if (ctx.chars.StartsWith(op.Keyword.AsSpan(), true))
					{
						ctx.chars.Advance(op.Keyword.Length);
						var prev = parseCtx.prec;
						try
						{
							parseCtx.prec = op.precedence;
							var operand = ParseExpression(ref ctx, ref parseCtx, (ConditionUnaryOperator)op.token);
							return operand;
						}
						finally
						{
							parseCtx.prec = prev;
						}
					}
				}

				return new ConditionAttribute(ConditionAttributeKind.Local, ReadAttrName(ref ctx));
			}
			else
			{
				throw new NotImplementedException();
			}
			throw new NotImplementedException();
		}

		private static ConditionNumericLiteral ParseNumber(ref SddlParseContext ctx, ConditionSignToken sign)
		{
			// TODO: Base
			long value = 0;
			while (char.IsNumber((char)ctx.PeekChar() /* EOF => -1 => 0xFFFF, which isn't a number */))
			{
				var c = ctx.PeekChar();
				ctx.Advance(1);
				value *= 10;
				value += (uint)(c - '0');
			}
			if (value > int.MaxValue)
				return new ConditionInt64Literal(value, sign, ConditionBaseToken.Decimal);
			else if (value > byte.MaxValue)
				return new ConditionInt32Literal((int)value, sign, ConditionBaseToken.Decimal);
			else
				return new ConditionByteLiteral((byte)value, sign, ConditionBaseToken.Decimal);
		}

		private static ConditionExpression ParseUnary(ConditionUnaryOperator logicalNot, ref SddlParseContext ctx, ref ExprParseContext parseCtx)
		{
			throw new NotImplementedException();
		}
	}

	enum Token
	{
		// [MS-DTYP] § 2.4.4.17.5 Literal Tokens
		Invalid = 0,
		Int8 = 1,
		Int16 = 2,
		Int32 = 3,
		Int64 = 4,
		Utf16String = 0x10,
		OctetString = 0x18,
		Composite = 0x50,
		Sid = 0x51,

		// Binary
		// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
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

		// Unary
		// [MS-DTYP] 2.4.4.17.7 Logical Operator Tokens
		// Unary Logical Operators
		Exists = 0x87,
		NotExists = 0x8d,
		LogicalNot = 0xa2,

		// Unary Relational Operators
		MemberOf = 0x89,
		DeviceMemberOf = 0x8a,
		MemberOfAny = 0x8b,
		DeviceMemberOfAny = 0x8c,
		NotMemberOf = 0x90,
		NotDeviceMemberOf = 0x91,
		NotMemberOfAny = 0x82,
		NotDeviceMemberOfAny = 0x93,

		// [MS-DTYP] 2.4.4.17.8 - Attribute Tokens
		LocalAttr = 0xF8,
		UserAttr = 0xF9,
		ResourceAttr = 0xFA,
		DeviceAttr = 0xFB,
	}

	public abstract class ConditionExpression
	{
		public abstract int BinaryLength { get; }
		public abstract int GetBytes(Span<byte> buffer);

		public abstract void ToSddlString(StringBuilder sb);
		public sealed override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			this.ToSddlString(sb);
			return sb.ToString();
		}
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public enum ConditionUnaryOperator : byte
	{
		// [MS-DTYP] 2.4.4.17.7 Logical Operator Tokens
		// Unary Logical Operators
		Exists = Token.Exists,
		NotExists = Token.NotExists,
		LogicalNot = Token.LogicalNot,

		// Unary Relational Operators
		MemberOf = Token.MemberOf,
		DeviceMemberOf = Token.DeviceMemberOf,
		MemberOfAny = Token.MemberOfAny,
		DeviceMemberOfAny = Token.DeviceMemberOfAny,
		NotMemberOf = Token.NotMemberOf,
		NotDeviceMemberOf = Token.NotDeviceMemberOf,
		NotMemberOfAny = Token.NotMemberOfAny,
		NotDeviceMemberOfAny = Token.NotDeviceMemberOfAny,
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public sealed class ConditionUnaryOperation : ConditionExpression
	{
		public ConditionUnaryOperation(ConditionUnaryOperator op, ConditionExpression operand)
		{
			if (operand is null) throw new ArgumentNullException(nameof(operand));
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

		private string Keyword => this.Operator switch
		{
			ConditionUnaryOperator.Exists => "Exists",
			ConditionUnaryOperator.NotExists => "Not_Exists",
			ConditionUnaryOperator.LogicalNot => "!",
			ConditionUnaryOperator.MemberOf => "Member_of",
			ConditionUnaryOperator.DeviceMemberOf => "Device_Member_of",
			ConditionUnaryOperator.MemberOfAny => "Member_of_Any",
			ConditionUnaryOperator.DeviceMemberOfAny => "Device_Member_of_Any",
			ConditionUnaryOperator.NotMemberOf => "Not_Member_of",
			ConditionUnaryOperator.NotDeviceMemberOf => "Not_Device_Member_of",
			ConditionUnaryOperator.NotMemberOfAny => "Not_Member_of_Any",
			ConditionUnaryOperator.NotDeviceMemberOfAny => "Not_Device_Member_of_Any",
		};
		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append(this.Keyword)
				.Append('(');
			this.Operand.ToSddlString(sb);
			sb.Append(')');
		}
	}

	// [MS-DTYP] § 2.4.4.17.6 Relational Operator Tokens
	public enum ConditionBinaryOperator
	{
		EqualsTo = Token.EqualsTo,
		NotEqualTo = Token.NotEqualTo,
		LessThan = Token.LessThan,
		LessOrEqual = Token.LessOrEqual,
		GreaterThan = Token.GreaterThan,
		GreaterOrEqual = Token.GreaterOrEqual,
		Contains = Token.Contains,
		AnyOf = Token.AnyOf,
		// ...
		NotContains = Token.NotContains,
		NotAnyOf = Token.NotAnyOf,

		// [MS-DTYP] 2.4.4.17.7 Logical Operator Tokens
		LogicalAnd = Token.LogicalAnd,
		LogicalOr = Token.LogicalOr,
	}

	public sealed class ConditionBinaryOperation : ConditionExpression
	{
		public ConditionBinaryOperation(ConditionBinaryOperator op, ConditionExpression left, ConditionExpression right)
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			Operator = op;
			Left = left;
			Right = right;
		}
		internal ConditionBinaryOperation(ConditionBinaryOperator op, ConditionExpression right, ConditionExpression left, bool swap)
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

		private string Keyword => this.Operator switch
		{
			ConditionBinaryOperator.EqualsTo => "==",
			ConditionBinaryOperator.NotEqualTo => "!=",
			ConditionBinaryOperator.LessThan => "<",
			ConditionBinaryOperator.LessOrEqual => "<=",
			ConditionBinaryOperator.GreaterThan => ">",
			ConditionBinaryOperator.GreaterOrEqual => ">=",
			ConditionBinaryOperator.Contains => " Contains ",
			// [sic] Trailing whitespace not required
			ConditionBinaryOperator.AnyOf => " Any_of",
			ConditionBinaryOperator.NotContains => " Not_Contains ",
			ConditionBinaryOperator.NotAnyOf => " Not_Any_of",
			ConditionBinaryOperator.LogicalAnd => "&&",
			ConditionBinaryOperator.LogicalOr => "||",
			// TODO: The Not_ conditions should be expressed as ! and the positive operator
		};
		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append('(');
			this.Left.ToSddlString(sb);
			sb.Append(this.Keyword);
			this.Right.ToSddlString(sb);
			sb.Append(')');
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
			if (name is null) throw new ArgumentNullException(nameof(name));
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

		private string Prefix => this.Kind switch
		{
			ConditionAttributeKind.Local => "",
			ConditionAttributeKind.User => "@User.",
			ConditionAttributeKind.Resource => "@Resource.",
			ConditionAttributeKind.Device => "@Device.",
		};
		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append(this.Prefix).Append(this.Name);
		}
	}

	// [MS-DTYP] § 2.4.4.17.5 - Literal Tokens
	public enum ConditionLiteralToken : byte
	{
		Invalid = 0,
		Byte = Token.Int8,
		Int16 = Token.Int16,
		Int32 = Token.Int32,
		Int64 = Token.Int64,
		String = Token.Utf16String,
		OctetString = Token.OctetString,
		Composite = Token.Composite,
		Sid = Token.Sid
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
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			Sid = sid;
		}

		public sealed override int LiteralBinaryLength => 4 /* length */ + this.Sid.BinaryLength;

		public override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Sid;

		public SecurityIdentifier Sid { get; }

		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			BinaryPrimitives.WriteInt32LittleEndian(buffer, this.Sid.BinaryLength);
			int length = this.Sid.GetBytes(buffer.Slice(4));
			Debug.Assert(length == this.Sid.BinaryLength);
			return 4 + length;
		}

		public override void ToSddlString(StringBuilder sb)
		{
			this.Sid.BuildString(sb);
		}
	}

	public sealed class ConditionStringLiteral : ConditionLiteralValue
	{
		public ConditionStringLiteral(string value)
		{
			if (value is null) throw new ArgumentNullException(nameof(value));
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

		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append($"\"{Escape()}\"");
		}

		private string Escape()
		{
			// TODO: Apply escaping
			return this.Value;
		}
	}

	public sealed class ConditionOctetStringLiteral : ConditionLiteralValue
	{
		public ConditionOctetStringLiteral(byte[] bytes)
		{
			if (bytes is null) throw new ArgumentNullException(nameof(bytes));
			Bytes = bytes;
		}


		public sealed override int LiteralBinaryLength => 4 /* length */ + this.Bytes.Length;

		public override ConditionLiteralToken LiteralToken => ConditionLiteralToken.OctetString;

		public byte[] Bytes { get; }

		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			return EncodeOctets(buffer, this.Bytes);
		}

		internal static int EncodeOctets(Span<byte> buffer, ReadOnlySpan<byte> bytes)
		{
			BinaryPrimitives.WriteInt32LittleEndian(buffer, bytes.Length);
			bytes.CopyTo(buffer.Slice(4));
			return 4 + bytes.Length;
		}

		// cf. https://learn.microsoft.com/en-us/windows/win32/secauthz/security-descriptor-definition-language-for-conditional-aces-#conditional-expressions
		// "The "#" sign is synonymous with "0" in resource attributes. For example, D:AI(XA;OICI;FA;;;WD;(OctetStringType==#1#2#3##)) is equivalent to and interpreted as D:AI(XA;OICI;FA;;;WD;(OctetStringType==#01020300))."

		public override void ToSddlString(StringBuilder sb)
		{
			// TODO: Look for more examples.  Is 0 acceptable?
			sb.Append('#');
			BinaryHelper.ToHexString(this.Bytes, HexStringOptions.Lowercase, sb);
		}
	}

	public abstract class ConditionNumericLiteral : ConditionLiteralValue
	{
		protected ConditionNumericLiteral(ConditionSignToken sign, ConditionBaseToken @base)
		{
			this.Sign = sign;
			this.Base = @base;
		}

		public abstract ulong UnsignedValue { get; }
		public int Radix => this.Base switch
		{
			ConditionBaseToken.Octal => 8,
			ConditionBaseToken.Decimal => 10,
			ConditionBaseToken.Hex => 16,
		};
		private string? RadixKeyword => this.Base switch
		{
			ConditionBaseToken.Octal => "0",
			ConditionBaseToken.Decimal => null,
			ConditionBaseToken.Hex => "0x",
		};

		private string SignKeyword => this.Sign switch
		{
			ConditionSignToken.Plus => "+",
			ConditionSignToken.Minus => "-",
			ConditionSignToken.None => "",
		};
		// TODO: Convert.ToString does not accept ulong.  Use long for now and hope for the best
		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append(this.Sign)
				.Append(this.RadixKeyword)
				.Append(Convert.ToString((long)this.UnsignedValue, this.Radix))
				;
		}

		protected abstract int ValueBinaryLength { get; }
		public sealed override int LiteralBinaryLength => this.ValueBinaryLength + 1 /* sign */ + 1 /* base */;

		public ConditionSignToken Sign { get; }
		public ConditionBaseToken Base { get; }

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
		public ConditionByteLiteral(byte value, ConditionSignToken sign = ConditionSignToken.None, ConditionBaseToken @base = ConditionBaseToken.Decimal)
			: base(sign, @base)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Byte;
		public byte Value { get; }
		public sealed override ulong UnsignedValue => (ulong)this.Value;
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
		public ConditionInt16Literal(short value, ConditionSignToken sign = ConditionSignToken.None, ConditionBaseToken @base = ConditionBaseToken.Decimal)
			: base(sign, @base)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int16;
		public short Value { get; }
		public sealed override ulong UnsignedValue => (ulong)this.Value;
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
		public ConditionInt32Literal(int value, ConditionSignToken sign = ConditionSignToken.None, ConditionBaseToken @base = ConditionBaseToken.Decimal)
			: base(sign, @base)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int32;
		public int Value { get; }
		public sealed override ulong UnsignedValue => (ulong)this.Value;
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
		public ConditionInt64Literal(long value, ConditionSignToken sign = ConditionSignToken.None, ConditionBaseToken @base = ConditionBaseToken.Decimal)
			: base(sign, @base)
		{
			Value = value;
		}

		public sealed override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Int64;
		public long Value { get; }
		public sealed override ulong UnsignedValue => (ulong)this.Value;
		/// <inheritdoc/>
		protected sealed override int ValueBinaryLength => 8;

		/// <inheritdoc/>
		protected sealed override int GetValueBytes(Span<byte> buffer)
		{
			BinaryPrimitives.WriteInt64LittleEndian(buffer, this.Value);
			return 8;
		}
	}

	public sealed class ConditionComposite : ConditionLiteralValue
	{
		public ConditionComposite(ImmutableArray<ConditionLiteralValue> values)
		{
			Values = values;
		}

		public ImmutableArray<ConditionLiteralValue> Values { get; }

		public override ConditionLiteralToken LiteralToken => ConditionLiteralToken.Composite;

		public sealed override int LiteralBinaryLength => 4 + this.Values.Sum(r => r.BinaryLength);

		protected sealed override int GetLiteralBytes(Span<byte> buffer)
		{
			var cb = this.Values.Sum(r => r.BinaryLength);
			BinaryPrimitives.WriteInt32LittleEndian(buffer, cb);
			int writeIndex = 4;
			foreach (var item in this.Values)
			{
				int cbItem = item.GetBytes(buffer.Slice(writeIndex));
				Debug.Assert(cbItem == item.BinaryLength);
				writeIndex += cbItem;
			}
			Debug.Assert(writeIndex == (4 + cb));
			return writeIndex;
		}

		public override void ToSddlString(StringBuilder sb)
		{
			sb.Append('{');
			for (int i = 0; i < Values.Length; i++)
			{
				if (i > 0)
					sb.Append(',');

				ConditionLiteralValue? item = this.Values[i];
				item.ToSddlString(sb);
			}
			sb.Append('}');
		}
	}
}
