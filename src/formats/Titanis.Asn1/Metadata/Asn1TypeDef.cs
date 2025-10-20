using System;

namespace Titanis.Asn1.Metadata
{
	public class Asn1TypeDef
	{
		public Asn1TypeDef(string name, Asn1Type definition)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));
			if (definition is null)
				throw new ArgumentNullException(nameof(definition));

			if (!IsValidName(name))
				throw new ArgumentException(string.Format(Messages.Asn1_TypeNameInvalid, name), nameof(name));

			this.Name = name;
			this.Definition = definition;
		}

		public string Name { get; private set; }
		public Asn1Type Definition { get; private set; }

		public sealed override string ToString() => $"{this.Name} ::= {this.Definition.DefinitionString}";

		public void Accept(IModuleVisitor visitor)
		{
			ArgumentNullException.ThrowIfNull(visitor);
			visitor.Visit(this);
		}

		public static bool IsValidName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (char.IsUpper(name[0]))
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