using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1Oid : IEquatable<Asn1Oid>
	{
		internal Asn1OidPart[] _subparts;
		public bool IsEmpty => this._subparts.IsNullOrEmpty();

		internal Asn1OidPart _part0;
		internal Asn1OidPart _part1;

		private int _mask;
		private bool HasPart0 => 0 != (this._mask & 1);
		private bool HasPart1 => 0 != (this._mask & 2);

		public Asn1OidPart this[int index]
		{
			get
			{
				if ((uint)index >= (uint)this.Count)
					throw new ArgumentOutOfRangeException(nameof(index));

				return index switch
				{
					0 => this._part0,
					1 => this._part1,
					_ => this._subparts[index - 2]
				};
			}
		}

		// TODO: Can OID parts be zero?
		public int Count =>
			((this._subparts != null) ? this._subparts.Length : 0)
			+ (this.HasPart0 ? 1 : 0)
			+ (this.HasPart1 ? 1 : 0)
			;
		public Asn1Tag Tag => Asn1PredefTag.ObjectIdentifier;

		public Asn1Oid(
			Asn1OidPart part0
			)
		{
			this._part0 = part0;
			this._mask = 1;
			this._part1 = 0;
			this._subparts = null;
		}

		public Asn1Oid(string str)
			: this(new Oid(str))
		{

		}

		public Asn1Oid(Oid oid)
		{
			if (oid is null)
				throw new ArgumentNullException(nameof(oid));

			string[] tokens = oid.Value.Split('.');
			if (tokens.Length > 0)
			{
				this._part0 = int.Parse(tokens[0]);
				if (tokens.Length > 1)
				{
					this._part1 = int.Parse(tokens[1]);
					this._mask = 1 | 2;

					if (tokens.Length > 2)
					{
						Asn1OidPart[] parts = new Asn1OidPart[tokens.Length - 2];
						for (int i = 0; i < parts.Length; i++)
						{
							parts[i] = new Asn1OidPart(int.Parse(tokens[i + 2]));
						}
						this._subparts = parts;
						return;
					}
				}
				else
				{
					this._part1 = 0;
					this._mask = 1;
				}
			}
			else
			{
				this._part0 = this._part1 = this._mask = 0;
			}
			this._subparts = null;
		}

		public Asn1Oid(
			Asn1OidPart part0,
			Asn1OidPart part1,
			params Asn1OidPart[] parts)
		{
			this._part0 = part0;
			this._part1 = part1;
			this._mask = 1 | 2;
			this._subparts = parts;
		}

		public override bool Equals(object obj)
		{
			return obj is Asn1Oid oid && Equals(oid);
		}

		public bool Equals(Asn1Oid other)
		{
			// TODO: Compare the array values
			return EqualityComparer<Asn1OidPart[]>.Default.Equals(_subparts, other._subparts);
		}

		public override int GetHashCode()
		{
			int hashCode = -1185072511;
			hashCode = hashCode * -1521134295 + EqualityComparer<Asn1OidPart>.Default.GetHashCode(this._part0);
			hashCode = hashCode * -1521134295 + EqualityComparer<Asn1OidPart>.Default.GetHashCode(this._part1);
			for (int i = 0; i < this._subparts.Length; i++)
			{
				hashCode = hashCode * -1521134295 + EqualityComparer<Asn1OidPart>.Default.GetHashCode(this._subparts[i]);
			}
			return hashCode;
		}

		public static bool operator ==(Asn1Oid left, Asn1Oid right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Oid left, Asn1Oid right)
		{
			return !(left == right);
		}

		public static Asn1Oid FromParts(Asn1OidPart[] parts)
		{
			Asn1OidPart part0 = new Asn1OidPart();
			Asn1OidPart part1 = new Asn1OidPart();
			Asn1OidPart[] etc = null;
			if (parts != null)
			{
				if (parts.Length > 0)
				{
					part0 = parts[0];
					if (parts.Length > 1)
					{
						part1 = parts[1];
						if (parts.Length > 2)
						{
							etc = new Asn1OidPart[parts.Length - 2];
							Array.Copy(parts, 2, etc, 0, parts.Length - 2);
						}
					}
				}
			}

			return new Asn1Oid(part0, part1, etc);
		}

		public Oid ToOid()
		{
			return new Oid(this.ToString());
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			if (this.HasPart0)
			{
				sb.Append(this._part0.Value);
				if (this.HasPart1)
				{
					sb.Append('.')
						.Append(this._part1.Value);
					if (this._subparts != null)
					{
						foreach (var part in this._subparts)
						{
							sb.Append('.')
								.Append(part.Value);
						}
					}
				}
			}

			return sb.ToString();
		}
	}
}