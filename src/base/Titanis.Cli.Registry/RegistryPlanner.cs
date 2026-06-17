using Titanis.Winterop.Security;

namespace Titanis.Cli.Registry
{
	public class RegistryPlanner : IRegistryItemVisitor
	{
		public RegistryPlanner(ILog? log)
		{
			this._log = log;
		}

		public readonly List<RegistryKeyGroup> keys = new List<RegistryKeyGroup>();
		private RegistryKeyGroup? _currentGroup;
		private readonly ILog? _log;

		void IRegistryItemVisitor.Visit(RegistryKeySpec key)
		{
			var group = new RegistryKeyGroup(key);
			this.keys.Add(group);
			this._currentGroup = group;
		}

		void IRegistryItemVisitor.Visit(RegistryValueSpec value)
		{
			if (this._currentGroup is null)
				throw new SyntaxException($"Value '{value.ValueName}' specified without an open key.");

			this._currentGroup.values.Add(value);
			this._currentGroup.access |= RegistryAccessRights.SetValue;
		}
	}
}
