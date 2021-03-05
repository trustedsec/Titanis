namespace Titanis.Security.Ntlm
{
	public interface INtlmAuthStore
	{
		NtlmAuthRecord GetUserAuthRecord(string userName);
	}
}