using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	public class WmiMetadataElement
	{
		protected WmiMetadataElement(
			string name,
			string classOfOrigin,
			WmiQualifier[]? qualifiers
			)
		{
			this.Name = name;
			this.ClassOfOrigin = classOfOrigin;
			this.Qualifiers = qualifiers;

			if (this.Qualifiers != null)
			{
				foreach (var qual in this.Qualifiers)
				{
					switch (qual.Name.ToUpper())
					{
						case "SUBTYPE":
							{
								var subtype = qual.Value as string;
								this.Subtype = subtype;
								if (subtype != null)
								{
									this.Subtype = subtype;
									Enum.TryParse(subtype, true, out this._subtypeCode);
								}
							}
							break;
						case "READ":
							{
								if (qual.Value is bool b)
									this.IsReadOnly = b;
							}
							break;
						case "STATIC":
							{
								if (qual.Value is bool b)
									this.IsStatic = b;
							}
							break;
						case "PRIVILEGES":
							this.Privileges = qual.Value as string[];
							break;
						case "DESCRIPTION":
							this.FullDescription = qual.Value as string;
							{
								int sep;
								if (this.FullDescription != null && ((sep = this.FullDescription.IndexOfAny(LinebrakChars)) >= 0))
								{
									this.ShortDescription = this.FullDescription.Substring(0, sep);
								}
								else
									this.ShortDescription = this.FullDescription;
							}
							break;
					}
				}
			}
		}

		private static readonly char[] LinebrakChars = new char[] { '\r', '\n' };

		public string Name { get; }

		public string ClassOfOrigin { get; }

		[Browsable(false)]
		public WmiQualifier[] Qualifiers { get; }

		private string? BuildQualifiersText()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var qual in this.Qualifiers)
			{
				if (sb.Length > 0)
					sb.Append(", ");

				qual.ToMof(sb);
			}

			return sb.ToString();
		}

		private string? _qualifiersText;
		[DisplayName("Qualifiers")]
		public string? QualifiersText => (this._qualifiersText ??= this.BuildQualifiersText());

		#region Qualifier metadata
		public string Subtype { get; }
		private CimSubtype _subtypeCode;
		public CimSubtype SubtypeCode => this._subtypeCode;

		[Browsable(false)]
		public string[]? Privileges { get; }
		[DisplayName("Privileges")]
		public string? PrivilegesText => (this.Privileges != null) ? string.Join(", ", this.Privileges) : null;

		public bool IsReadOnly { get; }

		public string ShortDescription { get; }
		public string FullDescription { get; }
		public bool IsStatic { get; }
		#endregion
	}
}
