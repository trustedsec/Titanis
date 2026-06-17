using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Titanis.Cli
{
	public class FileTypeInfo
	{
		public FileTypeInfo(string description, params string[] patterns)
		{
			Description = description;
			Patterns = patterns;
		}

		public string Description { get; }
		public string[] Patterns { get; }
	}
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public sealed class FileTypeAttribute : Attribute
	{
		public FileTypeAttribute(string description, params string[] pattern)
		{
			this.FileType = new FileTypeInfo(description, pattern);
		}

		public override object TypeId => this.FileType.Description;
		public FileTypeInfo FileType { get; }
	}

	[AttributeUsage(AttributeTargets.Property)]
	public abstract class FileSpecAttribute : Attribute
	{
		protected FileSpecAttribute(bool mustExist, ImmutableArray<FileTypeInfo> fileTypes)
		{
			MustExist = mustExist;
			FileTypes = fileTypes;
		}

		public bool MustExist { get; }
		public ImmutableArray<FileTypeInfo> FileTypes { get; }
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class KerberosTicketFileSpecAttribute : FileSpecAttribute
	{
		public static readonly ImmutableArray<FileTypeInfo> TicketFileTypes = [
			new FileTypeInfo("Kerberos ticket file", "*.ccache", "*.kirbi")
			];
		public KerberosTicketFileSpecAttribute(bool mustExist)
			: base(mustExist, TicketFileTypes)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RegistryFileSpecAttribute : FileSpecAttribute
	{
		public static readonly ImmutableArray<FileTypeInfo> RegistryFileTypes = [
			new FileTypeInfo("Registry export file", "*.reg")
			];
		public RegistryFileSpecAttribute(bool mustExist)
			: base(mustExist, RegistryFileTypes)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class KeytabFileSpecAttribute : FileSpecAttribute
	{
		public static readonly ImmutableArray<FileTypeInfo> KeytabFileTypes = [
			new FileTypeInfo("Kerberos keytab file", "*.keytab", "*.kt")
			];
		public KeytabFileSpecAttribute(bool mustExist)
			: base(mustExist, KeytabFileTypes)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CertificateFileSpecAttribute : FileSpecAttribute
	{
		public static readonly ImmutableArray<FileTypeInfo> CertificateFileTypes = [
			new FileTypeInfo("Certificate file", "*.cer", "*.crt", "*.pem", "*.pfx", "*.p12")
			];
		public CertificateFileSpecAttribute(bool mustExist)
			: base(mustExist, CertificateFileTypes)
		{
		}
	}
}
