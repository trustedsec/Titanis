using System;

namespace Titanis.Asn1.Metadata
{
	public class Asn1ValueDef
	{
		public Asn1ValueDef(string name, Asn1Type type, object value)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			if (!IsValidName(name))
				throw new ArgumentException(string.Format(Messages.Asn1_ValueNameInvalid, name), nameof(name));

			this.Name = name;
			this.Type = type;
			this.Value = value;
		}

		public string Name { get; private set; }
		public Asn1Type Type { get; }
		public object Value { get; private set; }

		internal void Accept(IModuleVisitor visitor)
		{
			visitor.Visit(this);
		}

		public static bool IsValidName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (char.IsLower(name[0]))
				{
					bool isValid = true;
					for (int i = 1; isValid && i < name.Length; i++)
					{
						char c = name[i];
						isValid =
							(c == '-' && name[i - 1] != '-')
							|| (char.IsUpper(c))
							|| (char.IsLower(c))
							|| (char.IsDigit(c))
							;
					}
					return isValid;
				}
			}
			return false;
		}
	}
}