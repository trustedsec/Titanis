namespace Titanis.Security.Kerberos
{
	enum PacBufferType
	{
		LogonInfo = 1,
		CredentialInfo = 2,
		ServerChecksum = 6,
		KdcChecksum = 7,
		ClientNameInfo = 0x0A,
		ConstrainedDelegationInfo = 11,
		UserPrincipalName = 12,
		ClientClaims = 13,
		DeviceInfo = 14,
		DeviceClaims = 15,
		TicketChecksum = 16,
		PacAttributes = 17,
		PacRequestorSid = 18,
		ExtendedKdcChecksum = 19,
		PacRequestorGuid = 20,
	}
}