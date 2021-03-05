using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Titanis.Net;

namespace Titanis.Smb2.PowerShell
{
	internal class SmbConnectionParameters
	{
		public static SmbConnectionParameters GetDefault()
		{
			return new SmbConnectionParameters
			{
				RemotePort = Smb2Client.TcpPort,
				Dialects = Smb2ConnectionOptions.DefaultSupportedDialects,
				NameResolveOptions = NameResolverOptions.Default,
				Capabilities = Smb2ConnectionOptions.DefaultSmb3Caps,
				EnforceVersionCompliance = false,
				SecurityMode = Smb2SecurityMode.SigningEnabled,
				PreauthSaltLength = Smb2ConnectionOptions.DefaultPreauthSaltLength,
				Ciphers = Smb2ConnectionOptions.DefaultCiphers,
				SigningAlgorithms = Smb2ConnectionOptions.DefaultSigningAlgorithms,
				CompressionCapabilities = CompressionCaps.None,
				CompressionAlgorithms = Smb2ConnectionOptions.DefaultCompressionAlgorithms,
				KdcPort = 88,
			};
		}

		#region Connection
		[Parameter]
		public string? HostName { get; set; }
		[Parameter]
		public int? RemotePort { get; set; }
		[Parameter]
		public NameResolverOptions? NameResolveOptions { get; set; }

		[Parameter]
		public Smb2Capabilities? Capabilities { get; set; }
		[Parameter]
		public SwitchParameter EnforceVersionCompliance { get; set; }
		[Parameter]
		public Smb2Dialect[]? Dialects { get; set; }
		[Parameter]
		public Smb2SecurityMode? SecurityMode { get; set; }
		[Parameter]
		public SwitchParameter RequireSecureNegotiate { get; set; }
		[Parameter]
		public Guid? ClientGuid { get; set; }
		[Parameter]
		public int? PreauthSaltLength { get; set; }
		[Parameter]
		public Cipher[]? Ciphers { get; set; }
		[Parameter]
		public SigningAlgorithm[]? SigningAlgorithms { get; set; }
		[Parameter]
		public CompressionCaps? CompressionCapabilities { get; set; }
		[Parameter]
		public CompressionAlgorithm[]? CompressionAlgorithms { get; set; }

		public SmbConnectionParameters MergeOnto(SmbConnectionParameters baseParams)
		{
			if (baseParams is null) throw new ArgumentNullException(nameof(baseParams));

			SmbConnectionParameters merged = new SmbConnectionParameters()
			{
				HostName = this.HostName ?? baseParams.HostName,
				RemotePort = this.RemotePort ?? baseParams.RemotePort,
				Capabilities = this.Capabilities ?? baseParams.Capabilities,
				NameResolveOptions = this.NameResolveOptions ?? baseParams.NameResolveOptions,
				Dialects = this.Dialects ?? baseParams.Dialects,
				SecurityMode = this.SecurityMode ?? baseParams.SecurityMode,
				EnforceVersionCompliance = this.EnforceVersionCompliance.IsPresent ? this.EnforceVersionCompliance : baseParams.EnforceVersionCompliance,
				RequireSecureNegotiate = this.RequireSecureNegotiate.IsPresent ? this.RequireSecureNegotiate : baseParams.RequireSecureNegotiate,
				ClientGuid = this.ClientGuid ?? baseParams.ClientGuid,
				PreauthSaltLength = this.PreauthSaltLength ?? baseParams.PreauthSaltLength,
				Ciphers = this.Ciphers ?? baseParams.Ciphers,
				SigningAlgorithms = this.SigningAlgorithms ?? baseParams.SigningAlgorithms,
				CompressionCapabilities = this.CompressionCapabilities ?? baseParams.CompressionCapabilities,
				CompressionAlgorithms = this.CompressionAlgorithms ?? baseParams.CompressionAlgorithms,

				UserName = this.UserName ?? baseParams.UserName,
				UserDomain = this.UserDomain ?? baseParams.UserDomain,
				Password = this.Password ?? baseParams.Password,
				Kdc = this.Kdc ?? baseParams.Kdc,
				KdcPort = this.KdcPort ?? baseParams.KdcPort,
				NtlmHash = this.NtlmHash ?? baseParams.NtlmHash,
				Workstation = this.Workstation ?? this.Workstation
			};
			return merged;
		}

		public Smb2ConnectionOptions ToConnectionOptions()
		{
			// TODO: Ensure the parameter block is complete

			SmbConnectionParameters parms = this;
			Smb2ConnectionOptions connOptions = new Smb2ConnectionOptions()
			{
				Capabilities = parms.Capabilities.Value,
				EnforcesVersionCompliance = parms.EnforceVersionCompliance,
				SupportedDialects = parms.Dialects,
				SecurityMode = parms.SecurityMode.Value,
				RequiresSecureNegotiate = parms.RequireSecureNegotiate,
				ClientGuid = parms.ClientGuid ?? Guid.NewGuid(),
				PreauthSaltLength = parms.PreauthSaltLength.Value,
				SupportedCiphers = parms.Ciphers,
				SupportedSigningAlgorithms = parms.SigningAlgorithms,
				CompressionCaps = parms.CompressionCapabilities.Value,
				SupportedCompressionAlgorithms = parms.CompressionAlgorithms,
			};
			return connOptions;
		}
		#endregion

		[Parameter]
		public string? UserName { get; set; }
		[Parameter]
		public string? UserDomain { get; set; }
		[Parameter]
		public string? Password { get; set; }
		[Parameter]
		public Cli.HexString? NtlmHash { get; set; }
		[Parameter]
		public string? Kdc { get; set; }
		[Parameter]
		public int? KdcPort { get; set; }
		[Parameter]
		public string? Workstation { get; set; }
	}
}
