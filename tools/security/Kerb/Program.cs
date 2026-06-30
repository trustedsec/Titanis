using System.ComponentModel;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	[Description("Commands for working with Kerberos authentication")]
	[Subcommand("getasinfo", typeof(GetASInfoCommand))]
	[Subcommand("asreq", typeof(AsreqCommand))]
	[Subcommand("tgsreq", typeof(RequestTicketCommand))]
	[Subcommand("renew", typeof(RenewTicketCommand))]
	[Subcommand("select", typeof(SelectCommand))]
	[Subcommand("changepw", typeof(ChangePasswordCommand))]
	[Subcommand("setpw", typeof(SetPasswordCommand))]
	[Subcommand("s2k", typeof(S2kCommand))]
	[Subcommand("keytab", typeof(KeytabCommand))]
	[Subcommand("forge", typeof(ForgeCommand))]
	public class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);

		public static bool TryPrintAuthorizationData(TicketInfo ticket, string heading, ILog log)
		{
			ArgumentNullException.ThrowIfNull(ticket);
			ArgumentNullException.ThrowIfNull(log);

			try
			{
				var asrepKey = ticket.AsrepKey;
				//var asrepKey = krb.CreateSessionKeyFor(this.AsrepKey);
				var authzData = ticket.DecryptAuthorizationData(ticket.TicketKey, asrepKey);

				log.WriteInfo(heading);

				// Hashes (PKINIT only)
				log.PrintIf($"  LM hash: ", authzData.LmHash?.ToHexString());
				log.PrintIf($"  NTLM hash: ", authzData.NtlmHash?.ToHexString());

				log.PrintIf($"  Requestor SID: ", authzData.RequestorSid);

				log.PrintIf($"  Full name: ", authzData.LogonInfo?.FullName);
				log.PrintIf($"  Account flags: ", authzData.LogonInfo?.UserAccountControl);
				log.PrintIf($"  Logon flags: ", authzData.LogonInfo?.UserFlags);
				log.PrintIf($"  Logon count: ", authzData.LogonInfo?.LogonCount);
				log.PrintIf($"  Logon domain SID: ", authzData.LogonInfo?.LogonDomainSid);
				log.PrintIf($"  User ID: ", authzData.LogonInfo?.UserId);
				log.PrintIf($"  User SID: ", authzData.LogonInfo?.UserSid);
				log.PrintIf($"  Kickoff time: ", authzData.LogonInfo?.KickOffTime);
				log.PrintIf($"  Logoff time: ", authzData.LogonInfo?.LogoffTime);
				log.PrintIf($"  Last successful logon: ", authzData.LogonInfo?.LastSuccessfulLogon);
				log.PrintIf($"  Last failed logon: ", authzData.LogonInfo?.LastFailedLogon);
				log.PrintIf($"  Bad password count: ", authzData.LogonInfo?.BadPasswordCount);

				log.PrintIf($"  Password last set: ", authzData.LogonInfo?.PasswordLastSet);
				log.PrintIf($"  Password expires: ", authzData.LogonInfo?.PasswordMustChange);
				log.PrintIf($"  S4U proxy target: ", authzData.S4uProxyTarget);
				if (!authzData.S4uTransitedList.IsNullOrEmpty())
				{
					log.WriteInfo($"  S4U transited services:");
					foreach (var s4uTransit in authzData.S4uTransitedList!)
					{
						log.WriteInfo($"    {s4uTransit}");
					}
				}

				// Groups
				var groups = authzData.GetSecurityGroups();
				log.WriteInfo($"  Security groups: ({groups.Count})");
				foreach (var group in groups)
				{
					var wks = group.Sid.AsWellKnownSid();
					log.WriteInfo($"    {group}");
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	static class LogExtensions
	{
		public static void PrintIf(this ILog log, string? label, string? message)
		{
			if (!string.IsNullOrEmpty(message))
				log.WriteInfo(label + message);
		}
		public static void PrintIf<T>(this ILog log, string? label, T? message)
		{
			if (message != null)
				log.WriteInfo(label + message);
		}
		public static void PrintIf<T>(this ILog log, string? label, T? message)
			where T : struct
		{
			if (message.HasValue)
				log.WriteInfo(label + message);
		}
	}
}
