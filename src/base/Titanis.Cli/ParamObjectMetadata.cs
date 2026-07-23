using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Titanis.Cli
{
	internal class ParamObjectMetadata
	{
		public ParamObjectMetadata(
			ReadOnlyCollection<ParameterMetadata> parameters,
			ReadOnlyCollection<ParameterMetadata> positionalParameters,
			IReadOnlyDictionary<string, ParameterMetadata> parametersByName,
			ReadOnlyCollection<ParameterGroupInfo> parameterGroups
			)
		{
			Parameters = parameters;
			PositionalParameters = positionalParameters;
			ParametersByName = parametersByName;
			ParameterGroups = parameterGroups;
		}

		public ReadOnlyCollection<ParameterMetadata> Parameters { get; }
		public ReadOnlyCollection<ParameterMetadata> PositionalParameters { get; }
		public IReadOnlyDictionary<string, ParameterMetadata> ParametersByName { get; }
		public ReadOnlyCollection<ParameterGroupInfo> ParameterGroups { get; }
	}
}
