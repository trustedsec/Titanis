using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Describes command metadata.
	/// </summary>
	/// <see cref="Command.GetCommandMetadata(Type, CommandMetadataContext)"/>
	public class CommandMetadata
	{
		internal CommandMetadata(
			Type implementingType,
			CommandMetadataContext context
			)
		{
			if (implementingType is null) throw new ArgumentNullException(nameof(implementingType));
			if (context is null) throw new ArgumentNullException(nameof(context));

			if (!context.Resolver.ReflectType(typeof(Command)).IsAssignableFrom(implementingType))
				throw new ArgumentException(Messages.Cli_NonCommandType, nameof(implementingType));

			this.ImplementingType = implementingType;
			this.Description = context.Resolver.GetCustomAttribute<DescriptionAttribute>(implementingType, true)?.Description;

			var atrOutput = context.Resolver.GetCustomAttribute<OutputRecordTypeAttribute>(implementingType, true);
			if (atrOutput != null)
			{
				this.OutputRecordType = atrOutput.RecordType;
				this.DefaultOutputStyle = atrOutput.DefaultOutputStyle;
				this.DefaultOutputFields = atrOutput.DefaultFields;
			}

			{
				Dictionary<string, ParameterMetadata> paramsByName = new Dictionary<string, ParameterMetadata>(StringComparer.OrdinalIgnoreCase);
				List<ParameterMetadata> parameters = new List<ParameterMetadata>();
				SortedList<int, ParameterMetadata> positional = new SortedList<int, ParameterMetadata>();

				Queue<ParameterGroupInfo> groupQueue = new Queue<ParameterGroupInfo>();
				List<ParameterGroupInfo> groups = new List<ParameterGroupInfo>();
				groupQueue.Enqueue(new ParameterGroupInfo(implementingType, ParameterGroupOptions.None));
				while (groupQueue.Count > 0)
				{
					var group = groupQueue.Dequeue();
					var groupType = group.GroupType;
					groups.Add(group);

					var props = context.GetProperties(groupType);
					foreach (PropertyDescriptor prop in props)
					{
						ParameterAttribute? attr = prop.GetCustomAttribute<ParameterAttribute>(true);
						ParameterGroupAttribute? groupAttr = prop.GetCustomAttribute<ParameterGroupAttribute>(true);
						if (groupAttr is not null)
						{
							if (attr is not null)
								throw new ArgumentException("A property can either have ParameterAttribute or ParameterGroupAttribute, but not both.", prop.Name);

							var propType = prop.PropertyType;
							if (!propType.IsClass)
								throw new MetadataException("A parameter group must be of a class type.", prop.Name);
							if (propType.IsAbstract)
								throw new MetadataException("A parameter group must be of an abstract class.", prop.Name);

							var ctor = propType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
							if (ctor is null)
								throw new MetadataException("A parameter group class must have a parameterless constructor.", prop.Name);

							ParameterGroupInfo subgroup = new ParameterGroupInfo(
								propType,
								group,
								prop,
								ctor,
								prop.GetCustomAttribute<CategoryAttribute>(true)?.Category,
								groupAttr.Options
								);
							groupQueue.Enqueue(subgroup);
						}

						if (attr is null)
							continue;

						if (OutputRecordType is null && 0 != (attr.Flags & ParameterFlags.OutputOnly))
							continue;

						int position = attr.Position;

						ParameterMetadata parm = CreateParameter(prop, attr, group, context);
						parameters.Add(parm);

						if (position != ParameterAttribute.NoPosition)
							positional.Add(position, parm);

						if (paramsByName.ContainsKey(parm.Name))
							throw new MetadataException(string.Format(Messages.Cli_DuplicateParamName, parm.Name), parm.Name);
						paramsByName.Add(parm.Name, parm);

						foreach (var alias in parm.Aliases)
						{
							if (paramsByName.ContainsKey(alias))
								throw new MetadataException(string.Format(Messages.Cli_DuplicateParamName, alias), null);
							paramsByName.Add(alias, parm);
						}
					}
				}

				this.ParameterGroups = new ReadOnlyCollection<ParameterGroupInfo>(groups);
				this.Parameters = new ReadOnlyCollection<ParameterMetadata>(parameters);
				this.PositionalParameters = new ReadOnlyCollection<ParameterMetadata>(positional.Values);
				this.ParametersByName = new ReadOnlyDictionary<string, ParameterMetadata>(paramsByName);
			}
		}


		private ParameterMetadata CreateParameter(
			PropertyDescriptor property,
			ParameterAttribute attr,
			ParameterGroupInfo? group,
			CommandMetadataContext context)
		{
			ParameterMetadata parm = new ParameterMetadata(
				property,
				attr,
				group,
				this,
				context
				);
			return parm;
		}

		public string? Description { get; }
		public Type ImplementingType { get; }
		public IReadOnlyList<ParameterGroupInfo> ParameterGroups { get; }
		public IReadOnlyList<ParameterMetadata> Parameters { get; }
		public IReadOnlyList<ParameterMetadata> PositionalParameters { get; }
		public IReadOnlyDictionary<string, ParameterMetadata> ParametersByName { get; }

		public Type? OutputRecordType { get; }
		public OutputStyle DefaultOutputStyle { get; }
		public string[]? DefaultOutputFields { get; }
	}
}
