using System.ComponentModel.Design;
using System.Text;
using System.Xml.Linq;
using Titanis.Linterop.Fuse;

namespace Titanis.Ldap.Fusion
{
	class LdapMountInfo
	{
		internal LdapClient ldapClient;
		internal uint uid;
		internal uint gid;
	}

	public abstract class LdapNodeBase : IFuseNode
	{
		private protected LdapNodeBase(LdapMountInfo mountInfo, string name)
		{
			this._mountInfo = mountInfo;
			this.Name = name;
		}

		internal readonly LdapMountInfo _mountInfo;
		internal LdapClient ldapClient => this._mountInfo.ldapClient;

		public string Name { get; }

		public virtual PosixFileMode Mode => PosixFileMode.Mode777;

		public abstract LinuxFileType FileType { get; }

		public uint Uid => this._mountInfo.uid;

		public uint Gid => this._mountInfo.gid;

		public abstract long FileSize { get; set; }

		public long BlockSize => 512;

		public long BlockCount => this.FileSize / BlockSize;

		public DateTime? LastAccessTime { get => null; set { } }

		public DateTime? LastWriteTime { get => null; set { } }

		public DateTime? LastChangeTime { get => null; set { } }

		public virtual Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task DeleteDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task DeleteFile(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual string[]? GetXAttributeNames() => null;

		public virtual Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}

	public sealed class LdapEntryNode : LdapNodeBase
	{
		private const string AttributesName = "attributes.txt";
		private const string ErrorName = "error.txt";

		internal LdapEntryNode(LdapMountInfo mountInfo, string name, LdapEntry entry)
			: base(mountInfo, name)
		{
			this.Entry = entry;
		}

		public LdapEntry Entry { get; }

		public override LinuxFileType FileType => LinuxFileType.Directory;

		public override long FileSize { get => 0x1000; set { } }

		public override Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			return Task.FromResult<IFuseOpenDirectory>(new LdapOpenDir(this));
		}


		public override async Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			if (name == AttributesName)
			{
				return GetAttributesNode();
			}
			else if (name == ErrorName)
			{
				return this.ErrorNode;
			}
			else
			{
				var entry = await TryGetEntry(name, cancellationToken).ConfigureAwait(false);
				return entry != null ? new LdapEntryNode(this._mountInfo, name, entry) : (IFuseNode?)null;
			}
		}

		private async Task<LdapEntry?> TryGetEntry(string name, CancellationToken cancellationToken)
		{
			return (await ldapClient.Search(new LdapQuery(Entry.EntryName, LdapSearchScope.SingleLevel, LdapFilter.Parse($"(name={name})"), null), cancellationToken).ConfigureAwait(false)).Entries.FirstOrDefault();
		}

		public override async Task DeleteFile(string name, CancellationToken cancellationToken)
		{
			if (name == ErrorName)
			{
				this.SetError(null);
			}

			// When deleting a directory, the shell may see the directory has files in it (i.e. attributes.txt) and try to delete them
			// allow the deletion to proceed without error, but do nothing.
			// This allows the caller to think the directory is now empty and attempt to delete it.
		}

		public override async Task DeleteDirectory(string name, CancellationToken cancellationToken)
		{
			var entry = await TryGetEntry(name, cancellationToken).ConfigureAwait(false);
			await ldapClient.Delete(entry.EntryName, cancellationToken).ConfigureAwait(false);
		}

		internal IFuseNode GetAttributesNode()
		{
			return new LdapAttributesNode(this._mountInfo, AttributesName, this);
		}


		private string? _error;
		internal IFuseNode? ErrorNode { get; private set; }
		internal void SetError(string? error)
		{
			this._error = error;
			if (!string.IsNullOrEmpty(error))
			{
				this.ErrorNode = new FuseMemoryFileNode(ErrorName, this.Uid, this.Gid, Encoding.UTF8.GetBytes(this._error ?? string.Empty));
			}
			else
			{
				this.ErrorNode = null;
			}
		}
	}

	sealed class LdapAttributesNode : FuseMemoryFileNode
	{
		internal LdapAttributesNode(LdapMountInfo mountInfo, string name, LdapEntryNode node) : base(name, mountInfo.uid, mountInfo.gid, [], PosixFileMode.ModeWriteAll)
		{
			this.EntryNode = node;
			this.Contents = this.BuildAttrContents(node.Entry);
		}

		private List<AttrValueText> _baseAttrs;
		private byte[] BuildAttrContents(LdapEntry entry)
		{
			StringWriter writer = new StringWriter();
			writer.WriteLine($"dn: {entry.EntryName}");
			List<AttrValueText> baseAttrs = new List<AttrValueText>();
			foreach (var attr in entry.Attributes)
			{
				if (attr.TypeName == LdapAttributeTypes.DistinguishedName.Name)
					continue;

				foreach (var value in attr.Values)
				{
					writer.WriteLine($"{attr.TypeName}: {value}");
					baseAttrs.Add(new AttrValueText(attr.AttributeType, value?.ToString(), value));
				}
			}

			// Add a blank line
			writer.WriteLine();

			this._baseAttrs = baseAttrs;

			byte[] bytes = Encoding.UTF8.GetBytes(writer.ToString());
			return bytes;
		}

		public LdapEntryNode EntryNode { get; }
		public LdapEntry Entry => this.EntryNode.Entry;

		struct AttrValueText : IEquatable<AttrValueText>
		{
			internal AttrValueText(AttributeTypeDescription attrType, string text, object? value)
			{
				AttrType = attrType;
				Text = text;
				Value = value;
			}

			public AttributeTypeDescription AttrType { get; }
			public string Text { get; }
			public object? Value { get; }

			public override bool Equals(object? obj)
			{
				return obj is AttrValueText text && Equals(text);
			}

			public bool Equals(AttrValueText other)
			{
				return EqualityComparer<AttributeTypeDescription>.Default.Equals(AttrType, other.AttrType) &&
					   Text == other.Text;
			}

			public override int GetHashCode()
			{
				return System.HashCode.Combine(AttrType, Text);
			}

			public static bool operator ==(AttrValueText left, AttrValueText right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(AttrValueText left, AttrValueText right)
			{
				return !(left == right);
			}
		}

		protected override async Task Commit(CancellationToken cancellationToken)
		{
			StringBuilder sbResult = new StringBuilder();

			try
			{
				List<AttrValueText> existingAttrs = new List<AttrValueText>(this._baseAttrs);
				List<AttrValueText> newBaseline = new List<AttrValueText>(this._baseAttrs);

				LdapEntry entry = this.Entry;
				LdapModifyRequest modreq = new LdapModifyRequest(entry.EntryName);

				var reader = new StringReader(Encoding.UTF8.GetString(this.Contents));
				int lineIndex = 0;
				while (reader.Peek() >= 0)
				{
					var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
					lineIndex++;

					if (string.IsNullOrEmpty(line))
						// Skip empty lines
						continue;

					int isep = line.IndexOf(':');
					if (isep <= 0)
					{
						sbResult.AppendLine($"Line {lineIndex}: Bad attribute specification");
						continue;
					}

					var attrName = line.Substring(0, isep).Trim();
					if (attrName.Equals("dn", StringComparison.OrdinalIgnoreCase))
						// TODO: Handle renames
						continue;

					var attrType = LdapAttributeTypes.TryGetByNameOrOid(attrName);
					if (attrType is null)
					{
						sbResult.AppendLine($"Line {lineIndex}: Unknown attribute '{attrName}'.");
						continue;
					}

					var attrText = line.Substring(isep + 1).TrimStart();
					var changeKey = new AttrValueText(attrType, attrText, null);
					if (existingAttrs.Remove(changeKey))
					{
						// Existing attribute; no change
						continue;
					}

					try
					{
						object value = attrType.Syntax.Parse(attrText);
						modreq.AddValue(attrType.Name, value);

						newBaseline.Add(new AttrValueText(attrType, attrText, value));
					}
					catch (Exception ex)
					{
						sbResult.AppendLine($"Line {lineIndex}: Bad attribute value: {ex.Message}");
						continue;
					}
				}

				bool hasError = sbResult.Length > 0;
				if (!hasError)
				{
					if (existingAttrs.Count > 0)
					{
						foreach (var attr in existingAttrs)
						{
							modreq.DeleteValue(attr.AttrType.Name, attr.Value);
							newBaseline.Remove(attr);
						}
					}

					if (modreq.Changes.Count > 0)
					{
						await this.EntryNode.ldapClient.Modify(modreq, cancellationToken).ConfigureAwait(false);
						this._baseAttrs = newBaseline;
					}
				}
			}
			catch (Exception ex)
			{
				sbResult.AppendLine($"Error modifying entry: {ex.Message}");
			}
			finally
			{
				this.EntryNode.SetError(sbResult.ToString());
			}
		}
	}


	class LdapOpenDir : OpenDirBase
	{
		internal LdapOpenDir(LdapEntryNode node)
		{
			this._node = node;
		}


		private LdapEntryNode _node;
		public override IFuseNode Node => this._node;
		protected LdapClient ldapClient => this._node.ldapClient;

		private LdapEntry[]? _children;

		protected override async Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken)
		{
			if (this._children == null)
			{
				this._children = (await ldapClient.Search(new LdapQuery(_node.Entry.EntryName, LdapSearchScope.SingleLevel, null, null), cancellationToken).ConfigureAwait(false)).Entries;
			}

			if (index == 0)
			{
				return this._node.GetAttributesNode();
			}
			else
			{
				index--;

				if (this._node.ErrorNode != null)
				{
					if (index == 0)
						return this._node.ErrorNode;

					index--;
				}

				if (index < this._children.Length)
				{
					var entry = this._children[index];
					var name = entry.EntryName.Rdns[0].Values[0];
					return new LdapEntryNode(this._node._mountInfo, name, entry);
				}
				else
					return null;
			}
		}
	}
}
