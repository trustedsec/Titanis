using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <summary>
/// Base class for commands that iterate over WMI objects.
/// </summary>
[OutputRecordType(typeof(WmiObject))]
internal abstract class WmiObjectCommandBase : WmiNamespaceCommandBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(10)]
	[Mandatory]
	[Description("Path to object or WQL query of objects to invoke on")]
	public string[] ObjectPathOrWqlQuery { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	[Parameter]
	[Description("Continue even if errors occur")]
	public SwitchParam ContinueOnError { get; set; }

	/// <summary>
	/// Called for each object selected by the user.
	/// </summary>
	/// <param name="obj">WMI object</param>
	/// <param name="scope"><see cref="WmiScope"/> containing the object</param>
	/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
	protected abstract Task ProcessObject(WmiObject obj, WmiScope scope, CancellationToken cancellationToken);

	/// <inheritdoc/>
	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		int count = 0;

		foreach (var spec in this.ObjectPathOrWqlQuery)
		{
			if (
				spec.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
				|| spec.StartsWith("ASSOCIATORS OF", StringComparison.OrdinalIgnoreCase)
				)
			{
				var wql = spec;
				this.WriteDiagnostic($"Running query '{wql}'");

				WmiQueryReader query;
				try
				{
					query = await ns.ExecuteWqlQueryAsync(wql, 1, cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Encountered error running '{wql}': {ex.Message}");
					if (this.ContinueOnError.IsSet)
						continue;
					else
						throw;
				}

				bool hasObject = false;
				while (await query.ReadAsync(cancellationToken))
				{
					hasObject = true;
					try
					{
						//this.WriteDiagnostic($"Processing object {query.Current.RelativePath}");
						await ProcessObject(query.Current, ns, cancellationToken);
						count++;
					}
					catch (Exception ex)
					{
						this.WriteError($"Failed: {ex.Message}");
						if (this.ContinueOnError.IsSet)
							continue;
						else
							throw;
					}
				}

				if (!hasObject)
					this.WriteWarning("No invocations because the query did not yield any instances");
			}
			else
			{
				string objPath = spec;
				this.WriteDiagnostic($"Getting object with path '{objPath}'");

				WmiObject? obj;
				try
				{
					obj = await ns.GetObjectAsync(objPath, cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Encountered error running '{objPath}': {ex.Message}");
					if (this.ContinueOnError.IsSet)
						continue;
					else
						throw;
				}

				if (obj != null)
				{
					this.WriteDiagnostic($"Processing object {obj.RelativePath}");
					try
					{
						await ProcessObject(obj, ns, cancellationToken);
					}
					catch (Exception ex)
					{
						this.WriteError($"Failed: {ex.Message}");
						if (this.ContinueOnError.IsSet)
							continue;
						else
							throw;
					}
					count++;
				}
				else
				{
					this.WriteError($"Object path `{objPath}' did not return an object.");
				}
			}
		}

		this.WriteVerbose($"Processed {count} object(s)");
		return 0;
	}

}
