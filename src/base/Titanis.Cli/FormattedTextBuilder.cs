using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis.Cli
{
	public class FormattedTextBuilder
	{
		public FormattedTextBuilder()
		{

		}

		private List<FormattedTextPart> _parts = new List<FormattedTextPart>();

		public FormattedTextBuilder Append(FormattedTextPart part)
		{
			if (part is null) throw new ArgumentNullException(nameof(part));
			this._parts.Add(part);
			return this;
		}
		public FormattedTextBuilder Text(string text) => this.Append(new TextPart(text));
		public FormattedTextBuilder Bold() => this.Append(new PushTextStylePart(FormattedTextStyles.Bold));
		public FormattedTextBuilder Italic() => this.Append(new PushTextStylePart(FormattedTextStyles.Italic));
		public FormattedTextBuilder PopStyle() => this.Append(new PopTextStylePart());
		public FormattedTextBuilder Bold(string text) => this.Bold().Text(text).PopStyle();
		public FormattedTextBuilder Italic(string text) => this.Italic().Text(text).PopStyle();

		public FormattedTextBuilder Link(string text, string linkTarget)
		{
			this._parts.Add(new LinkTextPart(text, linkTarget));
			return this;
		}

		public FormattedText Build() => new FormattedText(this._parts.ToArray());
	}
}
