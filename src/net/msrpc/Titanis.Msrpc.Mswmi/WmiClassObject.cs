using ms_dcom;
using ms_wmi;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;
using Titanis.Reflection;

namespace Titanis.Msrpc.Mswmi
{
	enum ClassWriteMode
	{
		ClassPart,
		ClassAndMethodsPart,
	}

	/// <summary>
	/// Represents a WMI class.
	/// </summary>
	/// <remarks>
	/// To retrieve a class, use <see cref="WmiScope.GetObjectAsync(string, System.Threading.CancellationToken)"/>
	/// with the name of the class.  To construct a class, use <see cref="WmiClassBuilder"/>.
	/// </remarks>
	public sealed class WmiClassObject : WmiObject, IDynamicMetaObjectProvider
	{
		internal WmiClassObject(
			byte[] classPartBytes,
			byte[]? methodPartBytes,
			string name,
			WmiClassObject? baseClass,
			string[] superclasses,
			WmiQualifier[] qualifiers,
			WmiProperty[] properties,
			WmiMethod[]? methods,
			int ndValueTableLength)
		{
			this._classPartBytes = classPartBytes;
			this._methodsPartBytes = methodPartBytes;
			this.Name = name;
			this.BaseClass = baseClass;
			this.Superclasses = superclasses;
			this.Qualifiers = qualifiers;
			this.Properties = properties;
			this.Methods = methods;
			this.NdValueTableLength = ndValueTableLength;
		}

		/// <inheritdoc/>
		public sealed override string ToString()
			=> $"class {this.Name}";

		public sealed override string RelativePath => this.Name;

		public readonly byte[] _classPartBytes;
		internal byte[] ClassPartBytes => this._classPartBytes;
		public readonly byte[]? _methodsPartBytes;

		public bool HasMethodPart => this._methodsPartBytes != null;

		/// <inheritdoc/>
		internal sealed override WmiObjectFlags ObjectFlags => WmiObjectFlags.CimClass;
		/// <inheritdoc/>
		internal sealed override void EncodeObjectBlockTo(ByteWriter writer)
		{
			this.BaseClass.WriteTo(writer, ClassWriteMode.ClassAndMethodsPart);
			this.WriteTo(writer, ClassWriteMode.ClassAndMethodsPart);
		}

		public string Name { get; }
		[Browsable(false)]
		public WmiClassObject? BaseClass { get; }
		public string? BaseClassName => this.BaseClass?.Name;
		[Browsable(false)]
		public string[] Superclasses { get; }
		[Browsable(false)]
		public sealed override WmiQualifier[] Qualifiers { get; }
		internal int NdValueTableLength { get; }
		internal int ValueTableLength => this.NdValueTableLength - NdTable.ComputeNdTableLength(this.Properties.Length);

		#region Methods
		[Browsable(false)]
		public WmiMethod[]? Methods { get; }
		private Dictionary<string, WmiMethod>? _methodsByName;
		public sealed override WmiMethod? GetMethod(string methodName)
		{
			if (string.IsNullOrEmpty(methodName)) throw new System.ArgumentException($"'{nameof(methodName)}' cannot be null or empty.", nameof(methodName));

			if (this.Methods != null)
			{
				var methods = (this._methodsByName ??= this.Methods?.ToDictionary(r => r.Name, StringComparer.OrdinalIgnoreCase));
				methods.TryGetValue(methodName, out var method);
				return method;
			}
			else
				return null;
		}
		#endregion
		#region Properties
		[Browsable(false)]
		public WmiProperty[] Properties { get; }
		private Dictionary<string, WmiProperty>? _propsByName;
		public WmiProperty? GetProperty(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName)) throw new System.ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));

			if (this.Properties != null)
			{
				var props = (this._propsByName ??= this.Properties?.ToDictionary(r => r.Name));
				props.TryGetValue(propertyName, out var prop);
				return prop;
			}
			else
				return null;
		}
		#endregion

		public WmiInstanceObject Instantiate(Dictionary<string, object>? propertyValues = null)
		{
			if (propertyValues != null)
			{
				// Check values
				foreach (var propValueEntry in propertyValues)
				{
					var prop = this.GetProperty(propValueEntry.Key);
					if (prop == null)
						throw new ArgumentException($"Class '{this.Name}' does not contain a property named '{propValueEntry.Key}'.", nameof(propertyValues));

					if (!prop.CheckValue(propValueEntry.Value))
						throw new ArgumentException($"The value specified for property '{prop.Name}' is not valid.", nameof(propertyValues));
				}
			}

			var props = this.Properties;
			WmiInstanceProperty[] instanceProps = new WmiInstanceProperty[props.Length];
			for (int i = 0; i < props.Length; i++)
			{
				var prop = props[i];

				NdFlags ndFlags = NdFlags.DefaultInherited | NdFlags.IsNull;
				object? propValue = null;
				if (propertyValues != null && propertyValues.TryGetValue(prop.Name, out propValue))
				{
					ndFlags =
						(propValue is null) ? NdFlags.IsNull
						: NdFlags.None;
				}

				//WmiQualifier[] propQuals = (prop.Qualifiers.Length > 0)
				//	? Array.FindAll(prop.Qualifiers, r => 0 != (r.Flavor & WmiQualifierFlavor.PropagateToInstance))
				//	: Array.Empty<WmiQualifier>()
				//	;
				WmiQualifier[] propQuals = Array.Empty<WmiQualifier>();
				var instanceProp = new WmiInstanceProperty(
					prop,
					propQuals,
					ndFlags,
					propValue);
				instanceProps[i] = instanceProp;
			}

			// Build NdTableValueTable
			var ndTable = new NdTable(this.Properties.Length);

			byte[] valueTable = new byte[this.NdValueTableLength - ndTable.Bytes.Length];
			if (propertyValues != null)
			{
				foreach (var propValueEntry in propertyValues)
				{
					var prop = this.GetProperty(propValueEntry.Key)!;
					var ndFlags =
						(propValueEntry.Value is null) ? NdFlags.IsNull
						: NdFlags.None;
					ndTable.SetFlags(prop.DeclarationOrder, ndFlags);
				}
			}

			var qualifiers = Array.FindAll(this.Qualifiers, r => 0 != (r.Flavor & WmiQualifierFlavor.PropagateToInstance));

			return new WmiInstanceObject(
				this,
				qualifiers,
				instanceProps);
		}

		internal void WriteTo(ByteWriter writer, ClassWriteMode mode)
		{
			switch (mode)
			{
				case ClassWriteMode.ClassPart:
					writer.WriteBytes(this._classPartBytes);
					break;
				case ClassWriteMode.ClassAndMethodsPart:
					if (this._methodsPartBytes == null)
						throw new InvalidOperationException("This WmiClassObject instance does not contain a MethodsPart");
					writer.WriteBytes(this._classPartBytes);
					writer.WriteBytes(this._methodsPartBytes);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(mode));
			}
		}

		public sealed override string ToMof()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var qual in this.Qualifiers)
			{
				qual.ToMof(sb);
				sb.AppendLine();
			}

			sb.Append("class ").Append(this.Name ?? "<anonymous>");
			string? baseClassName = this.BaseClass?.Name;
			if (baseClassName != null)
				sb.Append(" : ").Append(baseClassName);

			sb.AppendLine(" {");

			foreach (var prop in this.Properties)
			{
				prop.ToMof(sb, "\t");
				sb.AppendLine();
			}

			sb.AppendLine("}");

			return sb.ToString();
		}

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new WmiClassMeta(this, parameter);
		}
	}

	abstract class WmiMetaObject : DynamicMetaObject
	{
		public WmiMetaObject(Expression expression, BindingRestrictions restrictions) : base(expression, restrictions)
		{
		}

		public WmiMetaObject(Expression expression, BindingRestrictions restrictions, object value) : base(expression, restrictions, value)
		{
		}

		protected abstract WmiObject WmiObject { get; }

		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			var wmiMethod = this.WmiObject.GetMethod(binder.Name);
			if (wmiMethod != null)
			{
				ElementInit[] elems = new ElementInit[args.Length];

				var inputClass = wmiMethod.InputSignature;
				var inputProps = inputClass?.Properties ?? Array.Empty<WmiProperty>();
				CancellationToken cancellationToken = CancellationToken.None;

				bool failed = false;
				for (int i = 0; i < args.Length; i++)
				{
					var arg = args[i];
					if (arg.Value is CancellationToken t)
					{
						cancellationToken = t;
						continue;
					}


					var argName = (i < binder.CallInfo.ArgumentNames.Count) ? binder.CallInfo.ArgumentNames[i] : null;
					if (argName == null)
					{
						if (i < inputProps.Length)
							argName = inputProps[i].Name;
						else
						{
							failed = true;
							break;
						}
					}

					// TODO: Check for duplicate arg names

					elems[i] = Expression.ElementInit(addMethod, Expression.Constant(argName), Expression.Convert(arg.Expression, typeof(object)));
				}

				if (!failed)
				{
					return new DynamicMetaObject(Expression.Call(
						Expression.Convert(this.Expression, typeof(WmiObject)),
						invokeMethod,
						Expression.Constant(wmiMethod.Name),
						Expression.ListInit(
							Expression.New(ctor, new Expression[] { Expression.Constant(args.Length) }),
							elems
							),
						Expression.Constant(cancellationToken)
						), BindingRestrictions.GetInstanceRestriction(this.Expression, this.WmiObject));
				}
			}

			return binder.FallbackInvoke(this, args, null);
		}

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		private static readonly MethodInfo invokeMethod = ReflectionHelper.MethodOf<WmiObject, string, Dictionary<string, object?>, CancellationToken>((o, a1, a2, a3) => o.InvokeMethodAsync(a1, a2, a3));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		private static readonly ConstructorInfo ctor = ReflectionHelper.ConstructorOf<int>(r => new Dictionary<string, object?>(r));
		private static readonly MethodInfo addMethod = ReflectionHelper.MethodOf<Dictionary<string, object?>, string, object?>((o, k, v) => o.Add(k, v));
	}

	sealed class WmiClassMeta : WmiMetaObject
	{
		private WmiClassObject _klass;

		public WmiClassMeta(WmiClassObject obj, Expression parameter)
			: base(parameter, BindingRestrictions.Empty, obj)
		{
			this._klass = obj;
		}

		protected sealed override WmiObject WmiObject => this._klass;
	}
}