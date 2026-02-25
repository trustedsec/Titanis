using System.ComponentModel;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	abstract class RegistryKeyCommand : RegistryCommand
	{
		[Parameter(10)]
		[Mandatory]
		[Description("Path of target registry key")]
		public string KeyPath { get; set; }

		/// <summary>
		/// Gets the <see cref="RegistryAccessRights"/> required for the command.
		/// </summary>
		protected abstract RegistryAccessRights RequiredKeyAccess { get; }

		private RegistryRootKey _rootKey;
		private string? _keyPath;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			int isep = this.KeyPath.IndexOfAny(new char[] { '/', '\\' });
			string rootName;
			string? path;
			if (isep > 0)
			{
				rootName = this.KeyPath.Substring(0, isep);
				path = this.KeyPath.Substring(isep + 1);

				var sep = this.KeyPath[isep];
				if (sep == '/')
					path = path.Replace('/', '\\');
			}
			else
			{
				rootName = this.KeyPath;
				path = null;
			}

			RegistryRootKey rootKey = RemoteRegistryClient.TryResolveRootKey(rootName);
			if (rootKey == RegistryRootKey.Invalid)
				context.LogError($"The key path begins with an unsupported root key '{rootName}'");

			this._rootKey = rootKey;
			this._keyPath = path;

			RegistryKeyOptions options = RegistryKeyOptions.None;
			if (this.BackupSemantics.IsSet)
				options |= RegistryKeyOptions.BackupRestore;
		}


		protected abstract Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken);

		protected sealed override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			bool keyIsRoot = (this._keyPath is null);
			var rootKey = await client.OpenRootKey(this._rootKey, keyIsRoot ? this.RequiredKeyAccess : RegistryAccessRights.EnumerateSubkeys, cancellationToken);

			RegistryKey key;
			if (string.IsNullOrEmpty(this._keyPath))
			{
				key = rootKey;
				rootKey = null;
			}
			else
			{
				key = await rootKey.OpenSubkey(this._keyPath, this.RequiredKeyAccess, this.KeyOptions, cancellationToken);
			}

			return await this.RunAsync(key, client, cancellationToken);
		}
	}
}
