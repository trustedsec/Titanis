using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Registry
{
	class RegistryExporter
	{

	}
	public static class RegistryExporterExtensions
	{
		public static void ExportKeyToFile(this IRegistryKey key, string fileName)
		{

		}

		public static RegistryExportResult ExportKeyTo(this IRegistryKey key, TextWriter writer)
		{
			ArgumentNullException.ThrowIfNull(key);
			ArgumentNullException.ThrowIfNull(writer);

			writer.WriteLine("Windows Registry Editor Version 5.00");

			throw new NotImplementedException();
		}
	}

	public record struct RegistryExportResult(int KeyCount, int EntryCount)
	{
	}
}
