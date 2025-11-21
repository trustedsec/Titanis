using Lightweight_Directory_Access_Protocol_V3;

namespace Titanis.Ldap
{
	class LdapResponse
	{
		internal ILdapChannelSearchCallback? searchCallback;

		internal LDAPMessage? message;
		internal TaskCompletionSource<int> taskSource = new TaskCompletionSource<int>();
		internal CancellationTokenRegistration cancelReg;
	}
}
