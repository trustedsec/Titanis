using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	internal sealed class EnumNameListProvider : ValueListProvider
	{
		public EnumNameListProvider(IEnumerable<Type> enumTypes)
		{
			if (enumTypes is null)
				throw new ArgumentNullException(nameof(enumTypes));
			if (!enumTypes.All(r => r.IsEnum))
				throw new ArgumentException("The types must be Enum types.");

			HashSet<string> names = new HashSet<string>(
				enumTypes.SelectMany(r => r.GetEnumNames()));
			this.Names = names.ToArray();
		}

		public string[] Names { get; }

		public sealed override Array? GetValueListFor(ParameterMetadata parameter, object? command, CommandMetadataContext context)
		{
			return this.Names;
		}
	}

}