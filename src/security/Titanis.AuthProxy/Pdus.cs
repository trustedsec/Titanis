using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.AuthProxy
{
	using ULONG = uint;
	using UINT = uint;
	using USHORT = ushort;
	using AuthSessionId = int;
	using TimeStamp = long;

	enum AuthProxyMessageType
	{
		Error = 1,
		AcquireCreds,
		InitContext,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AuthProxyRequestHeader
	{
		internal static unsafe int StructSize => sizeof(AuthProxyRequestHeader);

		internal int cbMessage;
		internal AuthProxyMessageType messageType;
	}

	enum AuthPackageType
	{
		Custom = 0,
		Ntlm,
		Kerberos,
		Negotiate
	};

	enum SspiCaps
	{
		DceStyle = 0x200,
		Integrity = 0x00010000,
		ExtendedError = 0x00004000,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AcquireCredsRequest
	{
		internal static unsafe int StructSize => sizeof(AcquireCredsRequest);

		internal SspiCaps requiredCaps;
		internal AuthPackageType package;

		internal USHORT offPackageName;
		internal USHORT offPrincipalName;
		internal USHORT offTargetSpn;
		internal USHORT cbAuthData;
		internal USHORT offAuthData;
		internal USHORT cbAuthToken;
		internal USHORT offAuthToken;
		internal USHORT cbChannelBinding;
		internal USHORT offChannelBinding;
	};


	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AcquireCredsReply
	{
		internal static unsafe int StructSize => sizeof(AcquireCredsReply);

		internal AuthSessionId sessionId;
		internal TimeStamp tsCredExpiry;

		internal InitializeContextReply initContext;
	};

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct InitializeContextRequest
	{
		internal static unsafe int StructSize => sizeof(InitializeContextRequest);

		internal int authSessionId;
		internal int cbToken;
		internal int offToken;
	};

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct InitializeContextReply
	{
		internal static unsafe int StructSize => sizeof(InitializeContextReply);

		internal int flags;
		internal TimeStamp tsContextExpiry;
		internal int maxTokenSize;
		internal int maxSignatureSize;
		internal int blockSize;
		internal int cbTrailer;

		internal int cbToken;
		internal int offToken;
		internal int cbSessionKey;
		internal int offSessionKey;
	};


	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AuthProxyReplyHeader
	{
		internal static unsafe int StructSize => sizeof(AuthProxyReplyHeader);

		internal uint cbMessage;
		internal Hresult status;
	}
}
