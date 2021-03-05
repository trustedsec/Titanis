using ms_samr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Msrpc.Mssamr
{
	public enum SamEntryType
	{
		User = SID_NAME_USE.SidTypeUser,
		Group = SID_NAME_USE.SidTypeGroup,
		Domain = SID_NAME_USE.SidTypeDomain,
		Alias = SID_NAME_USE.SidTypeAlias,
		WellKnownGroup = SID_NAME_USE.SidTypeWellKnownGroup,
		DeletedAccount = SID_NAME_USE.SidTypeDeletedAccount,
		Invalid = SID_NAME_USE.SidTypeInvalid,
		Unknown = SID_NAME_USE.SidTypeUnknown,
		Computer = SID_NAME_USE.SidTypeComputer,
		Label = SID_NAME_USE.SidTypeLabel,
	}

	public class SamEntry
	{
		internal SamEntry(uint id, string name, SamEntryType entryType)
		{
			this.Id = id;
			this.Name = name;
			this.EntryType = entryType;
		}

		public uint Id { get; }
		public string Name { get; }
		public SamEntryType EntryType { get; set; }

		public override string ToString()
			=> $"{this.EntryType} {this.Name} : [{this.Id}]";
	}
}
