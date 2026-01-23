using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;

namespace Wmi.Registry
{

	[Description("Export registry values to file")]
	[Example(@"Export all values and direct subkeys of HKLM\Software\MyApp to a file named results.reg", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -OutputFile results.reg")]
	[Example(@"Query the value name 'InstallPath' under HKLM\Software\MyApp to a file named results.reg", @"{0} -UserName milchick -Password Br3@kr00m! -ValueName InstallPath \\LUMON-FS1\HKLM\Software\MyApp -OutputFile results.reg")]
	[Example(@"Finds all non-empty default value under HKLM\Software\Microsoft to a file, printing those results to stdout", @"{0} -UserName milchick -Password Br3@kr00m! -ValueEmpty -Recursive \\LUMON-FS1\HKLM\Software\Microsoft")]
	[Example(@"Search for any value name or data item containing the string 'password' under HKLM\Software to a file named results.reg", @"{0} -UserName milchick -Password Br3@kr00m! -ValueSearch -DataSearch -SearchPattern password -Recursive \\LUMON-FS1\HKLM\Software -OutputFile results.reg")]
	internal class RegistryExportCommand : RegistryQueryCommandBase
	{
		[Parameter]
		[Description("Name of output file")]
		public string? OutputFile { get; set; }

		[Parameter]
		[Description("Overwrites existing output file")]
		public SwitchParam Overwrite { get; set; }

		private RegistryExporter? _exporter;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if (string.IsNullOrEmpty(OutputFile))
			{
				// Nothing to check
			}
			else
			{
				var outputFilePath = this.ResolveFsPath(this.OutputFile);
				if (File.Exists(outputFilePath))
				{
					if (this.Overwrite.IsSet)
					{
						var attrs = File.GetAttributes(outputFilePath);
						if (0 != (attrs & (FileAttributes.System | FileAttributes.ReadOnly | FileAttributes.Hidden)))
						{
							this.WriteWarning($"{outputFilePath} marked as read-only, hidden, or system; clearing attributes and overwriting.");
							// Clear the read-only bit
							File.SetAttributes(outputFilePath, FileAttributes.Normal);
						}
					}
					else
					{
						context.LogError(nameof(OutputFile), $"Output file '{outputFilePath}' already exists.  Use -{nameof(Overwrite)} to overwrite.");
					}
				}
			}
		}

		protected override void OnBeforeQuery()
		{
			base.OnBeforeQuery();

			TextWriter writer;
			if (string.IsNullOrEmpty(OutputFile))
			{
				writer = new StreamWriter(this.Context.OpenRawOutputStream());
			}
			else
			{
				var outputFileName = this.ResolveFsPath(this.OutputFile);

				this.WriteDiagnostic($"Creating output file '{outputFileName}'");
				var filestream = new FileStream(outputFileName, new FileStreamOptions
				{
					Mode = FileMode.Create,
					Access = FileAccess.Write,
					Share = FileShare.Read,
					Options = FileOptions.None,
				});
				filestream.Write(Encoding.Unicode.GetPreamble());
				writer = new StreamWriter(filestream, Encoding.Unicode);
			}

			this._exporter = new RegistryExporter(writer);
		}

		protected override void OnKeyMatch(RegistryPath keyPath)
		{
			Debug.Assert(this._exporter != null);

			this._exporter.WriteKey(keyPath);
		}

		protected override void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData)
		{
			Debug.Assert(this._exporter != null);

			this._exporter.WriteValue(keyPath, valueName, valueKind, valueData);
		}

		protected override void OnQueryComplete()
		{
			if (this._exporter is not null)
			{
				this._exporter.Close();

				var dataWritten = this._exporter.KeyCount > 0 || this._exporter.ValueCount > 0;
				if (!dataWritten)
				{
					// Delete the file if nothing was written
					if (this.OutputFile != null)
					{
						File.Delete(OutputFile);
						this.WriteWarning($"No data found to export.");
					}
				}
			}
		}
	}

}
