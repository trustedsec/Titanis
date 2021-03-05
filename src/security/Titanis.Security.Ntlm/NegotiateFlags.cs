using System;

namespace Titanis.Security.Ntlm
{
	[Flags]
	public enum NegotiateFlags : uint
	{
		W_Negotiate56 = (1U << 31),
		V_NegotiateKeyExchange = (1U << 30),
		U_Negotiate128 = (1U << 29),
		r1 = (1U << 28),
		r2 = (1U << 27),
		r3 = (1U << 26),
		T_NegotiateVersion = (1U << 25),
		r4 = (1U << 24),
		S_NegotiateTargetInfo = (1U << 23),
		R_RequestNonNTSessionKey = (1U << 22),
		r5 = (1U << 21),
		Q_NegotiateIdentify = (1U << 20),
		P_NegotiateExtendedSessionSecurity = (1U << 19),
		r6 = (1U << 18),
		O_TargetTypeServer = (1U << 17),
		N_TargetTypeDomain = (1U << 16),
		// TODO: Implement NTLM AUTH signing
		M_NegotiateAlwaysSign = (1U << 15),
		r7 = (1U << 14),
		L_NegotiateOemWorkstationSupplied = (1U << 13),
		K_NegotiateOemDomainSupplied = (1U << 12),
		J_Anonymous = (1U << 11),
		r8 = (1U << 10),
		H_NegotiateNtlm = (1U << 9),
		r9 = (1U << 8),
		G_NegotiateLMKey = (1U << 7),
		F_NegotiateDatagram = (1U << 6),
		E_NegotiateSeal = (1U << 5),
		D_NegotiateSign = (1U << 4),
		r10 = (1U << 3),
		C_RequestTarget = (1U << 2),
		B_NegotiateOem = (1U << 1),
		A_NegotiateUnicode = (1U << 0),

		TargetsMask = O_TargetTypeServer | N_TargetTypeDomain,
		FeatureMask =
			V_NegotiateKeyExchange
			| T_NegotiateVersion | R_RequestNonNTSessionKey | Q_NegotiateIdentify
			| P_NegotiateExtendedSessionSecurity | M_NegotiateAlwaysSign
			| F_NegotiateDatagram | E_NegotiateSeal | D_NegotiateSign | C_RequestTarget
			| G_NegotiateLMKey | H_NegotiateNtlm
	}
}
