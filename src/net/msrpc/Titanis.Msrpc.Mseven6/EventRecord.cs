using MS_EVEN6;
using System.ComponentModel;
using System.Text;
using System.Xml;
using Titanis.Msrpc.Mseven6.BinXml;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mseven6
{
	struct EventRecordInfo
	{
		internal string providerName;
		internal Guid providerGuid;
		internal ushort eventId;
		internal byte version;
		internal int level;
		internal ushort task;
		internal byte opcode;
		internal ulong keywords;
		internal DateTime timeCreated;
		internal long recordId;
		internal Guid? activityId;
		internal Guid? relatedActivityId;
		internal int processId;
		internal int threadId;
		internal string channel;
		internal string computer;
		internal SecurityIdentifier userSid;
		internal string name;
	}

	public struct EventRecordData
	{
		private readonly Node? valueNode;
		internal readonly EvtRpcVariant variant;

		internal EventRecordData(string? name, Node? valueNode, EvtRpcVariant variant)
		{
			this.Name = name;
			this.valueNode = valueNode;
			this.variant = variant;
		}

		public string? Name { get; }
		public object? Value => this.variant.GetValue();
	}

	public enum EventLevel : byte
	{
		Critical = 1,
		Error = 2,
		Warning = 3,
		Information = 4,
		Verbose = 5,
	}

	[Flags]
	public enum StandardEventKeywords : ulong
	{
		None = 0,
		ResponseTime = 0x1000000000000UL,
		WdiContext = 0x2000000000000UL,
		WdiDiagnostic = 0x4000000000000UL,
		Sqm = 0x8000000000000UL,
		AuditFailure = 0x10000000000000UL,
		// UNDONE: Same as AuditFailure, and deprecated
		//CorrelationHint = 0x,
		AuditSuccess = 0x20000000000000UL,
		// Actually CorrelationHint2
		CorrelationHint = 0x40000000000000UL,
		EventLogClassic = 0x80000000000000UL,
	}

	public partial class EventRecord
	{
		internal EventRecord(
			BinXml.BinXmlDocument doc,
			in EventRecordInfo info,
			EventRecordData[]? data
			)
		{
			this._doc = doc;
			this._info = info;
			this.Data = data ?? [];
		}

		private readonly BinXmlDocument _doc;
		private readonly EventRecordInfo _info;

		public string ProviderName => this._info.providerName;
		public Guid ProviderGuid => this._info.providerGuid;
		[DisplayFormatString("F0")]
		public ushort EventId => this._info.eventId;
		public string? Name => this._info.name;
		public byte Version => this._info.version;
		public EventLevel Level => (EventLevel)this._info.level;
		public ushort Task => this._info.task;
		public byte Opcode => this._info.opcode;
		public StandardEventKeywords Keywords => (StandardEventKeywords)this._info.keywords;
		public DateTime TimeCreated => this._info.timeCreated;
		public long RecordId => this._info.recordId;
		public Guid? ActivityId => this._info.activityId;
		public Guid? RelatedActivityId => this._info.relatedActivityId;
		public int ProcessId => this._info.processId;
		public int ThreadId => this._info.threadId;
		public string Channel => this._info.channel;
		public string Computer => this._info.computer;
		public SecurityIdentifier UserSid => this._info.userSid;
		internal EventRecordData[] Data { get; }

		public string? FormattedMessage { get; internal set; }

		private string? _xml;
		[Browsable(false)]
		public string? Xml => (this._xml ?? this.BuildXml());

		private string BuildXml()
		{
			StringBuilder output = new();
			XmlWriter writer = XmlWriter.Create(output);
			_doc.Accept(new BinXml.BinXmlWriterVisitor(writer));
			writer.Flush();

			return output.ToString();
		}

		internal object? GetNamedData(string name) => this.Data.FirstOrDefault(r => r.Name == name).Value;
	}
	public partial class EventRecord : ICustomTypeDescriptor
	{
		public AttributeCollection GetAttributes() => AttributeCollection.Empty;
		public string? GetClassName() => nameof(EventLogReader);
		public string? GetComponentName() => null;
		public TypeConverter? GetConverter() => null;
		public EventDescriptor? GetDefaultEvent() => null;
		public PropertyDescriptor? GetDefaultProperty() => null;
		public object? GetEditor(Type editorBaseType) => null;
		public EventDescriptorCollection GetEvents() => EventDescriptorCollection.Empty;
		public EventDescriptorCollection GetEvents(Attribute[]? attributes) => EventDescriptorCollection.Empty;


		private PropertyDescriptorCollection? _props;
		public PropertyDescriptorCollection GetProperties() => (this._props ??= this.BuildProps());

		public PropertyDescriptorCollection GetProperties(Attribute[]? attributes) => this.GetProperties();

		private PropertyDescriptorCollection? BuildProps()
		{
			var baseProps = TypeDescriptor.GetProperties(typeof(EventRecord));
			PropertyDescriptorCollection props = new PropertyDescriptorCollection(baseProps.OfType<PropertyDescriptor>().ToArray(), false);
			foreach (var datum in this.Data)
			{
				if (!string.IsNullOrEmpty(datum.Name))
				{
					var prop = props[datum.Name];
					if (prop is null)
						props.Add(new EventProperty(datum.Name, datum.Value?.GetType() ?? typeof(object)));
				}
			}

			return props;
		}


		public object? GetPropertyOwner(PropertyDescriptor? pd) => this;
	}
	class EventProperty : PropertyDescriptor
	{
		public EventProperty(string name, Type propertyType) : base(name, [])
		{
			this.PropertyType = propertyType;
		}

		public override Type ComponentType => typeof(EventRecord);

		public override bool IsReadOnly => true;

		public override Type PropertyType { get; }

		public override bool CanResetValue(object component) => false;
		public override object? GetValue(object? component) => ((EventRecord)component).GetNamedData(this.Name);

		public override void ResetValue(object component) => throw new NotImplementedException();

		public override void SetValue(object? component, object? value) => throw new NotImplementedException();

		public override bool ShouldSerializeValue(object component) => true;
	}
}
