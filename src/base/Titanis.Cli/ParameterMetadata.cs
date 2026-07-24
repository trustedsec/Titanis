using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
		AffectsOutput = 0x10,
		/// <summary>
		/// The parameter provides output only and does not accept input.
		/// </summary>
		OutputOnly = 0x20,
		/// <summary>
		/// Only show this parameter in advanced help
		/// </summary>
		IsAdvanced = 0x40,
	}

	/// <summary>
	/// Specifies behavior for a parameter group.
	/// </summary>
	[Flags]
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
			this.IsPositional = (this.Position != ParameterAttribute.NoPosition || !string.IsNullOrEmpty(attr.After));
			this.Group = group;
			this.DeclaringCommand = command;

			ParameterFlags flags = attr.Flags;

			var resolver = context.Resolver;

			// Mandatory
			if (property.IsDefined<MandatoryAttribute>())
				flags |= ParameterFlags.Mandatory;

			if (property.PropertyType == typeof(SwitchParam))
				flags |= ParameterFlags.IsSwitch;

			if (property.IsReadOnly)
				flags |= ParameterFlags.OutputOnly;

			if (property.IsDefined<AdvancedAttribute>())
				flags |= ParameterFlags.IsAdvanced;

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
				{
					elementType = property.PropertyType;
					var nullable = Nullable.GetUnderlyingType(elementType);
					if (nullable is not null)
						elementType = nullable;
					// The above check fails when run from MSBuild in the .NET Framework
					else if (elementType.IsGenericType && elementType.GetGenericTypeDefinition().FullName == "System.Nullable`1")
						elementType = elementType.GenericTypeArguments[0];
				}
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
			// DisplayName
			this.DisplayName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? MakeDisplayName(property.Name);
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

			// File type info
			{
				var atrFileSpec = property.GetCustomAttribute<FileSpecAttribute>(true);
				this.IsFileSpec = (atrFileSpec != null);
				this._atrFileSpec = atrFileSpec;
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
					catch (Exception)
					{
						// UNDONE: Silently fail
						//throw new MetadataException($"An error occurred while creating the value list provider for parameter '{property.Name}': {ex.Message}", property.Name, ex);
					}
				}
				else if (property.GetCustomAttribute<EnumNameListAttribute>() is EnumNameListAttribute enumNameListAttr)
				{
					var enumNameAttrs = enumNameListAttr.EnumTypes;
					this._valueListProvider = new EnumNameListProvider(enumNameAttrs);
				}
				else if (this.ElementType.IsEnum)
				{
					this._valueListProvider = Singleton.SingleInstance<EnumListProvider>();
				}
			}

			this.Flags = flags;
		}

		private string? MakeDisplayName(string name)
		{
			if (string.IsNullOrEmpty(name))
				return name;

			StringBuilder sb = new StringBuilder(name.Length);
			char p = name[0];
			bool pendingCaps = true;
			for (int i = 1; i <= name.Length; i++)
			{
				char c = (i < name.Length) ? name[i] : '\0';

				if (char.IsLower(p) && char.IsUpper(c))
				{
					sb.Append(p).Append(' ');
				}
				else
				{
					if (!pendingCaps && !char.IsUpper(c))
						p = char.ToLower(p);
					pendingCaps = char.IsUpper(c) && char.IsUpper(p);
					sb.Append(p);
				}

				p = c;
			}

			return sb.ToString();
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
		public bool IsOutputOnly => 0 != (this.Flags & ParameterFlags.OutputOnly);
		public bool AffectsOutput => 0 != (this.Flags & ParameterFlags.AffectsOutput);
		/// <summary>
		/// Gets a value indicating whether a parameter is positional.
		/// </summary>
		public bool IsPositional { get; }
		/// <summary>
		/// Gets the name of the parameter.
		/// </summary>
		public string Name => this.Property.Name;
		public string? DisplayName { get; }

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
		public TypeConverter GetConverter()
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
						defaultValue = converter.ConvertFrom(new ParameterConverterContext(null, this, ParameterConverterContextOptions.ForDefault), null, defaultValue);
				}
				return defaultValue;
			}
			return null;
		}

		/// <summary>
		/// Gets a value indicating whether the parameter is mandatory.
		/// </summary>
		public bool IsMandatory => (0 != (this.Flags & ParameterFlags.Mandatory));
		public bool IsAdvanced => (0 != (this.Flags & ParameterFlags.IsAdvanced));
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

		private readonly FileSpecAttribute? _atrFileSpec;
		public bool IsFileSpec { get; }
		public bool FileMustExist => this._atrFileSpec?.MustExist ?? false;
		public ImmutableArray<FileTypeInfo> FileTypes => this._atrFileSpec?.FileTypes ?? [];

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

		private object GetGroupObject(object instance, object owner, ParameterGroupInfo? group) => group == null ? owner : group.GetGroupObject(instance, owner);
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
		public void SetValue(object command, object? argValue)
		{
			if (command is null) throw new ArgumentNullException(nameof(command));

			var group = this.GetGroupObject(command, command, this.Group);
			this.Property.SetValue(group, argValue);
		}
	}
}
