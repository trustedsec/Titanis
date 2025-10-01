using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Titanis.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies behavior of parameters.
	/// </summary>
	[Flags]
	public enum ParameterFlags
	{
		None = 0,
		/// <summary>
		/// The parameter is mandatory.
		/// </summary>
		Mandatory = 1,
		/// <summary>
		/// The parameter is a list.
		/// </summary>
		IsList = 2,
		/// <summary>
		/// The parameter is a switch.
		/// </summary>
		IsSwitch = 4,
		/// <summary>
		/// The parameter has a default value defined.
		/// </summary>
		HasDefaultValue = 8,

		/// <summary>
		/// The parameter is only available if an OutputRecordType is specified.
		/// </summary>
		OutputOnly = 0x10,
	}

	/// <summary>
	/// Specifies behavior for a parameter group.
	/// </summary>
	public enum ParameterGroupOptions
	{
		None = 0,
		/// <summary>
		/// The parameter group is instantiated even if no parameters within it are set.
		/// </summary>
		AlwaysInstantiate = 1,
		/// <summary>
		/// The user must supply parameters for the group.
		/// </summary>
		Required = 2,
	}

	/// <summary>
	/// Describes a command parameter.
	/// </summary>
	public class ParameterMetadata
	{
		internal ParameterMetadata(
			PropertyDescriptor property,
			ParameterAttribute attr,
			ParameterGroupInfo? group,
			CommandMetadata command,
			CommandMetadataContext context
			)
		{
			this.Property = property;
			this.Position = attr.Position;
			this.Group = group;
			this.DeclaringCommand = command;

			ParameterFlags flags = attr.Flags;

			var resolver = context.Resolver;

			// Mandatory
			if (property.IsDefined<MandatoryAttribute>())
				flags |= ParameterFlags.Mandatory;

			if (property.PropertyType == typeof(SwitchParam))
				flags |= ParameterFlags.IsSwitch;

			// Element type
			Type? elementType;
			if (property.PropertyType.IsArray)
			{
				elementType = property.PropertyType.GetElementType();
				flags |= ParameterFlags.IsList;
			}
			else
			{
				elementType = ReflectionHelper.GetListElementType(property.PropertyType);
				if (elementType is not null)
					flags |= ParameterFlags.IsList;
				else
					elementType = property.PropertyType;
			}
			this.ElementType = elementType;


			// Aliases
			{
				AliasAttribute? aliasAttr = property.GetCustomAttribute<AliasAttribute>(true);
				this.Aliases = aliasAttr?.Aliases ?? Array.Empty<string>();
			}
			// Description
			this.Description = property.Description;
			// Category
			// Don't use property.Category, since this returns "Misc" if no category is specified.
			this.Category = property.GetCustomAttribute<CategoryAttribute>()?.Category;
			// Environment
			this.EnvironmentVariable = attr.EnvironmentVariable;
			// Placeholder
			this.Placeholder = (0 == (flags & ParameterFlags.IsSwitch))
				? (property.GetCustomAttribute<PlaceholderAttribute>(true)?.Name ?? PlaceholderFromType(property.PropertyType))
				: null;

			// Default value
			{
				var atrDefault = property.GetCustomAttribute<DefaultValueAttribute>(true);
				object? defaultValue = null;
				if (atrDefault != null)
				{
					defaultValue = atrDefault.Value;
					this.RawDefaultValue = defaultValue;
					flags |= ParameterFlags.HasDefaultValue;
				}
			}

			// Value list
			{
				var atrValueList = property.GetCustomAttribute<ValueListProviderAttribute>(true);
				if (atrValueList == null)
					atrValueList = resolver.GetCustomAttribute<ValueListProviderAttribute>(elementType, true);

				if (atrValueList != null)
				{
					object? providerObj = null;
					try
					{
						Type providerType = atrValueList.ProviderType;
						providerType = resolver.GetRuntimeType(providerType);

						providerObj = Activator.CreateInstance(providerType, true);

						var provider = providerObj as ValueListProvider;
						if (provider == null)
							throw new MetadataException($"The value list provider for parameter '{property.Name}' is not a {nameof(ValueListProvider)}", property.Name);

						this._valueListProvider = provider;
					}
					catch (Exception ex)
					{
						// UNDONE: Silently fail
						//throw new MetadataException($"An error occurred while creating the value list provider for parameter '{property.Name}': {ex.Message}", property.Name, ex);
					}
				}
				else if (property.IsDefined<EnumNameListAttribute>())
				{
					var enumNameAttrs = property.GetCustomAttribute<EnumNameListAttribute>(true).EnumTypes;
					this._valueListProvider = new EnumNameListProvider(enumNameAttrs);
				}
				else if (this.ElementType.IsEnum)
				{
					this._valueListProvider = Singleton.SingleInstance<EnumListProvider>();
				}
			}

			this.Flags = flags;
		}

		/// <summary>
		/// Gets the command that declares this parameter.
		/// </summary>
		public CommandMetadata DeclaringCommand { get; }

		internal static string PlaceholderFromType(Type type)
		{
			var elemType = type.GetListElementType();
			if (elemType != null)
				return PlaceholderFromType(elemType) + "[]";
			else
			{
				// UNDONE: This doesn't work with Reflection-only
				//var nullableUnderlying = Nullable.GetUnderlyingType(type);

				if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.Nullable`1")
				{
					type = type.GetGenericArguments()[0];
				}

				return type.Name;
			}
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.Property.Name} : {this.Property.PropertyType.Name}";

		/// <summary>
		/// Gets the property implementing the parameter.
		/// </summary>
		public PropertyDescriptor Property { get; }
		/// <summary>
		/// Gets the group containing this parameter.
		/// </summary>
		public ParameterGroupInfo? Group { get; }

		/// <summary>
		/// Gets the element type of the parameter.
		/// </summary>
		/// <remarks>
		/// If the parameter is a list, this is the type of element in the list.
		/// If the parameter is not a list, this is the same as <see cref="ParameterType"/>.
		/// </remarks>
		public Type ElementType { get; }
		/// <summary>
		/// Gets a <see cref="ParameterFlags"/> value describing aspects of the parameter.
		/// </summary>
		public ParameterFlags Flags { get; }
		/// <summary>
		/// Gets a value indicating whether a parameter is positional.
		/// </summary>
		public bool IsPositional => this.Position != ParameterAttribute.NoPosition;
		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		public string Name => this.Property.Name;
		/// <summary>
		/// Gets a list of aliases for this parameter.
		/// </summary>
		public string[] Aliases { get; }
		/// <summary>
		/// Gets the name of the placeholder for the value of this parameter.
		/// </summary>
		public string? Placeholder { get; }
		/// <summary>
		/// Gets the position of the parameter.
		/// </summary>
		public int Position { get; }
		/// <summary>
		/// Gets the type of the parameter.
		/// </summary>
		public Type ParameterType => this.Property.PropertyType;
		/// <summary>
		/// Gets a description of the parameter.
		/// </summary>
		public string? Description { get; }
		/// <summary>
		/// Gets the parameter category.
		/// </summary>
		public string? Category { get; }

		/// <summary>
		/// Gets the name of the environment variable corresponding to this parameter.
		/// </summary>
		public string? EnvironmentVariable { get; }

		// Converter
		private TypeConverter GetConverter()
		{
			var paramConverter = Command.GetScalarParamConverter(this.ElementType, this.Property);
			return paramConverter;
		}

		/// <summary>
		/// Gets a value indicating whether the parameter has a default value.
		/// </summary>
		public bool HasDefaultValue => 0 != (this.Flags & ParameterFlags.HasDefaultValue);
		/// <summary>
		/// Gets the row default value for this parameter.
		/// </summary>
		public object? RawDefaultValue { get; set; }

		private object? _convertedDefaultValue;
		/// <summary>
		/// Gets the default value for this parameter.
		/// </summary>
		public object? DefaultValue => (this._convertedDefaultValue ??= this.ConvertDefaultValue());

		private object? ConvertDefaultValue()
		{
			if (this.HasDefaultValue)
			{
				var defaultValue = this.RawDefaultValue;
				if (defaultValue != null && !this.ElementType.IsAssignableFrom(defaultValue.GetType()))
				{
					var converter = this.GetConverter();
					if (converter != null)
						defaultValue = converter.ConvertFrom(null, null, defaultValue);
				}
				return defaultValue;
			}
			return null;
		}

		/// <summary>
		/// Gets a value indicating whether the parameter is mandatory.
		/// </summary>
		public bool IsMandatory => (0 != (this.Flags & ParameterFlags.Mandatory));
		/// <summary>
		/// Gets a value indicating whether the parameter is a switch.
		/// </summary>
		/// <remarks>
		/// A switch parameter doesn't consume an argument.
		/// </remarks>
		public bool IsSwitch => 0 != (this.Flags & ParameterFlags.IsSwitch);
		/// <summary>
		/// Ges a value indicating whether the parameter accepts a list of values.
		/// </summary>
		public bool IsList => 0 != (this.Flags & ParameterFlags.IsList);

		/// <summary>
		/// Gets a value indicating whether the parameter defines a list of values.
		/// </summary>
		public bool HasValueList => this._valueListProvider != null;
		private ValueListProvider? _valueListProvider;
		private Array? _valueList;

		public Array? GetValueList(object? command, CommandMetadataContext context)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));
			if (this._valueList == null && this._valueListProvider != null)
			{
				this._valueList = this._valueListProvider.GetValueListFor(this, command, context);
			}

			return this._valueList;
		}

		/// <summary>
		/// Converts an argument into the type accepted by the parameter.
		/// </summary>
		/// <param name="rawValue">Provided argument value</param>
		/// <param name="context">Context to supply to the type converter</param>
		/// <returns>A value accepted by the parameter.</returns>
		/// <remarks>
		/// Most commonly, <paramref name="rawValue"/> is a string entered by the user.
		/// If the parameter type is an array, this method converts <paramref name="rawValue"/>
		/// to the appropriate element type.
		/// if <paramref name="rawValue"/> is already the correct type, it is returned
		/// unchanged with no error.
		/// </remarks>
		public object? ConvertValue(object rawValue, ITypeDescriptorContext? context)
		{
			if (rawValue is null) throw new ArgumentNullException(nameof(rawValue));

			if (this.ElementType.IsAssignableFrom(rawValue?.GetType()))
				return rawValue;

			return this.GetConverter().ConvertFrom(context, null, rawValue);
		}

		private object GetGroupObject(Command command, object owner, ParameterGroupInfo? group) => group == null ? owner : group.GetGroupObject(command, owner);
		/// <summary>
		/// Sets the value of the parameter.
		/// </summary>
		/// <param name="command">Target command object</param>
		/// <param name="argValue">Argument value</param>
		/// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// <paramref name="argValue"/> must be of the correct type.
		/// Use <see cref="ConvertValue(object, ITypeDescriptorContext?)"/> to convert the values if necessary.
		/// </remarks>
		public void SetValue(Command command, object? argValue)
		{
			if (command is null) throw new ArgumentNullException(nameof(command));

			var group = this.GetGroupObject(command, command, this.Group);
			this.Property.SetValue(group, argValue);
		}
	}
}
