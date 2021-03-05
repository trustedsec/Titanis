using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Titanis.Msrpc.Mswmi
{
	public sealed class WmiMethod : WmiMetadataElement
	{
		public WmiMethod(string? name, string originClassName, WmiQualifierFlavor flags, WmiQualifier[] qualifiers, WmiClassObject? inputSig, WmiClassObject outputSig)
			: base(name, originClassName, qualifiers)
		{
			this.Flags = flags;
			this.InputSignature = inputSig;
			this.OutputSignature = outputSig;
		}

		public sealed override string ToString()
			=> this.Name;

		public WmiQualifierFlavor Flags { get; }
		[Browsable(false)]
		public WmiClassObject? InputSignature { get; }
		[Browsable(false)]
		public WmiClassObject OutputSignature { get; }
		private string? _sig;
		public string Signature => (this._sig ??= this.BuildSignature());

		private string BuildSignature()
		{
			StringBuilder sb = new StringBuilder();

			if (this.IsStatic)
				sb.Append("static ");

			var outClass = this.OutputSignature;
			if (outClass != null)
			{
				var returnProp = outClass.GetProperty(WmiMethodBuilder.ReturnValueName);
				if (returnProp != null)
					sb.Append(returnProp.PropertyType.AsCSharpType()).Append(' ');
				else
					sb.Append("void ");
			}
			else
				sb.Append("void ");

			bool first = true;

			sb.Append(this.Name).Append('(');
			var inClass = this.InputSignature;
			if (inClass != null)
			{
				foreach (var prop in inClass.Properties)
				{
					if (first)
						first = false;
					else
						sb.Append(", ");

					sb.Append(prop.PropertyType.AsCSharpType())
						.Append(' ')
						.Append(prop.Name);
				}
			}

			if (outClass != null)
			{
				foreach (var prop in outClass.Properties)
				{
					if (prop.Name == WmiMethodBuilder.ReturnValueName)
						continue;

					if (first)
						first = false;
					else
						sb.Append(", ");

					sb.Append("out ")
						.Append(prop.PropertyType.AsCSharpType())
						.Append(' ')
						.Append(prop.Name);
				}

			}

			sb.Append(')');

			return sb.ToString();
		}
	}
}