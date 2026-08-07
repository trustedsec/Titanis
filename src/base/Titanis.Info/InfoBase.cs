using Microsoft.Data.Sqlite;
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using Titanis.Info.Schema;
using Titanis.Winterop.Security;

namespace Titanis.Info
{
	public class InfoBase
	{
		private readonly string _fileName;

		private InfoBase(string fileName)
		{
			this._fileName = fileName;
		}

		private static readonly Table<SchemaInfo> tblSchema = new Table<SchemaInfo>();
		private static readonly Table<Partition> _tblPartition = new Table<Partition>();
		internal static readonly Table<ItemData> _tblItem = new Table<ItemData>();
		internal static readonly Table<ItemHistory> _tblItemHistory = new Table<ItemHistory>();
		private static readonly Table<ItemClass> _tblItemClass = new Table<ItemClass>();
		private static readonly Table<ItemProperty> _tblItemProp = new Table<ItemProperty>();
		private static readonly Table<SecDesc> _tblSecDesc = new Table<SecDesc>();
		private static readonly Table<Ace> _tblAce = new Table<Ace>();
		internal static readonly Table<CommandLog> _tblCommandLog = new Table<CommandLog>();
		internal static readonly Table<CommandArg> _tblCommandArg = new Table<CommandArg>();
		internal static readonly Table<ItemMultiValue> _tblItemMulti = new Table<ItemMultiValue>();
		private static readonly Table<LogRecord> tblLogRecord = new Table<LogRecord>();
		private static readonly Table<LogRecordParam> tblLogRecordValue = new Table<LogRecordParam>();
		private static readonly TableInserter<ItemClass> _classInserter = _tblItemClass.BuildInserter();
		private static readonly TableInserter<ItemProperty> _propInserter = _tblItemProp.BuildInserter();
		internal static readonly TableInserter<LogRecord> _logInserter = tblLogRecord.BuildInserter();
		internal static readonly TableInserter<LogRecordParam> _logValueInserter = tblLogRecordValue.BuildInserter();
		internal static readonly TableInserter<ItemData> _itemInserter = _tblItem.BuildInserter();
		internal static readonly TableInserter<ItemMultiValue> _itemMultiValueInserter = _tblItemMulti.BuildInserter();
		internal static readonly TableInserter<SecDesc> _secdescInserter = _tblSecDesc.BuildInserter();
		internal static readonly TableInserter<Ace> _aceInserter = _tblAce.BuildInserter();
		internal static readonly TableInserter<CommandArg> cmdargInserter = _tblCommandArg.BuildInserter();

		private HashSet<string> _allItemDataFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		private readonly SemaphoreSlim _classLock = new SemaphoreSlim(1);
		private Dictionary<int, ItemClassInfo> _itemClassesById = new Dictionary<int, ItemClassInfo>();
		private Dictionary<string, ItemClassInfo> _itemClassesByName = new Dictionary<string, ItemClassInfo>(StringComparer.OrdinalIgnoreCase);
		private Task<int> TryGetClassId(string name, CancellationToken cancellationToken) => this.GetOrCreateClass(name, false, cancellationToken);
		private Task<int> GetOrCreateClass(string name, CancellationToken cancellationToken) => this.GetOrCreateClass(name, true, cancellationToken);
		private async Task<int> GetOrCreateClass(string name, bool create, CancellationToken cancellationToken)
		{
			if (this._itemClassesByName.TryGetValue(name, out var classInfo))
			{
				return classInfo.ClassId;
			}
			else
			{
				await this._classLock.WaitAsync(cancellationToken);
				try
				{
					if (this._itemClassesByName.TryGetValue(name, out classInfo))
					{
						return classInfo.ClassId;
					}
					else
					{
						bool isNew = false;
						classInfo = await this.WithTransaction(async (txact, arg, cx) =>
						{
							var recs = await _tblItemClass.Select(txact, r => r.Name == name, default(SelectQueryInfo), cx);
							var itemClass = recs.FirstOrDefault();

							ItemClassInfo? classInfo;
							if (itemClass != null)
							{
								// Load properties
								var classId = itemClass.Id;
								var props = await _tblItemProp.Select(txact, r => r.ClassId == classId, default(SelectQueryInfo), cx);

								classInfo = new ItemClassInfo(this, itemClass);
								foreach (var prop in props)
								{
									classInfo.AddProperty(new ItemPropertyInfo(prop));
								}
							}
							else
							{
								if (create)
								{
									itemClass = new ItemClass
									{
										Name = name
									};
									isNew = true;
									itemClass.Id = (int)await _classInserter.Insert(itemClass, txact, cx);
									classInfo = new ItemClassInfo(this, itemClass);
								}
								else
									classInfo = null;
							}
							return classInfo;
						}, (object)null, cancellationToken);
						if (classInfo != null)
						{
							this._itemClassesByName.Add(name, classInfo);
							var classId = classInfo.ClassId;
							this._itemClassesById.Add(classId, classInfo);
							if (isNew)
								classId = ~classId;
							return classId;
						}
						else
						{
							return 0;
						}
					}
				}
				finally
				{
					this._classLock.Release();
				}
			}
		}

		internal async Task WithTransaction<TArg>(Func<SqliteTransaction, TArg, CancellationToken, Task> func, TArg arg, CancellationToken cancellationToken)
		{
			using (SqliteConnection conn = new SqliteConnection($"data source={this._fileName}"))
			{
				await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
				using (SqliteTransaction tx = conn.BeginTransaction())
				{
					await func(tx, arg, cancellationToken).ConfigureAwait(false);
					tx.Commit();
				}
			}
		}

		internal async Task<TResult> WithTransaction<TArg, TResult>(Func<SqliteTransaction, TArg, CancellationToken, Task<TResult>> func, TArg arg, CancellationToken cancellationToken)
		{
			using (SqliteConnection conn = new SqliteConnection($"data source={this._fileName}"))
			{
				await conn.OpenAsync(cancellationToken).ConfigureAwait(false);
				using (SqliteTransaction tx = conn.BeginTransaction())
				{
					var res = await func(tx, arg, cancellationToken).ConfigureAwait(false);
					tx.Commit();
					return res;
				}
			}
		}

		public static async Task<InfoBase> OpenAsync(string fileName, CancellationToken cancellationToken)
		{
			var ib = new InfoBase(fileName);

			var schemaInfo = await ib.WithTransaction(async (txact, arg, cx) =>
			{
				var schemaInfo = (await tblSchema.Select(
					txact,
					null,
					r => new SchemaInfo
					{
						SchemaVersion = r.SchemaVersion
					},
					default(SelectQueryInfo),
					cx)).FirstOrDefault();

				var allFields = await _tblItemProp.Select(txact, null, r => r.FieldName, default(SelectQueryInfo), cx);
				ib._allItemDataFields = new HashSet<string>(allFields);

				return schemaInfo;
			}, (object?)null, cancellationToken);

			if (schemaInfo?.SchemaVersion == CurrentSchemaVersion)
				return ib;
			else
				throw new InvalidDataException($"The file '{fileName}' is not a valid infobase.");
		}

		public static async Task<InfoBase> CreateAsync(string fileName, CancellationToken cancellationToken)
		{
			if (File.Exists(fileName))
				throw new ArgumentException($"Cannot create an infobase with file '{fileName}' because the file already exists.  Remove the file before creating a new one.", nameof(fileName));

			var ib = new InfoBase(fileName);
			try
			{
				await ib.WithTransaction<object>(async (txact, arg, cx) =>
				{
					Type[] tableTypes = [
						typeof(Partition),
						typeof(SchemaInfo),
						typeof(CommandLog),
						typeof(CommandArg),
						typeof(LogRecord),
						typeof(LogRecordParam),
						typeof(ItemData),
						typeof(ItemHistory),
						typeof(ItemClass),
						typeof(ItemProperty),
						typeof(ItemMultiValue),
						typeof(SecDesc),
						typeof(Ace),
						];
					foreach (var tableType in tableTypes)
					{
						var script = DataHelpers.GenerateCreateTableScript(tableType);
						await txact.ExecuteCommandText(script, cx);
					}

					await txact.ExecuteCommandText(@$"
CREATE INDEX IX_{nameof(Partition)}_{nameof(Partition.Name)} ON {nameof(Partition)}({nameof(Partition.Name)});
CREATE INDEX IX_{nameof(CommandLog)}_{nameof(CommandLog.CommandName)} ON {nameof(CommandLog)}({nameof(CommandLog.PartitionId)}, {nameof(CommandLog.CommandName)});
CREATE INDEX IX_{nameof(CommandArg)} ON {nameof(CommandArg)}({nameof(CommandArg.CommandId)}, {nameof(CommandArg.Seq)});
CREATE INDEX IX_{nameof(ItemClass)}_name ON {nameof(ItemClass)}({nameof(ItemClass.Name)});
CREATE INDEX IX_{nameof(SecDesc)}_hash ON {nameof(SecDesc)}({nameof(SecDesc.Md5Hash)});
", cx);

					await tblSchema.Insert(() => new SchemaInfo
					{
						SchemaVersion = CurrentSchemaVersion
					}, txact, cx);
				}, null, cancellationToken).ConfigureAwait(false);

				fileName = null;
				return ib;
			}
			finally
			{
				if (fileName != null)
					File.Delete(fileName);
			}
		}

		private const int CurrentSchemaVersion = 1;

		private static async Task<int> LookupPartition(SqliteTransaction txact, string name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(name))
				return 0;

			var value = (int)(await _tblPartition.Select(txact, r => r.Name == name, r => r.Id, default, cancellationToken)).FirstOrDefault();
			if (value == 0)
			{
				value = (int)await _tblPartition.Insert(() => new Partition
				{
					Name = name
				},
					txact,
					cancellationToken);
			}
			return value;
		}

		public async Task<InvocationLog> LogCommand(
			string name,
			string version,
			Dictionary<string, object?> args,
			string? partition,
			string? comment,
			Dictionary<string, string>? attributes,
			CancellationToken cancellationToken
			)
		{
			(var cmdId, var partitionId) = await this.WithTransaction(async (txact, _, cx) =>
			{
				var partitionId = await LookupPartition(txact, partition, cancellationToken);
				var startTime = DateTime.UtcNow;
				var commandId = (int)await _tblCommandLog.Insert(() => new CommandLog
				{
					CommandName = name,
					Version = version,
					LogComment = comment,
					RunningAsUser = Environment.UserName,
					RunningOnComputer = Environment.MachineName,
					CommandLine = Environment.CommandLine,
					PartitionId = partitionId,
					StartTime = startTime
				}, txact, cx);

				foreach (var arg in args)
				{
					if (arg.Value is IList list)
					{
						int seq = 0;
						foreach (var item in list)
						{
							seq++;
							await cmdargInserter.Insert(new CommandArg
							{
								CommandId = commandId,
								Name = arg.Key,
								Seq = seq,
								Value = item?.ToString() ?? string.Empty
							}, txact, cx);
						}
					}
					else
					{
						await cmdargInserter.Insert(new CommandArg
						{
							CommandId = commandId,
							Name = arg.Key,
							Seq = 0,
							Value = arg.Value?.ToString() ?? string.Empty
						}, txact, cx);
					}
				}

				return (commandId, partitionId);
			}, (object?)null, cancellationToken);

			return new InvocationLog(this, cmdId, partitionId);
		}

		public async Task<List<CommandHistory>> QueryCommandHistory(string? partition, CancellationToken cancellationToken)
		{
			var recs = await this.WithTransaction(async (txact, arg, cx) =>
			{
				if (partition is null)
				{
					var recs = await _tblCommandLog.Select(txact, null, default(SelectQueryInfo), cx);
					return recs;
				}
				else
				{
					var partId = (partition.Length == 0) ? 0 : await LookupPartition(txact, partition, cancellationToken);
					var recs = await _tblCommandLog.Select(txact, r => r.PartitionId == partId, default(SelectQueryInfo), cx);
					return recs;
				}
			}, (object?)null, cancellationToken);

			List<CommandHistory> history = new List<CommandHistory>(recs.Count);
			foreach (var rec in recs)
			{
				var args = await this.WithTransaction(async (txact, arg, cx) =>
				{
					var cmdId = arg.Id;
					var args = await _tblCommandArg.Select(txact, r => r.CommandId == cmdId, default(SelectQueryInfo), cx);
					return args;
				}, rec, cancellationToken);

				history.Add(new CommandHistory(rec, args, this));
			}

			return history;
		}




		private static readonly HashSet<string> builtinItemFields = [
			// Item
			nameof(ItemData.Id),
			nameof(ItemData.Version),
			nameof(ItemData.SourceCommandId),
			nameof(ItemData.ItemClassId),
			nameof(ItemData.PartitionId),
			nameof(ItemData.PropIdList),
			// ItemHistory
			nameof(ItemHistory.ItemId),
			nameof(ItemHistory.Seq),
			nameof(ItemHistory.ItemFlags),
			];

		private async Task<long> CacheSecDesc(SqliteTransaction txact, SecurityDescriptor sd, CancellationToken cancellationToken)
		{
			var sddl = sd.ToSddlString(SecurityDescriptorSections.All);
			var bytes = sd.ToByteArray(SecurityInfo.Attribute);

			var md5 = MD5.Create();
			var hash = md5.ComputeHash(bytes);
			var sdrecs = (await _tblSecDesc.Select(txact, r => r.Md5Hash == hash, default(SelectQueryInfo), cancellationToken));
			foreach (var sdrec in sdrecs)
			{
				if (sdrec.Sddl == sddl)
					return sdrec.Id;
			}

			var id = (int)await _secdescInserter.Insert(new SecDesc()
			{
				BinaryForm = bytes,
				Sddl = sddl,
				Md5Hash = hash,
				OwnerSid = sd.Owner?.ToString(),
				Group = sd.Group?.ToString(),
			}, txact, cancellationToken);
			await CacheAcl(txact, id, sd.Dacl?.Entries, SecurityDescriptorSections.Access, cancellationToken);
			await CacheAcl(txact, id, sd.Sacl?.Entries, SecurityDescriptorSections.Audit, cancellationToken);
			return id;
		}

		private async Task CacheAcl(SqliteTransaction txact, int sdid, List<AccessControlEntry>? aces, SecurityDescriptorSections section, CancellationToken cancellationToken)
		{
			if (aces != null)
			{
				int seq = 0;
				foreach (var ace in aces)
				{
					seq++;
					var objectAce = ace as IObjectAce;
					var callbackAce = ace as ICallbackAce;
					var labelAce = ace as MandatoryLabelAce;
					await _aceInserter.Insert(new Ace
					{
						SecDescId = sdid,
						Section = section,
						Seq = seq,
						Trustee = ace.Trustee.ToString(),
						Flags = ace.AceFlags,
						AceType = ace.AceType,
						AccessMask = ace.AccessMask,
						ObjectType = objectAce?.ObjectType,
						InheritedObjectType = objectAce?.InheritedObjectType,
						CallbackData = callbackAce?.ApplicationData,
						LabelPolicy = labelAce?.Policy
					}, txact, cancellationToken);
				}
			}
		}

		internal async Task<ItemSchema> GetItemFields(
			object instance,
			ICustomTypeDescriptor descr,
			PropertyDescriptorCollection props,
			Dictionary<int, IList> multiValues,
			CancellationToken cancellationToken)
		{
			var keyFields = new Dictionary<string, object>();

			var className = descr.GetClassName() ?? instance.GetType().FullName;
			Dictionary<string, object> extraFields = new Dictionary<string, object>(props.Count);
			var classId = await this.GetOrCreateClass(className, cancellationToken);
			bool isNewClass = classId < 0;
			if (isNewClass)
				classId = ~classId;
			SortedSet<int> propIds = new SortedSet<int>();

			var classInfo = this._itemClassesById[classId];
			await this.WithTransaction(async (txact, arg, cx) =>
			{
				var classProps = classInfo.GetProperties();
				var classPropsByName = classInfo.GetPropsByName();
				var createdFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (PropertyDescriptor prop in props)
				{
					Type propType = prop.PropertyType;
					ItemPropertyFlags propFlags = ItemPropertyFlags.None;
					bool isKey = prop.GetCustomAttribute<InfoKeyAttribute>() != null;
					if (isKey)
						propFlags |= ItemPropertyFlags.IsKey;

					Type elemType;
					if (typeof(IList).IsAssignableFrom(propType))
					{
						propFlags |= ItemPropertyFlags.Multi;
						var typedList = propType.FindInterfaces((t, _) => (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>)), null)?.FirstOrDefault();
						elemType = (typedList is not null) ? typedList.GenericTypeArguments[0] : typeof(object);
					}
					else
					{
						elemType = propType;
					}

					bool isSecDesc = (elemType == typeof(SecurityDescriptor));
					if (isSecDesc)
					{
						elemType = typeof(long);
					}

					string fieldName;
					int propId;
					{
						if (!classPropsByName.TryGetValue(prop.Name, out var field))
						{
							if (0 == (propFlags & ItemPropertyFlags.Multi))
							{
								fieldName = prop.Name;
								if (builtinItemFields.Contains(fieldName))
									fieldName += "_";

								if (!this._allItemDataFields.Contains(fieldName) && createdFields.Add(fieldName))
								{
									await txact.ExecuteCommandText($"ALTER TABLE [{_tblItem.Name}] ADD COLUMN [{fieldName}] {DataHelpers.GetColTypeKeyword(propType)}", cx);
									await txact.ExecuteCommandText($"ALTER TABLE [{_tblItemHistory.Name}] ADD COLUMN [{fieldName}] {DataHelpers.GetColTypeKeyword(propType)}", cx);
								}
							}
							else
							{
								fieldName = prop.Name + "*";
							}

							TypeCode tc = (isSecDesc ? (TypeCode)DataTypeCode.SecDesc
							: Type.GetTypeCode((elemType.GetCustomAttribute<InfoValueAttribute>()?.DataType ?? elemType)));
							ItemProperty itemProp = new()
							{
								ClassId = classId,
								Name = prop.Name,
								FieldName = fieldName,
								ClrTypeCode = tc,
								Flags = propFlags
							};
							itemProp.Id = propId = (int)await _propInserter.Insert(itemProp, txact, cx);

							classInfo.AddProperty(new ItemPropertyInfo(itemProp));

						}
						else
						{
							fieldName = field.FieldName;
							propId = field.Id;
						}
					}

					if (0 == (propFlags & ItemPropertyFlags.Multi))
					{
						object propValue = prop.GetValue(instance);
						if (isKey)
							keyFields.Add(fieldName, propValue);
						if (isSecDesc && propValue is SecurityDescriptor sd)
						{
							propValue = await this.CacheSecDesc(txact, sd, cancellationToken);
						}

						propIds.Add(propId);
						extraFields.Add(fieldName, propValue);
					}
					else
					{
						var list = prop.GetValue(instance) as IList;
						if (list != null)
						{
							propIds.Add(propId);
							multiValues[propId] = list;
						}
					}
				}

				if (isNewClass && keyFields.Count > 0)
				{
					// Create the key index
					await txact.ExecuteCommandText($"CREATE INDEX [IX_{nameof(ItemData)}_classKey_{className}] ON {nameof(ItemData)}({nameof(ItemData.PartitionId)}, {nameof(ItemData.ItemClassId)}, {string.Join(",", keyFields.Keys)});", cx);
				}

				foreach (var newField in createdFields)
				{
					this._allItemDataFields.Add(newField);
				}

				return classId;
			}, (object?)null, cancellationToken);

			return new ItemSchema
			{
				classId = classId,
				isNewClass = isNewClass,
				extraFields = extraFields,
				keyFields = keyFields,
				propIds = propIds,
			};
		}

		public async Task<IList<Item>> GetItems(
			string className,
			int commandId,
			CancellationToken cancellationToken)
		{
			int classId;
			if (className is null)
			{
				classId = 0;
			}
			else
			{
				classId = await this.TryGetClassId(className, cancellationToken);
				if (classId == 0)
					return [];
			}

			var classInfo = this._itemClassesById[classId];

			var itemData = await this.WithTransaction(async (txact, arg, cx) =>
			{
				var props = classInfo.GetProperties().Where(r => !r.IsMultiValued).ToArray();

				Expression<Func<ItemData, bool>>? predicate =
					(classId > 0) ? (r => r.ItemClassId == classId)
					: null;

				var items = await _tblItem.Select(
					txact,
					predicate,
					r => new ItemInfo
					{
						ItemId = r.Id,
						Version = r.Version,
						ItemFlags = r.ItemFlags
					},
					new SelectQueryInfo()
					{
						extraFields = props.Select(r => r.GetExtraFieldInfo()).ToArray(),
						sortFields = [nameof(ItemData.Id)]
					},
					cx);

				foreach (var item in items)
				{
					var itemId = item.ItemId;
					var version = item.Version;
					if (0 != (item.ItemFlags & ItemFlags.HasMultiValues))
					{
						item.multiValues = await _tblItemMulti.Select(
							txact,
							r => r.ItemId == itemId && r.EndVersion > version,
							default,
							cx);
					}
				}

				return items;
			}, (object?)null, cancellationToken);

			var items = itemData.Select(r => new Item(this, r, classInfo)).ToArray();

			return items;
		}
	}

	struct ItemSchema
	{
		internal int classId;
		internal bool isNewClass;
		internal Dictionary<string, object> extraFields;
		internal Dictionary<string, object> keyFields;
		internal SortedSet<int> propIds;
	}

	enum DataTypeCode
	{
		SecDesc = -1,
	}
}
