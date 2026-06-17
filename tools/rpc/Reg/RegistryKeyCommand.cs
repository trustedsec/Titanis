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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		private RegistryPath _keyPath;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			try
			{
				_keyPath = RegistryPath.Parse(KeyPath);
			}
			catch (ArgumentException ex)
			{
				context.LogError(nameof(KeyPath), $"The Key path is invalid: ${ex.Message}");
			}

			RegistryKeyOptions options = RegistryKeyOptions.None;
			if (this.BackupSemantics.IsSet)
				options |= RegistryKeyOptions.BackupRestore;
		}

		protected abstract Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken);

		protected sealed override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{

			var rootKey = await client.OpenRootKey(this._keyPath.Root, _keyPath.IsRootPath ? this.RequiredKeyAccess : RegistryAccessRights.EnumerateSubkeys, cancellationToken);

			RegistryKey key;
			if (_keyPath.IsRootPath)
			{
				key = rootKey;
				rootKey = null;
			}
			else
			{
				key = await rootKey.OpenSubkey(this._keyPath.KeyPath, this.RequiredKeyAccess, this.KeyOptions, cancellationToken);
			}

			return await this.RunAsync(key, client, cancellationToken);
		}
	}
}
