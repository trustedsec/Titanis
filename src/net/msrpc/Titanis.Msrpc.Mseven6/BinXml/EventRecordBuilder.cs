namespace Titanis.Msrpc.Mseven6.BinXml
{
	class EventRecordBuilder : BinXmlTreeVisitor
	{
		public EventRecordBuilder()
		{
		}

		enum PredefinedElement
		{
			Root = 0,
			Event,
			System,
			EventData,
			Provider,
			Correlation,
			Execution,
			Security,
			TimeCreated,
			Data,
			Custom,
		}
		struct EventRecordState
		{
			internal PredefinedElement elem;
			internal EventRecordInfo info;
			internal string? dataName;
			internal List<EventRecordData> data;
		}
		private EventRecordState _state;

		public EventRecord BuildRecord(BinXmlDocument doc)
		{
			ArgumentNullException.ThrowIfNull(doc);
			this._state = new EventRecordState();
			doc.Accept(this);
			return new EventRecord(doc, this._state.info, this._state.data?.ToArray());
		}
		public override void Visit(BinXmlDocument node)
		{
			node.Prolog?.Accept(this);
			node.Fragment.Accept(this);
			node.Misc?.Accept(this);
		}

		private Node? _nestedValue;
		protected override void VisitValueNode(Node data)
		{
			if (data is Substitution subst)
			{
				subst.Accept(this);
			}
			else
			{
				this._nestedValue = data;
				base.VisitValueNode(data);
			}
		}

		public override void Visit(Element node)
		{
			var prev = this._state.elem;
			var prevValue = this._nestedValue;
			try
			{
				this._nestedValue = null;

				this._state.elem = (prev, node.Name) switch
				{
					(PredefinedElement.Root, "Event") => PredefinedElement.Event,
					(PredefinedElement.Event, "System") => PredefinedElement.System,
					(PredefinedElement.Event, "EventData") => PredefinedElement.EventData,
					(PredefinedElement.System, "Provider") => PredefinedElement.Provider,
					(PredefinedElement.System, "TimeCreated") => PredefinedElement.TimeCreated,
					(PredefinedElement.System, "Correlation") => PredefinedElement.Correlation,
					(PredefinedElement.System, "Execution") => PredefinedElement.Execution,
					(PredefinedElement.Security, "Security") => PredefinedElement.Security,
					(PredefinedElement.EventData, "Data") => PredefinedElement.Data,
					_ => PredefinedElement.Custom,
				};
				if (this._state.elem == PredefinedElement.EventData)
				{
					this._state.data = new List<EventRecordData>();
				}

				base.Visit(node);

				if (this._state.elem is PredefinedElement.Data)
				{
					this._state.data.Add(new EventRecordData(this._state.dataName, this._nestedValue, (this._nestedValue as IValueNode)?.AsVariant() ?? default));
				}
				else if (this._nestedValue != null)
				{
					switch ((prev, node.Name, this._nestedValue))
					{
						case (PredefinedElement.System, "EventID", UnsignedIntegerValue value):
							this._state.info.eventId = (byte)value.Value;
							break;
						case (PredefinedElement.System, "Version", UnsignedIntegerValue value):
							this._state.info.version = (byte)value.Value;
							break;
						case (PredefinedElement.System, "Level", UnsignedIntegerValue value):
							this._state.info.level = (int)value.Value;
							break;
						case (PredefinedElement.System, "Task", UnsignedIntegerValue value):
							this._state.info.task = (ushort)value.Value;
							break;
						case (PredefinedElement.System, "Opcode", UnsignedIntegerValue value):
							this._state.info.opcode = (byte)value.Value;
							break;
						case (PredefinedElement.System, "Keywords", HexIntegerValue value):
							this._state.info.keywords = value.Value;
							break;
						case (PredefinedElement.System, "EventRecordID", UnsignedIntegerValue value):
							this._state.info.recordId = (long)value.Value;
							break;
						case (PredefinedElement.System, "Channel", TextValueNode value):
							this._state.info.channel = value.Value;
							break;
						case (PredefinedElement.System, "Computer", TextValueNode value):
							this._state.info.computer = value.Value;
							break;
						case (PredefinedElement.Event, "EventData", _):
							// Do nothing
							break;
						default:
							break;
					}
				}
			}
			finally
			{
				this._state.elem = prev;
				this._nestedValue = prevValue;
				this._state.dataName = null;
			}
		}

		public override void Visit(Attr node)
		{
			var prevValue = this._nestedValue;
			try
			{
				this._nestedValue = null;
				base.Visit(node);

				switch ((this._state.elem, node.Name, this._nestedValue))
				{
					case (PredefinedElement.Provider, "Name", TextValueNode value):
						this._state.info.providerName = value.Value;
						break;
					case (PredefinedElement.Provider, "Guid", GuidValue value):
						this._state.info.providerGuid = value.Value;
						break;
					case (PredefinedElement.TimeCreated, "SystemTime", TimeValue value):
						this._state.info.timeCreated = value.Value;
						break;
					case (PredefinedElement.Correlation, "ActivityID", GuidValue value):
						this._state.info.activityId = value.Value;
						break;
					case (PredefinedElement.Correlation, "RelatedActivityID", GuidValue value):
						this._state.info.relatedActivityId = value.Value;
						break;
					case (PredefinedElement.Execution, "ProcessID", IntegerValue value):
						this._state.info.processId = (int)value.Value;
						break;
					case (PredefinedElement.Execution, "ThreadID", IntegerValue value):
						this._state.info.threadId = (int)value.Value;
						break;
					case (PredefinedElement.Security, "UserID", SidValue value):
						this._state.info.userSid = value.Sid;
						break;
					case (PredefinedElement.EventData, "Name", TextValueNode value):
						this._state.info.name = value.Value;
						break;
					case (PredefinedElement.Data, "Name", TextValueNode value):
						this._state.dataName = value.Value;
						break;
					case (PredefinedElement.Event, "xmlns", _):
						// Ignore
						break;
					default:
						break;
				}
			}
			finally
			{
				this._nestedValue = prevValue;
			}
		}

		private TemplateInstance? _templ;
		public override void Visit(TemplateInstance node)
		{
			var prev = this._templ;
			try
			{
				this._templ = node;
				node.Element.Accept(this);
			}
			finally
			{
				this._templ = prev;
			}
		}

		public override void Visit(Substitution node)
		{
			this.VisitValueNode(this._templ.Data[node.Id].Value);
		}
	}
}
