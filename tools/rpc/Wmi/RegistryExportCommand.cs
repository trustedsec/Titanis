using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;

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

		HashSet<string> seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		TextWriter? writer;

		bool dataWritten = false;


		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if (string.IsNullOrEmpty(OutputFile))
			{
				writer = new StreamWriter(this.Context.OpenRawOutputStream());
			}
			else
			{
				if (File.Exists(OutputFile))
				{
					if (this.Overwrite.IsSet)
					{
						var attrs = File.GetAttributes(OutputFile);
						if (0 != (attrs & (FileAttributes.System | FileAttributes.ReadOnly | FileAttributes.Hidden)))
						{
							this.WriteWarning($"{OutputFile} marked as read-only, hidden, or system; clearing attributes and overwriting.");
							// Clear the read-only bit
							File.SetAttributes(OutputFile, FileAttributes.Normal);
						}
					}
				}
				var filestream = new FileStream(OutputFile, new FileStreamOptions
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

		protected override void WriteRegistryRecord(RegistryEntry entry)
		{
			dataWritten = true;
			Debug.Assert(writer != null);
			if (seenKeys.Add(entry.SubKey))
			{
				writer.WriteLine();
				writer.WriteLine($"[{entry.Root}\\{entry.SubKey}]");
			}
			entry.ExportTo(writer);
		}

		protected override void OnCommandComplete()
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
