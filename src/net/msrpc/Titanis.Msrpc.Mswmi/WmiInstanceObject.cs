using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;
using Titanis.Reflection;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Represents an instance of a WMI class object.
	/// </summary>
	public sealed partial class WmiInstanceObject : WmiObject, IDynamicMetaObjectProvider
	{
		internal WmiInstanceObject(
			WmiClassObject wmiClass,
			WmiQualifier[]? qualifiers,
			WmiInstanceProperty[] properties
			)
		{
			Debug.Assert(properties != null && !properties.Contains(null));
			this.WmiClass = wmiClass;
			this.Qualifiers = qualifiers ?? Array.Empty<WmiQualifier>();
			this.Properties = properties;

#if DEBUG
			// Ensure the properties are specified in the correct order.
			var declOrder = -1;
			foreach (var prop in properties)
			{
				Debug.Assert(prop.ClassProperty.DeclarationOrder > declOrder);
				declOrder = prop.ClassProperty.DeclarationOrder;
			}
#endif

			this._hasPropQuals = properties.Any(r => r.Qualifiers.Length > 0);
		}

		/// <inheritdoc/>
		internal sealed override WmiObjectFlags ObjectFlags => WmiObjectFlags.CimInstance;

		private bool _hasPropQuals;

		/// <inheritdoc/>
		internal sealed override void EncodeObjectBlockTo(ByteWriter writer)
		{
			// Build NdTableValueTable
			var ndTable = new NdTable(this.Properties.Length);

			var cls = this.WmiClass;

			byte[] valueTable = new byte[cls.NdValueTableLength - ndTable.Bytes.Length];
			ByteWriter heapWriter = new ByteWriter();
			List<QualifierSet>? propQualsets =
				this._hasPropQuals ? new List<QualifierSet>(this.Properties.Length)
				: null;
			foreach (var prop in this.Properties)
			{
				ndTable.SetFlags(prop.ClassProperty.DeclarationOrder, prop.NdFlags);
				if (prop.NdFlags == NdFlags.None)
				{
					ndTable.SetFlags(prop.ClassProperty.DeclarationOrder, NdFlags.None);
					prop.ClassProperty.WriteValue(valueTable, prop.Value, heapWriter);
				}

				if (propQualsets != null)
					propQualsets.Add(QualifierSet.Encode(prop.Qualifiers, heapWriter));
			}

			InstanceType instanceType = new InstanceType
			{
				classPartBytes = cls.ClassPartBytes,
				classNameRef = heapWriter.WriteHeapString(cls.Name),
				ndTable = ndTable,
				instanceData = valueTable,
				instanceQualifierSet = QualifierSet.Encode(this.Qualifiers, heapWriter),
				qualsetFlag = (propQualsets != null) ? InstancePropQualsetFlag.HasProps : InstancePropQualsetFlag.NoProps,
				propQualsets = propQualsets?.ToArray() ?? Array.Empty<QualifierSet>(),
			};
			instanceType.instanceHeap = new Heap(heapWriter.GetData());

			writer.WritePduStruct(instanceType);
		}

		public WmiClassObject WmiClass { get; }
		[Browsable(false)]
		public sealed override WmiQualifier[] Qualifiers { get; }

		private WmiClassObject GetScriptingClass()
		{
			var wmiClass = this.WmiClass;
			if (wmiClass.Methods == null)
			{
				// The class returned with the object doesn't contain the method definitions,
				// so scripting with methods will fail.
				// Check the class cache for a previous request for the class that has methods.

				var cached = this.Scope?.TryGetCachedClass(wmiClass.Name);
				if (cached != null && cached.Methods != null)
					wmiClass = cached;
			}
			return wmiClass;
		}
		public sealed override WmiMethod? GetMethod(string methodName)
			=> this.GetScriptingClass().GetMethod(methodName);

		#region Properties
		public WmiInstanceProperty[] Properties { get; }
		private Dictionary<string, WmiInstanceProperty>? _propsByName;
		private Dictionary<string, WmiInstanceProperty> PropsByName => (this._propsByName ??= this.Properties.ToDictionary(r => r.ClassProperty.Name, StringComparer.OrdinalIgnoreCase));

		public object? this[string propertyName]
		{
			get
			{
				if (string.IsNullOrEmpty(propertyName))
					throw new ArgumentNullException(nameof(propertyName));

				var propsByName = this.PropsByName;
				if (propsByName.TryGetValue(propertyName, out var instProp))
				{
					return instProp.Value;
				}
				else
				{
					var classProp = this.WmiClass.GetProperty(propertyName);
					if (classProp == null)
						throw new ArgumentException($"The object does not contain a property '{propertyName}'.", nameof(propertyName));

					return classProp.DefaultValue;
				}
			}
			set
			{
				this.SetProperty(propertyName, value);
			}
		}

		public object? SetProperty(string propertyName, object? value)
		{
			if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));

			if (this.PropsByName.TryGetValue(propertyName, out var prop))
			{
				prop.Value = value;
				return value;
			}
			else
			{
				throw new ArgumentException($"The object does not have a property named '{propertyName}'.", nameof(propertyName));
			}
		}
		#endregion

		/// <inheritdoc/>
		public sealed override string RelativePath
		{
			get
			{
				var keyField = this.KeyProperty;
				if (keyField == null || keyField.Value == null)
					throw new InvalidOperationException("The object does not have a key property set.");

				// TODO: Escape quotes and such
				string value = keyField.Value?.ToString() ?? string.Empty;
				value = value.Replace(@"\", @"\\");
				value = value.Replace("\"", "\\\"");
				string path = $"{this.WmiClass.Name}=\"{value}\"";
				return path;
			}
		}
		/// <summary>
		/// Gets the key property.
		/// </summary>
		public WmiInstanceProperty? KeyProperty
		{
			get
			{
				foreach (var prop in this.Properties)
				{
					if (prop.ClassProperty.Qualifiers.Any(r => r is { Name: "key", Value: bool b and true }))
						return prop;
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the value of the key property, if set.
		/// </summary>
		public object? Key => this.KeyProperty?.Value;

		/// <inheritdoc/>
		public sealed override string ToMof()
		{
			// TODO: Convert the instance to MOF
			StringBuilder sb = new StringBuilder();
			return null;
			throw new NotImplementedException();
		}

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			var wmiClass = this.GetScriptingClass();
			return new WmiInstanceMeta(this, parameter, wmiClass);
		}
	}

	sealed class WmiInstanceMeta : WmiMetaObject
	{
		private WmiClassObject _klass;
		private WmiInstanceObject _obj;
		private readonly WmiClassObject _wmiClass;

		public WmiInstanceMeta(WmiInstanceObject obj, Expression parameter, WmiClassObject wmiClass)
			: base(parameter, BindingRestrictions.Empty, obj)
		{
			this._obj = obj;
			this._wmiClass = wmiClass;
		}

		protected sealed override WmiObject WmiObject => this._obj;

		private static readonly MethodInfo indexGetter = ReflectionHelper.MethodOf<WmiInstanceObject, object?>(r => r[null]);
		private static readonly MethodInfo indexSetter = ReflectionHelper.MethodOf<WmiInstanceObject?>(r => r.SetProperty(null, null));
		public sealed override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			return new DynamicMetaObject(
				Expression.Call(Expression.Convert(this.Expression, typeof(WmiInstanceObject)), indexGetter, new Expression[] { Expression.Constant(binder.Name) }),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(WmiInstanceObject))
				);
		}
		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			return new DynamicMetaObject(
				Expression.Call(Expression.Convert(this.Expression, typeof(WmiInstanceObject)), indexSetter, new Expression[] { Expression.Constant(binder.Name), value.Expression }),
				BindingRestrictions.GetTypeRestriction(this.Expression, typeof(WmiInstanceObject))
				);
		}
	}

	public class WmiInstanceProperty
	{
		private object? _value;

		internal WmiInstanceProperty(
			WmiProperty property,
			WmiQualifier[]? qualifiers,
			NdFlags ndFlags,
			object? value
			)
		{
			this.ClassProperty = property;
			this.Qualifiers = qualifiers ?? Array.Empty<WmiQualifier>();
			this.NdFlags = ndFlags;
			this.Value = value;
		}

		public sealed override string ToString()
			=> $"{this.ClassProperty.Name} = {this.Value}";

		public WmiProperty ClassProperty { get; }
		public WmiQualifier[] Qualifiers { get; }
		public NdFlags NdFlags { get; private set; }
		public object? Value
		{
			get => _value;
			internal set
			{
				if (value == null)
				{
					this.NdFlags = NdFlags.IsNull;
				}
				else
				{
					if (!this.ClassProperty.CheckValue(value))
						throw new ArgumentException($"Type mismatch assigning property '{this.ClassProperty.Name}' of class '{this.ClassProperty.ClassOfOrigin}'.", nameof(value));

					this.NdFlags = NdFlags.None;
				}
				_value = value;
			}
		}
	}
	partial class WmiInstanceObject : ICustomTypeDescriptor
	{
		AttributeCollection ICustomTypeDescriptor.GetAttributes() => AttributeCollection.Empty;
		string? ICustomTypeDescriptor.GetClassName() => this.WmiClass.Name;
		string? ICustomTypeDescriptor.GetComponentName() => this.Key?.ToString();
		TypeConverter? ICustomTypeDescriptor.GetConverter() => null;
		EventDescriptor? ICustomTypeDescriptor.GetDefaultEvent() => null;
		PropertyDescriptor? ICustomTypeDescriptor.GetDefaultProperty() => null;
		object? ICustomTypeDescriptor.GetEditor(Type editorBaseType) => null;
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => EventDescriptorCollection.Empty;
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[]? attributes) => EventDescriptorCollection.Empty;

		private PropertyDescriptorCollection? _props;
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return (this._props ??= this.BuildProps());
		}

		private PropertyDescriptorCollection BuildProps()
		{
			var wmiProps = Array.ConvertAll<WmiInstanceProperty, PropertyDescriptor>(this.Properties, r => new WmiPropertyDescriptor(r.ClassProperty));
			return new PropertyDescriptorCollection(wmiProps, true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[]? attributes)
		{
			// TODO: Apply property filters
			return (this._props ??= this.BuildProps());
		}

		object? ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor? pd)
		{
			return this;
		}

		class WmiPropertyDescriptor : PropertyDescriptor
		{
			private readonly WmiProperty property;

			internal WmiPropertyDescriptor(WmiProperty property)
				: base(property.Name, GetWmiAttributes(property))
			{
				this.property = property;
			}

			private static Attribute[]? GetWmiAttributes(WmiProperty property)
			{
				List<Attribute> attributes = new List<Attribute>();
				return null;
			}

			public override Type ComponentType => typeof(WmiInstanceObject);

			public override bool IsReadOnly => true;

			public override Type PropertyType => ToClrType(this.property.PropertyType);

			private Type ToClrType(CimType propertyType)
			{
				// TODO: Determine CLR type for CimType
				return typeof(object);
			}

			public override bool CanResetValue(object component) => false;

			public override object? GetValue(object? component) => ((WmiInstanceObject)component)[this.Name];

			public override void ResetValue(object component)
			{
				throw new NotSupportedException();
			}

			public override void SetValue(object? component, object? value)
			{
				throw new NotSupportedException();
			}

			public override bool ShouldSerializeValue(object component) => true;
		}
	}
}
