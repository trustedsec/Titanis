using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using Titanis.Info.Schema;

namespace Titanis.Info
{
	public partial class InvocationLog
	{
		private readonly int id;
		private readonly int partitionId;

		internal InvocationLog(InfoBase owner, int id, int partitionId)
		{
			Owner = owner;
			this.id = id;
			this.partitionId = partitionId;
		}

		public InfoBase Owner { get; }

		public Task LogError(Exception ex, CancellationToken cancellationToken)
		{
			var details = ex.ToString();
			int exitCode = GetErrorCode(ex);
			var endTime = DateTime.UtcNow;
			return this.Owner.WithTransaction((txact, arg, cx) =>
				arg.Update(txact, r => r.Id == this.id, r => new CommandLog
				{
					EndTime = endTime,
					ErrorDetails = details,
					ExitCode = exitCode
				}, null, cx), InfoBase._tblCommandLog, cancellationToken);
		}

		public Task LogReturnValue(int retval, CancellationToken cancellationToken)
		{
			var endTime = DateTime.UtcNow;
			return this.Owner.WithTransaction((txact, arg, cx) =>
				arg.Update(txact, r => r.Id == this.id, r => new CommandLog
				{
					EndTime = endTime,
					ExitCode = retval
				}, null, cx), InfoBase._tblCommandLog, cancellationToken);
		}

		private int GetErrorCode(Exception ex)
		{
			while (ex is AggregateException agg)
			{
				ex = agg.InnerException;
			}

			if (ex is IHaveErrorCode hasErrorCode)
				return hasErrorCode.ErrorCode;
			return ex.HResult;
		}

		private long _itemSeq;
		public async Task WriteItem(object? item, CancellationToken cancellationToken)
		{
			if (item is null)
				return;

			var descr = (item as ICustomTypeDescriptor) ?? TypeDescriptor.GetProvider(item).GetTypeDescriptor(item);
			var props = descr.GetProperties();
			var className = descr.GetClassName() ?? (item.GetType().FullName);
			var seq = (int)Interlocked.Increment(ref this._itemSeq);

			var multiValues = new Dictionary<int, IList>();
			var itemSchema = await this.Owner.GetItemFields(item, descr, props, multiValues, cancellationToken);
			var extFields = itemSchema.extraFields;
			var keyFields = itemSchema.keyFields;

			await this.Owner.WithTransaction(async (txact, arg, cx) =>
			{
				// Check if it exists
				long itemId = 0;
				int version = 0;
				byte[]? existingPropIdList = null;
				if (!itemSchema.isNewClass && (keyFields.Count > 0))
				{
					keyFields.Add(nameof(ItemData.ItemClassId), itemSchema.classId);
					var existing = (await InfoBase._tblItem.Select(
						txact,
						r => r.ItemClassId == itemSchema.classId && r.PartitionId == this.partitionId,
						r => new { ItemId = r.Id, r.Version, r.PropIdList },
						new SelectQueryInfo() { keys = keyFields },
						cx))?.FirstOrDefault();
					if (existing != null)
					{
						itemId = existing.ItemId;
						version = existing.Version + 1;
						existingPropIdList = existing.PropIdList;
					}
				}

				var itemFlags = ItemFlags.None;
				if (multiValues.Count > 0)
					itemFlags |= ItemFlags.HasMultiValues;

				byte[] propIdList = BuildPropertyIdList(itemSchema.propIds);

				if (itemId == 0)
				{
					version = 1;
					itemId = await InfoBase._tblItem.Insert(() => new ItemData
					{
						Version = 1,
						PartitionId = this.partitionId,
						SourceCommandId = this.id,
						ItemClassId = itemSchema.classId,
						PropIdList = propIdList,
						ItemFlags = itemFlags,
					}, txact, cx, extFields);
				}
				else
				{
					// TODO: Merge property ID lists
					if (existingPropIdList != null)
					{
						ReadPropListInto(existingPropIdList, itemSchema.propIds);
						propIdList = BuildPropertyIdList(itemSchema.propIds);
					}
					await InfoBase._tblItem.Update(txact, r => r.Id == itemId, r => new ItemData { Version = version, ItemFlags = itemFlags, PropIdList = propIdList }, extFields, cx);
				}

				var insertedAt = DateTime.UtcNow;
				var itemHistId = await InfoBase._tblItemHistory.Insert(() => new ItemHistory
				{
					ItemId = itemId,
					Version = version,
					InsertedAt = insertedAt,
					SourceCommandId = this.id,
					PropIdList = propIdList,
					Seq = seq,
					ItemFlags = itemFlags
				}, txact, cx, extFields);
				foreach (var multiValue in multiValues)
				{
					int seq = 0;
					foreach (var value_ in multiValue.Value)
					{
						seq++;
						var value = value_;
						if (value is IInfoValue info)
							value = info.GetValue();
						var tc = Convert.GetTypeCode(value);
						value = DataHelpers.ToDataValue(value);

						await InfoBase._tblItemMulti.Update(txact, r => r.ItemId == itemId && r.PropertyId == multiValue.Key && r.EndVersion == int.MaxValue,
							r => new ItemMultiValue
							{
								EndVersion = version
							}, null, cx);

						if (value != null)
						{
							var strValue = value.ToString();
							var blobValue = value as byte[];
							long? intValue = value as long?;
							double? realValue = value as double?;
							await InfoBase._itemMultiValueInserter.Insert(new ItemMultiValue
							{
								ItemId = itemId,
								StartVersion = version,
								EndVersion = int.MaxValue,
								PropertyId = multiValue.Key,
								Seq = seq,
								ClrTypeCode = tc,

								TextValue = strValue,
								BlobValue = blobValue,
								IntValue = intValue,
								RealValue = realValue
							}, txact, cx);
						}
					}
				}
			}, (object?)null, cancellationToken);
		}

		struct PropListCtx
		{
			private byte[] bytes;
			private int writeIndex;

			public void Init(int version)
			{
				this.bytes = ArrayPool<byte>.Shared.Rent(16);
				BinaryPrimitives.WriteInt16LittleEndian(this.bytes, PropListVersion);
				this.writeIndex = 2;
			}
			public void WriteByte(byte b)
			{
				if (this.writeIndex >= this.bytes.Length)
				{
					var newBytes = ArrayPool<byte>.Shared.Rent((this.writeIndex + 16) & ~0x0F);
					this.bytes.CopyTo(newBytes.AsSpan());
					ArrayPool<byte>.Shared.Return(this.bytes);
					this.bytes = newBytes;
				}
				this.bytes[this.writeIndex] = b;
				this.writeIndex++;
			}

			public byte[] GetBytesAndRelease()
			{
				var bytes = new byte[this.writeIndex];
				this.bytes.Slice(0, this.writeIndex).CopyTo(bytes);

				return bytes;
			}
		}
		private const int PropListVersion = 1;

		private void ReadPropListInto(byte[] propIdList, SortedSet<int> propIds)
		{
			if (propIdList.Length < 2)
				throw new ArgumentException($"The property ID list is invalid.", nameof(propIdList));

			var version = BinaryPrimitives.ReadInt16LittleEndian(propIdList);
			if (version != PropListVersion)
				throw new NotSupportedException($"The property ID list has an unsupported version {version}.");

			int readIndex = 2;
			int lastPropId = 0;
			while (readIndex < propIdList.Length)
			{
				int value = 0;
				byte b;
				do
				{
					b = propIdList[readIndex];
					value <<= 7;
					value |= (b & 0x7F);
					readIndex++;
				} while ((sbyte)b < 0);

				int propId = lastPropId + value;
				propIds.Add(propId);
				lastPropId = propId;
			}
		}

		private byte[] BuildPropertyIdList(SortedSet<int> propIds)
		{
			int lastPropId = 0;
			PropListCtx ctx = new PropListCtx();
			ctx.Init(PropListVersion);
			foreach (var id in propIds)
			{
				var diff = id - lastPropId;
				if (diff < (1 << 7))
					ctx.WriteByte((byte)diff);
				else if (diff < (1 << 14))
				{
					ctx.WriteByte((byte)((diff >> 7) | 0x80));
					ctx.WriteByte((byte)(diff & 0x7F));
				}
				else if (diff < (1 << 21))
				{
					ctx.WriteByte((byte)((diff >> 14) | 0x80));
					ctx.WriteByte((byte)((diff >> 7) | 0x80));
					ctx.WriteByte((byte)(diff & 0x7F));
				}
				else
					throw new NotImplementedException($"Property ID is out of range.");

				lastPropId = id;
			}

			var bytes = ctx.GetBytesAndRelease();
			return bytes;
		}
	}

	partial class InvocationLog : ILog
	{
		public LogMessageSeverity LogLevel { get; set; }
		public LogFormat Format { get; set; }

		public void MarkTaskComplete()
		{
		}

		private int _logSeq;
		public void WriteMessage(LogMessage message)
		{
			var id = Task.Factory.StartNew(() => this.Owner.WithTransaction(async (txact, arg, cx) =>
			{
				var recId = (int)await InfoBase._logInserter.Insert(new LogRecord
				{
					CommandId = this.id,
					Seq = Interlocked.Increment(ref this._logSeq),
					MessageId = message.MessageId,
					LoggedAt = message.LogDate,
					Severity = message.Severity,
					Source = message.Source,
					Text = message.Text
				}, txact, cx);

				if (message.Parameters != null && message.MessageType?.ParameterNames != null)
				{
					string[] names = message.MessageType.ParameterNames;
					for (int i = 0; i < names.Length; i++)
					{
						if (i < message.Parameters.Length)
						{
							string param = names[i];
							string value = message.Parameters[i]?.ToString();
							if (param != null && value != null)
							{
								await InfoBase._logValueInserter.Insert(new LogRecordParam
								{
									LogId = recId,
									Seq = i,
									Name = param,
									Value = value
								}, txact, cx);
							}
						}
					}
				}

				return recId;
			}, message, CancellationToken.None)).Unwrap().Result;
		}

		public void WriteTaskError(Exception ex)
		{
		}

		public void WriteTaskStart(string description)
		{
		}
	}
}
