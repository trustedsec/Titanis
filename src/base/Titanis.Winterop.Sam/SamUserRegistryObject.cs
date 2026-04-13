using System.Collections.Immutable;
using System.Text;
using Titanis.Winterop.Sam;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public class SamUserRegistryObject : SamRegistryObjectBase
	{
		/// <summary>
		/// Number of variable attributes for a user object.
		/// </summary>
		/// <remarks>
		/// Calculated by measuring the difference between the offset of an account name within the data and the offset specified within the data.
		/// </remarks>
		private const int UserAttributeCount = 17;

		public SamUserRegistryObject(
			SamStore store,
			uint rid,
			ImmutableArray<byte> fixedData,
			ImmutableArray<byte> variableData
			)
			: base(store, fixedData, variableData, UserAttributeCount)
		{
			this.Rid = rid;
		}

		public uint Rid { get; }

		private ReadOnlySpan<byte> GetVariableUserAttribute(SamUserAttrIndex attr) => base.GetVariableAttribute((int)attr);
		private string? GetStringAttribute(SamUserAttrIndex attr)
		{
			if (!this.HasVariableAttributeData)
				return null;

			return Encoding.Unicode.GetString(this.GetVariableUserAttribute(attr));
		}

		public string? AccountName => this.GetStringAttribute(SamUserAttrIndex.AccountName);
		public string? FullName => this.GetStringAttribute(SamUserAttrIndex.FullName);
		public string? Description => this.GetStringAttribute(SamUserAttrIndex.Description);
		public string? UserComment => this.GetStringAttribute(SamUserAttrIndex.UserComment);
		public string? HomeDirectory => this.GetStringAttribute(SamUserAttrIndex.HomeDir);
		public string? HomeDrive => this.GetStringAttribute(SamUserAttrIndex.HomeDrive);
		public string? LogonScript => this.GetStringAttribute(SamUserAttrIndex.LogonScript);
		public SamEncryptedBlob EncryptedLmHash => new SamEncryptedBlob(this.GetVariableUserAttribute(SamUserAttrIndex.EncryptedLmHash));
		public SamEncryptedBlob EncryptedNtHash => new SamEncryptedBlob(this.GetVariableUserAttribute(SamUserAttrIndex.EncryptedNtHash));

		public byte[]? GetDecryptedLmHash() => (this.EncryptedLmHash.IsEmpty) ? null : this.Store.Decrypt(this.Rid, this.EncryptedLmHash);
		public byte[]? GetDecryptedNtHash() => (this.EncryptedNtHash.IsEmpty) ? null : this.Store.Decrypt(this.Rid, this.EncryptedNtHash);
	}
	/// <summary>
	/// Specifies an attribute of a user object.
	/// </summary>
	/// <remarks>
	/// Most of these were discerned through experimentation (i.e. setting properties in Computer Management and seeing what happens).  Some (e.g. <see cref="Workstations"/>) are guesses based on [MS-SAMR].
	/// </remarks>
	enum SamUserAttrIndex
	{
		SecurityDescriptor = 0,
		AccountName,
		FullName,
		Description,
		UserComment,
		Parameters,
		HomeDir,
		HomeDrive,
		LogonScript,
		ProfilePath,
		Workstations,
		LogonHours,
		Unknown12,
		EncryptedLmHash,
		EncryptedNtHash,
		NtHistory,
		LmHistory,
		Unknown17
	}
}
