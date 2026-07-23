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
		struct ParamInfo
		{
			internal ParameterMetadata param;
			internal ParameterAttribute attr;
		}

		internal CommandMetadata(
			ICustomTypeDescriptor typeDescr,
			Type implementingType,
			CommandMetadataContext context
			)
		{
			if (typeDescr is null) throw new ArgumentNullException(nameof(typeDescr));
			if (context is null) throw new ArgumentNullException(nameof(context));

			// UNDONE: This can now be used with generic objects
			//if (!context.Resolver.ReflectType(typeof(CommandBase)).IsAssignableFrom(implementingType))
			//	throw new ArgumentException(string.Format(Messages.Cli_NonCommandType, implementingType.FullName), nameof(implementingType));

			this.ImplementingType = implementingType;

			var typeAttrs = typeDescr.GetAttributes();
			OutputRecordTypeAttribute? atrOutput = null;
			foreach (var attr in typeAttrs)
			{
				if (attr is DescriptionAttribute desc)
					this.Description = desc.Description;
				else if (attr is OutputRecordTypeAttribute outrec)
					atrOutput = outrec;
			}

			if (atrOutput != null)
			{
				this.OutputRecordType = atrOutput.RecordType;
				this.DefaultOutputStyle = atrOutput.DefaultOutputStyle;
				this.DefaultOutputFields = atrOutput.DefaultFields;
			}

			var paramMd = DiscoverParameters(this, context, typeDescr, this.OutputRecordType is not null);
			this.Parameters = paramMd.Parameters;
			this.PositionalParameters = paramMd.PositionalParameters;
			this.ParametersByName = paramMd.ParametersByName;
			this.ParameterGroups = paramMd.ParameterGroups;
		}

		public static CommandMetadata DiscoverParameters(object instance)
			{
			if (instance is null) throw new ArgumentNullException(nameof(instance));

			MetadataResolver resolver = new ReflectionMetadataResolver();
			CommandMetadataContext context = new CommandMetadataContext(resolver);
			return new CommandMetadata(resolver.GetDescriptor(instance), instance?.GetType(), context);
		}

		static ParamObjectMetadata DiscoverParameters(
			CommandMetadata owner,
			CommandMetadataContext context,
			ICustomTypeDescriptor ownerDescr,
			bool isOutputDeclared
			)
		{
				Dictionary<string, ParameterMetadata> paramsByName = new Dictionary<string, ParameterMetadata>(StringComparer.OrdinalIgnoreCase);
				List<ParameterMetadata> parameters = new List<ParameterMetadata>();
				SortedList<int, ParameterMetadata> positional = new SortedList<int, ParameterMetadata>();
				List<ParamInfo> relposParams = new List<ParamInfo>();

			HashSet<Type> paramGroupTypes = new HashSet<Type>();
			Queue<(ICustomTypeDescriptor, ParameterGroupInfo)> groupQueue = new();
				List<ParameterGroupInfo> groups = new List<ParameterGroupInfo>();
			groupQueue.Enqueue((ownerDescr, new ParameterGroupInfo(ParameterGroupOptions.None)));
				while (groupQueue.Count > 0)
				{
				(var groupTypeDescr, var group) = groupQueue.Dequeue();
					groups.Add(group);

				var props = groupTypeDescr.GetProperties();
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

							if (paramGroupTypes.Add(propType))
							{
							ParameterGroupInfo subgroup = new ParameterGroupInfo(
								group,
								prop,
								ctor,
								prop.GetCustomAttribute<CategoryAttribute>(true)?.Category,
								groupAttr.Options
								);
							groupQueue.Enqueue((context.Resolver.GetDescriptor(propType), subgroup));
						}
							else
							{
								throw new InvalidOperationException($"Parameter group '{propType.FullName}' is included more than once.  Property={prop.ComponentType.FullName}.{prop.Name}");
							}
						}

						if (attr is null)
							continue;

					if (!isOutputDeclared && 0 != (attr.Flags & ParameterFlags.AffectsOutput))
							continue;

					ParameterMetadata parm = new ParameterMetadata(prop, attr, group, owner, context);
						parameters.Add(parm);

						int position;
						if (!string.IsNullOrEmpty(attr.After))
						{
							position = ParameterAttribute.NoPosition;
							relposParams.Add(new ParamInfo { param = parm, attr = attr });
						}
						else
							position = attr.Position;

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

			var parameterGroups = new ReadOnlyCollection<ParameterGroupInfo>(groups);

			ReadOnlyCollection<ParameterMetadata> posParams;
				if (relposParams.Count > 0)
				{
					LinkedList<ParameterMetadata> s = new LinkedList<ParameterMetadata>(positional.Values);
				List<ParameterMetadata> orderedParams = new List<ParameterMetadata>();

					while (s.Count > 0)
					{
						var parm = s.First.Value;
						s.RemoveFirst();

					orderedParams.Add(parm);

						var afters = relposParams.FindAll(r => r.attr.After == parm.Name);
						if (afters.Count > 0)
						{
							foreach (var after in afters)
							{
								relposParams.Remove(after);
								s.AddFirst(after.param);
							}
						}
					}

					if (relposParams.Count > 0)
					{
						var relpos = relposParams[0];
						throw new MetadataException($"Parameter declaration for '{relpos.param.Name}' specifies relative positioning to '{relpos.attr.After}', but no parameter named '{relpos.attr.After}' has been declared.", relpos.param.Name);
					}

				posParams = new ReadOnlyCollection<ParameterMetadata>(orderedParams);
				}
				else
				{
				posParams = new ReadOnlyCollection<ParameterMetadata>(positional.Values);
				}

			return new ParamObjectMetadata(
				new ReadOnlyCollection<ParameterMetadata>(parameters),
				posParams,
				new ReadOnlyDictionary<string, ParameterMetadata>(paramsByName),
				parameterGroups
				);
		}

		public string? Description { get; }
		public Type? ImplementingType { get; }
		public IReadOnlyList<ParameterGroupInfo> ParameterGroups { get; }
		public IReadOnlyList<ParameterMetadata> Parameters { get; }
		public IReadOnlyList<ParameterMetadata> PositionalParameters { get; }
		public IReadOnlyDictionary<string, ParameterMetadata> ParametersByName { get; }

		public Type? OutputRecordType { get; }
		public OutputStyle DefaultOutputStyle { get; }
		public string[]? DefaultOutputFields { get; }
	}
}
