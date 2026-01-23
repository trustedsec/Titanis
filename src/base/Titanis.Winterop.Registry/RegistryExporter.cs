using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Registry
{
	public class RegistryExporter:IRegistrySearchCallback
	{
		public RegistryExporter(TextWriter writer)
		{
			ArgumentNullException.ThrowIfNull(writer);

			this._writer = writer;

			writer.WriteLine("Windows Registry Editor Version 5.00");
		}

		private readonly TextWriter _writer;

		public int KeyCount { get; private set; }
		public int ValueCount { get; private set; }

		private RegistryPath? _lastPath;

		public void WriteKey(RegistryPath keyPath)
		{
			ArgumentNullException.ThrowIfNull(keyPath);
			this._lastPath = keyPath;
			this.WriteKeySectionHeader(keyPath);

			this.KeyCount++;
		}

		public void WriteValue(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData)
		{
			if (this._lastPath != keyPath)
			{
				this._lastPath = keyPath;
				WriteKeySectionHeader(keyPath);
			}

			if (valueData is null)
				return;

			this.ValueCount++;

			string valueNameEscaped = string.IsNullOrEmpty(valueName) ? "@=" : $"\"{valueName.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"=";
			this._writer.Write(valueNameEscaped);

			valueData.ExportTo(this._writer, valueNameEscaped.Length);
		}

		private void WriteKeySectionHeader(RegistryPath keyPath)
		{
			this._writer.WriteLine();
			this._writer.WriteLine($"[{keyPath.Root}\\{keyPath.KeyPath}]");
		}

		public void Close()
		{
			var writer = this._writer;
			writer.Flush();
			writer.Close();
			writer.Dispose();
		}

		void IRegistrySearchCallback.OnKeyMatch(RegistryPath keyPath)
		{
			this.WriteKey(keyPath);
		}

		void IRegistrySearchCallback.OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData)
		{
			this.WriteValue(keyPath, valueName, valueKind, valueData);
		}
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
