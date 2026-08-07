using System.Reflection;
using Titanis.Winterop.Security;

namespace Titanis.Info.Schema
{
	class SchemaInfo
	{
		[PrimaryKey]
		public int Id { get; set; }
		public int SchemaVersion { get; set; }
	}
	class Partition
	{
		[PrimaryKey]
		public long Id { get; set; }
		public string Name { get; set; }
	}
	class CommandLog
	{
		[PrimaryKey]
		public long Id { get; set; }
		public string CommandName { get; set; }
		public string Version { get; set; }
		public int PartitionId { get; set; }
		public string? LogComment { get; set; }
		public string? RunningAsUser { get; set; }
		public string? RunningOnComputer { get; set; }
		public string? CommandLine { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public int? ExitCode { get; set; }
		public string? ErrorDetails { get; set; }
	}
	class CommandArg
	{
		[PrimaryKey]
		public long Id { get; set; }
		public long CommandId { get; set; }
		public int Seq { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
	class LogRecord
	{
		[PrimaryKey]
		public long Id { get; set; }
		public long CommandId { get; set; }
		public int Seq { get; set; }
		public int MessageId { get; set; }
		public DateTime LoggedAt { get; set; }

		public LogMessageSeverity Severity { get; set; }
		public string? Text { get; set; }
		public string? Source { get; set; }
	}
	class LogRecordParam
	{
		[PrimaryKey]
		public long Id { get; set; }
		public long LogId { get; set; }
		public int Seq { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
	[Flags]
	enum ItemFlags
	{
		None = 0,
		HasMultiValues = 1,
	}
	class ItemData
	{
		[PrimaryKey]
		public long Id { get; set; }
		public int Version { get; set; }
		public int SourceCommandId { get; set; }
		public int ItemClassId { get; set; }
		public int PartitionId { get; set; }
		public byte[] PropIdList { get; set; }
		public ItemFlags ItemFlags { get; set; }
	}
	class ItemHistory
	{
		[PrimaryKey]
		public long Id { get; set; }
		public long ItemId { get; set; }
		public int Version { get; set; }
		public DateTime InsertedAt { get; set; }
		public byte[] PropIdList { get; set; }
		public int SourceCommandId { get; set; }
		public int Seq { get; set; }
		public ItemFlags ItemFlags { get; set; }
	}
	class ItemMultiValue
	{
		[PrimaryKey]
		public long Id { get; set; }
		public long ItemId { get; set; }
		public int PropertyId { get; set; }
		public int StartVersion { get; set; }
		public int EndVersion { get; set; }
		public long Seq { get; set; }
		public TypeCode ClrTypeCode { get; set; }
		public string? TextValue { get; set; }
		public long? IntValue { get; set; }
		public double? RealValue { get; set; }
		public byte[]? BlobValue { get; set; }
	}
	class ItemClass
	{
		[PrimaryKey]
		public int Id { get; set; }
		public string Name { get; set; }
	}

	[Flags]
	enum ItemPropertyFlags
	{
		None = 0,
		IsKey = 1,
		Multi = 2,
	}
	class ItemProperty
	{
		[PrimaryKey]
		public int Id { get; set; }
		public int ClassId { get; set; }
		public string Name { get; set; }
		public string FieldName { get; set; }
		public TypeCode ClrTypeCode { get; set; }
		public ItemPropertyFlags Flags { get; set; }
	}
	class SecDesc
	{
		[PrimaryKey]
		public int Id { get; set; }
		public byte[] Md5Hash { get; set; }
		public string Sddl { get; set; }
		public byte[] BinaryForm { get; set; }
		public string? OwnerSid { get; set; }
		public string? Group { get; set; }
	}
	class Ace
	{
		internal MandatoryLabelPolicy? LabelPolicy;

		[PrimaryKey]
		public long Id { get; set; }
		public int SecDescId { get; set; }
		public SecurityDescriptorSections Section { get; set; }

		public int Seq { get; set; }
		public string? Trustee { get; set; }
		public AccessControlEntryFlags Flags { get; set; }
		public AccessControlEntryType AceType { get; set; }
		public uint AccessMask { get; set; }
		public Guid? ObjectType { get; set; }
		public Guid? InheritedObjectType { get; set; }
		public byte[]? CallbackData { get; set; }
	}
}
