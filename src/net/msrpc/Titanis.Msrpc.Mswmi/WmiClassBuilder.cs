using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Builds a WMI class.
	/// </summary>
	/// <remarks>
	/// Call <see cref="DefineProperty(string, CimType, object?, WmiQualifier[])"/> and
	/// <see cref="DefineMethod(string, CimType)"/> to define properties and methods, respectively.
	/// </remarks>
	public class WmiClassBuilder
	{
		/// <summary>
		/// Initializes a new <see cref="WmiClassBuilder"/>.
		/// </summary>
		/// <param name="name">Name of the new class</param>
		/// <param name="baseClass">Base class</param>
		/// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/> or empty.</exception>
		public WmiClassBuilder(
			string name,
			WmiClassObject? baseClass
			)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

			this.ClassName = name;
			this.BaseClass = baseClass;
			if (baseClass != null)
			{
				this._classIndex = baseClass.Superclasses.Length + 1;

				var props = new List<WmiProperty>(baseClass.Properties.Length);
				var propsByName = new Dictionary<string, WmiProperty>(baseClass.Properties.Length);
				int declOrder = 0;
				foreach (var prop in baseClass.Properties)
				{
					// Notably, mofcomp does not set the OriginPropagated flag
					WmiProperty prop2 = new(
						prop.Name,
						prop.PropertyTypeInheritedFlag | CimType.Inherited,
						prop.ClassOfOriginIndex + 1,
						prop.ClassOfOrigin,
						declOrder++,
						prop.Qualifiers,
						(uint)prop.DeclarationOrder,
						prop.DefaultValue,
						prop.NdFlags | NdFlags.DefaultInherited
						);
					props.Add(prop2);
					propsByName.Add(prop.Name, prop2);
				}
				this._nextPropDeclOrder = declOrder;
				this._props = props;
				this._propsByName = propsByName;

				this._valueTableWriter = new ByteWriter(baseClass.ValueTableLength);
				this._valueTableWriter.SetPosition(baseClass.ValueTableLength);
			}


		}

		public static WmiClassObject BuildEmptyClass()
		{
			ByteWriter writer = new ByteWriter();
			{
				ByteWriter heapWriter = new ByteWriter();
				ClassPart classPart = new ClassPart
				{
					header = new ClassHeader
					{
						classNameRef = HeapStringRef.Null,
						ndValueTableLength = 0
					},
					derivationList = new DerivationList
					{
						classNames = null
					},
					qualifierSet = new QualifierSet(),
					propertyLookup = new PropertyLookupTable(),
				};
				classPart.classHeap = new Heap(heapWriter.GetData());

				writer.WritePduStruct(classPart);
			}
			byte[] classPartBytes = writer.GetData().ToArray();

			writer = new ByteWriter();
			writer.WritePduStruct(EncodeMethodPart(Array.Empty<WmiMethodBuilder>()));
			byte[] methodPartBytes = writer.GetData().ToArray();

			var emptyBaseClass = new WmiClassObject(
				classPartBytes,
				methodPartBytes,
				string.Empty,
				null,
				Array.Empty<string>(),
				Array.Empty<WmiQualifier>(),
				Array.Empty<WmiProperty>(),
				Array.Empty<WmiMethod>(),
				0);

			return emptyBaseClass;
		}

		public string ClassName { get; set; }
		public WmiClassObject? BaseClass { get; }
		private ByteWriter _classHeapWriter = new ByteWriter();
		protected ByteWriter ClassHeapWriter => this._classHeapWriter;
		private ByteWriter? _valueTableWriter;

		#region Qualifiers
		private List<WmiQualifier>? _qualifiers;
		public List<WmiQualifier> Qualifiers => this._qualifiers ??= new List<WmiQualifier>();
		#endregion

		#region Properties
		private readonly int _classIndex;
		private List<WmiProperty>? _props;
		private Dictionary<string, WmiProperty>? _propsByName;
		public List<WmiProperty> Properties => this._props ??= new List<WmiProperty>();

		private int _nextPropDeclOrder;

		public void DefineProperty(
			string name,
			CimType propertyType,
			object? defaultValue,
			params WmiQualifier[]? qualifiers
			)
			=> this.DefineProperty(
				name,
				propertyType,
				CimSubtype.Unspecified,
				defaultValue,
				qualifiers);
		public void DefineProperty(
			string name,
			CimType propertyType,
			CimSubtype subtype,
			object? defaultValue,
			params WmiQualifier[]? qualifiers
			)
		{
			if (defaultValue != null)
			{
				if (!WmiProperty.CheckValue(propertyType, subtype, defaultValue))
					throw new ArgumentException("The default value does not match the property type.", nameof(defaultValue));
			}

			NdFlags ndFlags = (defaultValue == null) ? NdFlags.IsNull : NdFlags.None;
			if (this._propsByName is not null && this._propsByName.TryGetValue(name, out var prop))
			{
				Debug.Assert(this._valueTableWriter is not null);

				if (propertyType != prop.PropertyType)
					throw new ArgumentException($"Property '{name}' cannot be redefined with type '{propertyType}', as it is already defined with type '{prop.PropertyType}'.", nameof(propertyType));

				prop.DefaultValue = defaultValue;
				prop.NdFlags = ndFlags;
				if (!qualifiers.IsNullOrEmpty())
				{
					List<WmiQualifier> propquals = new List<WmiQualifier>(prop.Qualifiers);
					foreach (var qual in qualifiers)
					{
						var index = propquals.FindIndex(r => r.Name == qual.Name);
						if (index >= 0)
						{
							var existingQual = propquals[index];
							if (existingQual.IsInherited && existingQual.NotOverridable)
								throw new ArgumentException($"Qualifier '{qual.Name}' is declared NotOverridable in the base class.", nameof(qualifiers));

							propquals[index] = qual;
						}
						else
						{
							propquals.Add(qual);
						}
					}
				}
			}
			else
			{
				var valueTableWriter = (this._valueTableWriter ??= new ByteWriter());

				prop = new WmiProperty(
					name,
					propertyType,
					this._classIndex,
					this.ClassName,
					this._nextPropDeclOrder++,
					qualifiers ?? Array.Empty<WmiQualifier>(),
					(uint)valueTableWriter.Position,
					defaultValue,
					ndFlags
					);

				if (defaultValue == null)
				{
					// [MS-WMIO] § 2.2.74 - NoValue

					int cbSlot = EncodedValue.ValueTableSizeOf(propertyType);
					switch (cbSlot)
					{
						case 4:
							valueTableWriter.WriteUInt32LE(0xFFFF_FFFF);
							break;
						default:
							for (int i = 0; i < cbSlot; i++)
							{
								valueTableWriter.WriteByte(0xff);
							}
							break;
					}
				}
				else
					valueTableWriter.WriteEncodedValue(propertyType, subtype, defaultValue, this.ClassHeapWriter);

				(this._props ??= new List<WmiProperty>()).Add(prop);
				(this._propsByName ??= new Dictionary<string, WmiProperty>()).Add(prop.Name, prop);
			}
		}
		#endregion

		#region Methods
		private List<WmiMethodBuilder> _methods = new List<WmiMethodBuilder>();

		public WmiMethodBuilder DefineMethod(
			string name,
			CimType returnType)
		{
			var method = new WmiMethodBuilder(name, returnType)
			{
				classIndex = this._classIndex
			};
			this._methods.Add(method);
			return method;
		}
		#endregion

		public WmiClassObject BuildClass()
		{
			string name = this.ClassName;
			var baseClass = this.BaseClass ?? BuildEmptyClass();
			var qualifiers = this.Qualifiers?.ToArray() ?? Array.Empty<WmiQualifier>();

			if (baseClass == null)
				baseClass = BuildEmptyClass();

			ByteWriter writer = new ByteWriter();
			// HACK: mofcomp seems to pad a new class with 0x20 trailing bytes
			var classHeapWriter = this.ClassHeapWriter;
			var classPart = EncodeClassPart(baseClass, qualifiers, this.ClassHeapWriter, 0x0);
			writer.WritePduStruct(classPart);
			byte[] classPartBytes = writer.GetData().ToArray();

#if DEBUG
			{
				var classTest = new ByteMemoryReader(classPartBytes).ReadPduStruct<ClassPart>().CreateClass(null);
			}
#endif

			writer = new ByteWriter();
			MethodsPart methodsPart = EncodeMethodPart(this._methods);
			writer.WritePduStruct(methodsPart);
			byte[] methodsPartBytes = writer.GetData().ToArray();

			List<string> superclasses = new List<string>();
			if (!string.IsNullOrEmpty(baseClass?.Name))
			{
				superclasses.Add(baseClass.Name);
				superclasses.AddRange(baseClass.Superclasses);
			}

			//#if DEBUG
			//			var cls = classPart.CreateClassWithMethods(this.BaseClass, methodsPart);
			//#else
			var cls = new WmiClassObject(
				classPartBytes,
				methodsPartBytes,
				name,
				baseClass,
				superclasses.ToArray(),
				qualifiers,
				this.Properties.ToArray(),
				Array.Empty<WmiMethod>(),
				0);
			//#endif

			return cls;
		}

		private ClassPart EncodeClassPart(
			WmiClassObject baseClass,
			WmiQualifier[] qualifiers,
			ByteWriter? heapWriter,
			int trailingPadding
			)
		{
			byte[] valueTable = (this._valueTableWriter != null)
				? this._valueTableWriter.GetData().ToArray()
				: Array.Empty<byte>();

			ClassPart classPart = new ClassPart(
				new ClassHeader(heapWriter.WriteHeapString(this.ClassName))
				{
				})
			{
				derivationList = new DerivationList(
					string.IsNullOrEmpty(baseClass?.Name) ? Array.Empty<string>() : baseClass.Superclasses.PrependWith(baseClass.Name)
					),
				qualifierSet = QualifierSet.Encode(qualifiers, heapWriter),
				valueTable = _valueTableWriter?.GetData().ToArray() ?? Array.Empty<byte>()
			};

			if (this._props != null)
			{
				this.EncodeProps(heapWriter, out var propTable, out var ndTable);
				classPart.header.ndValueTableLength = (uint)(ndTable.ByteLength + valueTable.Length);
				classPart.propertyLookup = propTable;
				classPart.ndTable = ndTable;
			}

			classPart.classHeap = new Heap(heapWriter.GetData());
			return classPart;
		}

		private void EncodeProps(ByteWriter heapWriter, out PropertyLookupTable propTable, out NdTable ndTable)
		{
			this._props.Sort((x, y) => x.Name.CompareTo(y.Name));
			var propInfos = this._props.ConvertAll(r => new PropertyLookup(
				heapWriter.WriteHeapString(r.Name),
				heapWriter.WriteHeapStruct(r.Encode(heapWriter)
				)));
			propTable = new PropertyLookupTable(propInfos.ToArray());

			ndTable = new NdTable(this._props.Count);
			for (int i = 0; i < this._props.Count; i++)
			{
				var prop = this._props[i];
				ndTable.SetFlags(prop.DeclarationOrder, prop.NdFlags);
			}
		}

		internal static MethodsPart EncodeMethodPart(IList<WmiMethodBuilder> methodBuilders)
		{
			ByteWriter methodHeapWriter = new ByteWriter();

			MethodDescription[] methodDescs = new MethodDescription[methodBuilders.Count];
			for (int i = 0; i < methodBuilders.Count; i++)
			{
				WmiMethodBuilder? method = methodBuilders[i];
				methodDescs[i] = method.Build(methodHeapWriter);
			}

			// [MS-WMIO] § 2.2.38 - MethodsPart
			MethodsPart methodPart = new MethodsPart
			{
				methodCount = (ushort)methodBuilders.Count,
				methods = methodDescs,
			};
			methodPart.methodHeap = new Heap(methodHeapWriter.GetData());

			return methodPart;
		}
	}
}
