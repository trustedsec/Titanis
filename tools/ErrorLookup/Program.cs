using System.ComponentModel;
using Titanis.Ldap;
using Titanis.Security.Kerberos;
using Titanis.Winterop;

namespace Titanis.Cli.ErrorLookup;

struct ErrorCodeInfo
{
	public ErrorCodeInfo(
		uint code,
		string kind,
		string symbolicName,
		string? message
		)
	{
		this.Code = code;
		this.Kind = kind;
		this.SymbolicName = symbolicName;
		this.Message = message;
	}

	public uint Code { get; }
	public string Kind { get; }
	public string SymbolicName { get; }
	public string? Message { get; }

	public string CodeHex => $"0x{this.Code:X8}";
}

[Command]
[Description("Looks up error codes")]
[OutputRecordType(typeof(ErrorCodeInfo))]
internal class Program : Command
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);

	[Parameter(0)]
	[Mandatory]
	[Description("Error code (decimal, hex, name)")]
	public string[] ErrorCode { get; set; }

	private bool TryGetName(uint value, Type enumType, Func<uint, string?> messageLookup, out ErrorCodeInfo info)
	{
		var obj = Enum.ToObject(enumType, value);
		string name = Enum.GetName(enumType, value);
		if (name != null)
		{
			info = new ErrorCodeInfo(value, enumType.Name, name, messageLookup?.Invoke(value));
			this.WriteRecord(info);
			return true;
		}

		info = new ErrorCodeInfo();
		return false;
	}

	private bool TryFindName(string search, Type enumType, Func<uint, string?> messageLookup)
	{
		var names = Enum.GetNames(enumType);
		names = Array.FindAll(names, r => r.Contains(search, StringComparison.OrdinalIgnoreCase));
		if (names.Length > 0)
		{
			foreach (var name in names)
			{
				var value = (uint)((IConvertible)Enum.Parse(enumType, name)).ToInt64(null);
				var info = new ErrorCodeInfo(value, enumType.Name, name, messageLookup?.Invoke(value));
				this.WriteRecord(info);
			}
			return true;
		}

		return false;
	}

	protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		bool found = false;
		foreach (var code in this.ErrorCode)
		{
			if ((code.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && uint.TryParse(code.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out var value)) || uint.TryParse(code, null, out value))
			{
				ErrorCodeInfo info;
				found |=
					this.TryGetWin32Message(value, out info)
					| TryGetName(value, typeof(Hresult), r => ((Hresult)r).GetException().Message, out info)
					| TryGetName(value, typeof(Ntstatus), r => ((Ntstatus)r).GetException().Message, out info)
					| TryGetName(value, typeof(LdapResultCode), null, out info)
					| TryGetName(value, typeof(KerberosErrorCode), null, out info)
					;
			}
			else
			{
				found |=
					TryFindName(code, typeof(Win32ErrorCode), r => ((Win32ErrorCode)r).GetException().Message)
					| TryFindName(code, typeof(Hresult), r => ((Hresult)r).GetException().Message)
					| TryFindName(code, typeof(Ntstatus), r => ((Ntstatus)r).GetException().Message)
					| TryFindName(code, typeof(LdapResultCode), null)
					| TryFindName(code, typeof(KerberosErrorCode), null)
					;

			}
		}
		return Task.FromResult(found ? 0 : 1);
	}

	private bool TryGetWin32Message(uint value, out ErrorCodeInfo info)
	{
		if ((value & 0xFFFF_0000) == 0x8007_0000)
			value &= 0xFFFF;
		return TryGetName(value, typeof(Win32ErrorCode), r => ((Win32ErrorCode)r).GetException().Message, out info);
	}
}
