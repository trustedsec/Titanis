using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Describes an output field in a table or list.
	/// </summary>
	public abstract class OutputField
	{
		private protected OutputField()
		{
		}

		internal IOutputFormatter? formatter;

		public static OutputField<TRecord, TValue> Create<TRecord, TValue>(
			string name,
			string caption,
			Func<TRecord, TValue> projection,
			string? format = null,
			DisplayAlignment alignment = DisplayAlignment.Left
			)
			=> new OutputField<TRecord, TValue>(name, caption, projection, format, alignment);
		public static OutputField CreateForProperty(Type recordType, string propertyName, CommandMetadataContext mdContext)
		{
			if (recordType is null) throw new ArgumentNullException(nameof(recordType));
			if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));
			if (mdContext is null) throw new ArgumentNullException(nameof(mdContext));
			var prop = TypeDescriptor.GetProperties(recordType)[propertyName];
			if (prop is null)
				throw new ArgumentException($"Type '{recordType.FullName}' does not have a property '{propertyName}'");

			return new PropertyOutputField(prop, mdContext);
		}

		public abstract string Name { get; }
		public abstract string Caption { get; }
		public abstract string? FormatString { get; }
		public abstract DisplayAlignment Alignment { get; }
		public string? FormatStringOverride { get; set; }
		public virtual bool IsFileSize => false;
		public string? EffectiveFormatString => this.FormatStringOverride ?? this.FormatString;

		public abstract object? GetValue(object record);
		public string? FormatValue(object? value, OutputStyle outputStyle)
		{
			var fmt = this.EffectiveFormatString;
			return
				(fmt is not null) ? (
					(this.formatter is not null) ? this.formatter.FormatValue(value, fmt, this, outputStyle)
					: (value is IFormattable f) ? (fmt.Contains("{0") ? string.Format(fmt, f) : f.ToString(fmt, null))
					: value?.ToString()
				) : value?.ToString();
		}

		class EnumValueProperty : PropertyDescriptor
		{
			private readonly Type enumType;

			internal EnumValueProperty(Type enumType)
				: base("Value", Array.Empty<Attribute>())
			{
				this.enumType = enumType;
			}

			public override Type ComponentType => this.enumType;
			public override bool IsReadOnly => true;
			public override Type PropertyType => this.enumType;
			public override bool CanResetValue(object component) => false;
			public override object GetValue(object component) => component;
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			public override bool ShouldSerializeValue(object component) => false;
		}

		public static OutputField[] GetFieldsFor(Type recordType, CommandMetadataContext context, string[]? fieldNames = null, bool includeDummyFields = false)
		{
			if (recordType is null) throw new ArgumentNullException(nameof(recordType));


			PropertyDescriptorCollection props;
			if (recordType.IsEnum)
			{
				props = new PropertyDescriptorCollection(new PropertyDescriptor[] { new EnumValueProperty(recordType) });
			}
			else
			{
				props = context.GetProperties(recordType);
			}

			return GetFieldList(context, fieldNames, props, includeDummyFields);
		}

		private static OutputField[] GetFieldList(CommandMetadataContext context, string[]? fieldNames, PropertyDescriptorCollection props, bool includeDummyFields)
		{
			List<OutputField> fields = new List<OutputField>(props.Count);
			if (fieldNames != null)
			{
				// If the descriptor has duplicate property names, handle gracefully
				var propsByName = props.OfType<PropertyDescriptor>().ToLookup(r => r.Name, StringComparer.OrdinalIgnoreCase);
				foreach (var name in fieldNames)
				{
					var prop = propsByName[name].FirstOrDefault();
					if (prop != null)
						fields.Add(new PropertyOutputField(prop, context));
					else if (includeDummyFields)
						fields.Add(new DummyOutputField(name, name, null, DisplayAlignment.Left));
				}
			}
			else
			{
				foreach (PropertyDescriptor prop in props)
				{
					if (fieldNames == null && prop.IsBrowsable)
						fields.Add(new PropertyOutputField(prop, context));
				}
				if (fields.Count == 0)
				{
					// The object has no properties; print the object itself.
					fields.Add(new SelfOutputField());
				}
			}

			return fields.ToArray();
		}

		public static OutputField[] GetFieldsFor(object record, string[]? fieldNames = null, bool includeDummyFields = false)
		{
			if (record is null) throw new ArgumentNullException(nameof(record));

			var props = TypeDescriptor.GetProperties(record);
			var context = new CommandMetadataContext(MetadataResolver.Default);

			return GetFieldList(context, fieldNames, props, includeDummyFields);
		}
	}

	internal sealed class PropertyOutputField : OutputField
	{
		internal PropertyOutputField(
			PropertyDescriptor property,
			CommandMetadataContext context
			)
		{
			if (property is null) throw new ArgumentNullException(nameof(property));

			this.Property = property;
			this.Caption = this.Name;

			var propertyType = property.PropertyType;
			{
				var attr = context.Resolver.GetCustomAttribute<DisplayAlignmentAttribute>(propertyType, true);
				this.Alignment = attr != null ? attr.Alignment : DisplayAlignmentAttribute.GetDefaultAlignmentFor(propertyType);
			}
			{
				var attr = context.Resolver.GetCustomAttribute<DisplayFormatStringAttribute>(propertyType, true);
				this.FormatString = attr != null ? attr.FormatString : DisplayFormatStringAttribute.GetDefaultFormatFor(propertyType);
			}

			foreach (Attribute attr in property.Attributes)
			{
				if (attr is DisplayNameAttribute disp)
					this.Caption = disp.DisplayName;
				else if (attr is DisplayFormatStringAttribute fmt)
					this.FormatString = fmt.FormatString;
				else if (attr is DisplayAlignmentAttribute align)
					this.Alignment = align.Alignment;
				else if (attr is FileSizeAttribute size)
					this.IsFileSize = true;
			}
		}

		public override string ToString()
			=> $"{this.Name} : {this.Property.PropertyType.Name}";

		public PropertyDescriptor Property { get; }

		public sealed override string Name => this.Property.Name;
		public sealed override string Caption { get; }
		public sealed override string? FormatString { get; }
		public sealed override DisplayAlignment Alignment { get; }
		public sealed override bool IsFileSize { get; }

		public sealed override object? GetValue(object record)
		{
			if (this.Property.ComponentType.IsInstanceOfType(record))
			{
				var value = this.Property.GetValue(record);
				return value;
			}
			else
			{
				return null;
			}
		}
	}

	/// <summary>
	/// Represents a field that may or may not be valid.
	/// </summary>
	/// <remarks>
	/// This class is mainly used for dynamic records.
	/// Rather than being bound to a specific property on a specific class,
	/// this implementation binds to the name on each record, and if
	/// no property is found, outputs nothing.
	/// </remarks>
	public sealed class DummyOutputField : OutputField
	{
		public DummyOutputField(
			string name,
			string caption,
			string? format,
			DisplayAlignment alignment = DisplayAlignment.Left
			)
		{
			this.Name = name;
			this.Caption = caption;
			this.FormatString = format;
			this.Alignment = alignment;
		}

		public sealed override string Name { get; }
		public sealed override string Caption { get; }
		public sealed override string? FormatString { get; }
		public sealed override DisplayAlignment Alignment { get; }

		private string? _properName;

		public sealed override object? GetValue(object rec)
		{
			if (rec is null)
				return null;

			var props = TypeDescriptor.GetProperties(rec);
			var prop = props[this.Name];
			if (prop is null)
				prop = props.Find(this.Name, true);

			if (prop is null)
				return null;
			else
			{
				object value = prop.GetValue(rec);
				return value;
			}
		}
	}

	/// <summary>
	/// Describes an output field in a table or list.
	/// </summary>
	/// <typeparam name="TRecord">Type of output record</typeparam>
	public abstract class OutputField<TRecord> : OutputField
	{
		public OutputField(
			string name,
			string caption,
			string? format,
			DisplayAlignment alignment = DisplayAlignment.Left
			)
		{
			this.Name = name;
			this.Caption = caption;
			this.FormatString = format;
			this.Alignment = alignment;
		}

		public sealed override string Name { get; }
		public sealed override string Caption { get; }
		public sealed override string? FormatString { get; }
		public sealed override DisplayAlignment Alignment { get; }

		public abstract object? GetValue(TRecord rec);
		public sealed override object? GetValue(object record)
		{
			if (record is TRecord rec)
				return this.GetValue(rec);
			else
				return null;
		}
	}

	public class SelfOutputField : OutputField
	{
		public override string Name => string.Empty;

		public override string Caption => string.Empty;

		public override string? FormatString => null;

		public override DisplayAlignment Alignment => DisplayAlignment.Left;

		public override object? GetValue(object record)
		{
			return record;
		}
	}

	public sealed class OutputField<TRecord, TValue> : OutputField<TRecord>
	{
		public OutputField(
			string name,
			string caption,
			Func<TRecord, TValue> selector,
			string? format = null,
			DisplayAlignment alignment = DisplayAlignment.Left)
			: base(name, caption, format, alignment)
		{
			this.Selector = selector;
		}

		public Func<TRecord, TValue> Selector { get; }

		public override object? GetValue(TRecord rec)
			=> this.Selector(rec);
	}
}
