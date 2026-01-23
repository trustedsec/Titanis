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

		TextWriter? writer;
		private bool dataWritten;


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
			writer.WriteLine("Windows Registry Editor Version 5.00");
		}

		private RegistryPath? _lastPath;
		protected override void OnKeyMatch(RegistryPath keyPath)
		{
			this._lastPath = keyPath;
			this.dataWritten = true;
		}

		protected override void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData)
		{
			dataWritten = true;
			Debug.Assert(writer != null);

			if (this._lastPath != keyPath)
			{
				this._lastPath = keyPath;
				WriteKeySectionHeader(keyPath);
			}

			var entry = new RegistryEntry(keyPath, valueName, valueData);
			entry.ExportTo(writer);
		}

		private void WriteKeySectionHeader(RegistryPath keyPath)
		{
			writer.WriteLine();
			writer.WriteLine($"[{keyPath.Root}\\{keyPath.KeyPath}]");
		}

		protected override void OnQueryComplete()
		{
			if (writer is not null)
			{
				if (dataWritten)
				{
					//reg.exe always seems to have an extra newline at the end
					writer.WriteLine();
				}
				writer.Flush();
				writer.Close();
				writer.Dispose();
				if (!dataWritten)
				{
					// Delete the file if nothing was written
					File.Delete(OutputFile);
					this.WriteWarning($"No data found to export.");
				}
			}
		}
	}

}
