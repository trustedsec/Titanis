using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides functionality to support hierarchical output.
	/// </summary>
	public interface ISupportTreeOutput
	{
		/// <summary>
		/// Gets a <see cref="TreeHandler"/> to handle details of building the output hierarchy.
		/// </summary>
		/// <returns></returns>
		TreeHandler CreateTreeHandler();
	}
	/// <summary>
	/// Handles building hierarchical output.
	/// </summary>
	/// <seealso cref="ISupportTreeOutput.CreateTreeHandler"/>
	/// <remarks>
	/// As output is generated, the records are added by calling <see cref="AddRecord(object?)"/>.
	/// Once the output is complete, call <see cref="BuildTree"/> to generate an ordered list of nodes.
	/// </remarks>
	public abstract class TreeHandler
	{
		internal OutputField[] fields;

		public string? KeyField { get; set; }
		public OutputField? KeyDisplayField { get; set; }
		public OutputField? ReplaceDisplayField(string field)
		{
			if (string.Equals(field, this.KeyField, StringComparison.OrdinalIgnoreCase))
				return this.KeyDisplayField;
			else
				return null;
		}

		public OutputField[] GetDisplayFields()
		{
			var replace = this.ReplaceDisplayField(this.fields[0].Name);
			if (replace != null)
			{
				var copy = (OutputField[])this.fields.Clone();
				copy[0] = replace;
				this.fields = copy;
			}

			return this.fields;
		}

		/// <summary>
		/// Adds a record to the tree.
		/// </summary>
		/// <param name="record"></param>
		public abstract void AddRecord(object? record);
		/// <summary>
		/// Generates an ordered list of nodes.
		/// </summary>
		/// <returns>A list of <see cref="TreeNode"/> objects representing the records.</returns>
		public abstract IReadOnlyList<TreeNode> BuildTree();

		/// <summary>
		/// Represents a node within the tree.
		/// </summary>
		/// <param name="record">The record represented by the node</param>
		public abstract class TreeNode(object record)
		{
			/// <summary>
			/// Gets the record represented by the node.
			/// </summary>
			public object Record => record;
			/// <summary>
			/// Gets a bitfield indicating which lines to draw for parents.
			/// </summary>
			internal ulong lines;
			/// <summary>
			/// Gets the depth of the node
			/// </summary>
			internal int depth;
			/// <summary>
			/// Gets a value indicating whether this is the last child node of its parent.
			/// </summary>
			internal bool isLastChild;

			/// <summary>
			/// Builds the line art to preceed the node caption.
			/// </summary>
			/// <param name="forSubseqRow">Indicates whether this is for a row after the header</param>
			/// <returns>Line art to print</returns>
			internal abstract string BuildLineArt(bool forSubseqRow = false);
		}
	}

	public class TreeHandler<TKey, TRecord> : TreeHandler
	{
		public TreeHandler(
			Func<TRecord, TKey> keySelector,
			Func<TRecord, TKey> parentKeySelector,
			IEqualityComparer<TKey>? keyComparer = null,
			IComparer<TKey>? keySorter = null)
		{
			if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));
			if (parentKeySelector is null) throw new ArgumentNullException(nameof(parentKeySelector));

			this._keySelector = keySelector;
			this._parentKeySelector = parentKeySelector;
			this._keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			this._keySorter = keySorter;
			this._nodesByKey = new ConcurrentDictionary<TKey, TreeNodeTyped>(this._keyComparer);
		}

		class TreeNodeTyped(object record) : TreeNode(record)
		{
			internal TKey key;

			internal List<TreeNodeTyped>? childNodes;
			internal void AddChild(TreeNodeTyped node)
			{
				(this.childNodes ??= new List<TreeNodeTyped>()).Add(node);
			}

			public bool HasChildren => !this.childNodes.IsNullOrEmpty();

			internal override string BuildLineArt(bool subseq = false)
			{
				StringBuilder sb = new StringBuilder();

				var node = this;
				if (node.depth > 0)
				{
					for (int i = 0; i < node.depth - 1; i++)
					{
						bool hasLine = 0 != (node.lines & (1UL << (i + 1)));
						sb.Append(hasLine ? "│ " : "  ");
					}
					//sb.Append(node.isLastChild ? "└ " : "├ ");
					sb.Append(
						subseq ? node.isLastChild ? "  " : this.HasChildren ? "│ │ " : "│ "
						: (node.isLastChild ? "└─" : "├─"));
					//: ((node.isLastChild ? "└" : "├") + (this.HasChildren ? "┬" : "─")));
				}

				return sb.ToString();
			}
		}

		private readonly List<TreeNodeTyped> _nodes = new List<TreeNodeTyped>();
		private readonly ConcurrentDictionary<TKey, TreeNodeTyped> _nodesByKey;
		private readonly Func<TRecord, TKey> _keySelector;
		private readonly Func<TRecord, TKey> _parentKeySelector;
		private readonly IEqualityComparer<TKey>? _keyComparer;
		private readonly IComparer<TKey>? _keySorter;

		public override void AddRecord(object? record)
		{
			var node = new TreeNodeTyped(record);
			if (record is TRecord typed)
			{
				var key = this._keySelector(typed);
				node.key = key;
				// Ignore duplicate keys
				this._nodesByKey.TryAdd(key, node);
			}
			this._nodes.Add(node);
		}

		public override IReadOnlyList<TreeNode> BuildTree()
		{
			List<TreeNodeTyped> rootNodes = new List<TreeNodeTyped>();
			foreach (var node in this._nodes)
			{
				TreeNodeTyped? parentNode;
				if (node.Record is TRecord rec)
				{
					var parentKey = this._parentKeySelector(rec);
					this._nodesByKey.TryGetValue(parentKey, out parentNode);
				}
				else
					parentNode = null;

				if (parentNode != null)
					parentNode.AddChild(node);
				else
					rootNodes.Add(node);
			}

			List<TreeNode> allNodes = new(this._nodes.Count);
			ArrangeTree(rootNodes, 0, 0UL, allNodes);

			return allNodes;
		}

		private void ArrangeTree(List<TreeNodeTyped> nodes, int depth, ulong lineMask, List<TreeNode> allNodes)
		{
			if (nodes.Count > 0 && this._keySorter != null)
				nodes.Sort((x, y) => this._keySorter.Compare(x.key, y.key));

			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNodeTyped? node = nodes[i];
				allNodes.Add(node);

				var childMask = lineMask;
				bool isLast = (i == nodes.Count - 1);
				node.isLastChild = isLast;
				if (!isLast)
					childMask |= (1UL << depth);

				node.depth = depth;
				node.lines = childMask;

				if (node.childNodes != null)
					ArrangeTree(node.childNodes, depth + 1, childMask, allNodes);
			}
		}
	}
}
