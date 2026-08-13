using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.Security.Sspi
{
	[StructLayout(LayoutKind.Sequential)]
	struct SEC_CHANNEL_BINDINGS
	{
		internal int dwInitiatorAddrType;
		internal int cbInitiatorLength;
		internal int dwInitiatorOffset;
		internal int dwAcceptorAddrType;
		internal int cbAcceptorLength;
		internal int dwAcceptorOffset;
		internal int cbApplicationDataLength;
		internal int dwApplicationDataOffset;
	}

	enum SspiSecBufferType : uint
	{
		EMPTY = 0,
		DATA = 1,
		TOKEN = 2,
		PKG_PARAMS = 3,
		MISSING = 4,
		EXTRA = 5,
		STREAM_TRAILER = 6,
		STREAM_HEADER = 7,
		NEGOTIATION_INFO = 8,
		PADDING = 9,
		STREAM = 10,
		MECHLIST = 11,
		MECHLIST_SIGNATURE = 12,
		TARGET = 13,
		CHANNEL_BINDINGS = 14,
		CHANGE_PASS_RESPONSE = 15,
		TARGET_HOST = 16,
		ALERT = 17,
		APPLICATION_PROTOCOLS = 18,
		SRTP_PROTECTION_PROFILES = 19,
		SRTP_MASTER_KEY_IDENTIFIER = 20,
		TOKEN_BINDING = 21,
		PRESHARED_KEY = 22,
		PRESHARED_KEY_IDENTITY = 23,
		DTLS_MTU = 24,
		SEND_GENERIC_TLS_EXTENSION = 25,
		SUBSCRIBE_GENERIC_TLS_EXTENSION = 26,
		FLAGS = 27,
		TRAFFIC_SECRETS = 28,
		CERTIFICATE_REQUEST_CONTEXT = 29,
		CHANNEL_BINDINGS_RESULT = 30,
		APP_SESSION_STATE = 31,
		SESSION_TICKET = 32,

		ATTRMASK = 0xF0000000,
		READONLY = 0x80000000,
		READONLY_WITH_CHECKSUM = 0x10000000,
		RESERVED = 0x60000000,
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SspiSecBuffer
	{
		public SspiSecBuffer(int cb, SspiSecBufferType type, IntPtr bytes)
		{
			this.cbBuffer = cb;
			this.BufferType = type;
			this.pvBuffer = bytes;
		}
		internal int cbBuffer;
		internal SspiSecBufferType BufferType;
		internal IntPtr /* byte[] */ pvBuffer;
	}


	struct SecHandle
	{
		internal nuint dwLower;
		internal nuint dwUpper;
	}

	enum SspiSecBufferVersion : uint
	{
		SECBUFFER_VERSION = 0
	}

	struct Buf
	{

	}

	[StructLayout(LayoutKind.Sequential)]
	ref struct SspiSecBufferDesc
	{
		public SspiSecBufferDesc(int bufferCount, IntPtr pBuffers)
		{
			this.ulVersion = SspiSecBufferVersion.SECBUFFER_VERSION;
			this.cBuffers = bufferCount;
			this.pBuffers = pBuffers;
		}

		internal SspiSecBufferVersion ulVersion;
		internal int cBuffers;
		internal IntPtr /* SspiSecBuffer[] */ pBuffers;
	}

	enum SspiDrep : uint
	{
		SECURITY_NATIVE_DREP = 0x00000010
	}

	internal class NativeMethods
	{
		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "AcquireCredentialsHandleW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows")]
		internal static extern Hresult AcquireCredentialsHandle(
			[MarshalAs(UnmanagedType.LPWStr)] string? pszPrincipal,
			[MarshalAs(UnmanagedType.LPWStr)] string pszPackage,
			SECPKG_CRED fCredentialUse,
			IntPtr pvLogonId,
			IntPtr pAuthData,
			[MarshalAs(UnmanagedType.FunctionPtr)] SEC_GET_KEY_FN? pGetKeyFn,
			IntPtr pvGetKeyArgument,
			out SecHandle phCredential,
			out long ptsExpiry);

		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "InitializeSecurityContextW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows")]
		internal static extern Hresult InitializeSecurityContext_Test(
			ref SecHandle phCredential,
			IntPtr phContext,
			[MarshalAs( UnmanagedType.LPWStr)]
			string? pszTargetName,
			SspiCaps fContextReq,
			uint Reserved1,
			SspiDrep TargetDataRep,
			ref SspiSecBufferDesc pInput,
			IntPtr Reserved2,
			IntPtr phNewContext,
			IntPtr pOutput,
			IntPtr pfContextAttr,
			IntPtr ptsExpiry
			);

		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "InitializeSecurityContextW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows")]
		internal static extern Hresult InitializeSecurityContext(
			in SecHandle phCredential,
			IntPtr phContext,
			[MarshalAs( UnmanagedType.LPWStr)]
			string? pszTargetName,
			SspiCaps fContextReq,
			uint Reserved1,
			SspiDrep TargetDataRep,
			ref SspiSecBufferDesc pInput,
			uint Reserved2,
			out SecHandle phNewContext,
			ref SspiSecBufferDesc pOutput,
			out SspiCaps pfContextAttr,
			out long ptsExpiry
			);

		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "InitializeSecurityContextW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[SupportedOSPlatform("windows")]
		internal static extern Hresult InitializeSecurityContext(
			in SecHandle phCredential,
			ref SecHandle phContext,
			[MarshalAs( UnmanagedType.LPWStr)]
			string? pszTargetName,
			SspiCaps fContextReq,
			uint Reserved1,
			SspiDrep TargetDataRep,
			ref SspiSecBufferDesc pInput,
			uint Reserved2,
			out SecHandle phNewContext,
			ref SspiSecBufferDesc pOutput,
			out SspiCaps pfContextAttr,
			out long ptsExpiry
			);

		[DllImport("SECUR32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern Hresult FreeContextBuffer(IntPtr pvContextBuffer);



		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "QueryContextAttributesW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult QueryContextAttributes(
			ref SecHandle phContext,
			SECPKG_ATTR ulAttribute,
			out SecPkgContext_SessionKey pBuffer);
		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "QueryContextAttributesW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult QueryContextAttributes(
			ref SecHandle phContext,
			SECPKG_ATTR ulAttribute,
			out SecPkgContext_Sizes pBuffer);
		[DllImport("SECUR32.dll", ExactSpelling = true, EntryPoint = "QueryContextAttributesW"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult QueryContextAttributes(
			ref SecHandle phContext,
			SECPKG_ATTR ulAttribute,
			out SecPkgContext_StreamSizes pBuffer);

		[DllImport("SECUR32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult EncryptMessage(ref SecHandle phContext, uint fQOP, ref SspiSecBufferDesc pMessage, int MessageSeqNo);

		[DllImport("SECUR32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult DecryptMessage(
			ref SecHandle phContext,
			ref SspiSecBufferDesc pMessage,
			int MessageSeqNo,
			out uint fQOP);

		[DllImport("SECUR32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult MakeSignature(
			ref SecHandle phContext,
			uint fQOP,
			ref SspiSecBufferDesc pMessage,
			int MessageSeqNo);
		[DllImport("SECUR32.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		internal static extern unsafe Hresult VerifySignature(
			ref SecHandle phContext,
			ref SspiSecBufferDesc pMessage,
			int MessageSeqNo,
			out uint fQOP
			);
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SecPkgContext_Sizes
	{
		internal int cbMaxToken;
		internal int cbMaxSignature;
		internal int cbBlockSize;
		internal int cbSecurityTrailer;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SecPkgContext_StreamSizes
	{
		internal int cbHeader;
		internal int cbTrailer;
		internal int cbMaximumMessage;
		internal int cBuffers;
		internal int cbBlockSize;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SecPkgContext_SessionKey
	{
		internal int SessionKeyLength;
		internal IntPtr SessionKey;
	}
	internal enum SECPKG_CRED : uint
	{
		SECPKG_CRED_INBOUND = 1U,
		SECPKG_CRED_OUTBOUND = 2U,
	}

	[UnmanagedFunctionPointerAttribute(CallingConvention.Winapi)]
	internal unsafe delegate void SEC_GET_KEY_FN(void* Arg, void* Principal, uint KeyVer, void** Key, out Hresult Status);
	internal enum SECPKG_ATTR : uint
	{
		SECPKG_ATTR_C_ACCESS_TOKEN = 2147483666U,
		SECPKG_ATTR_C_FULL_ACCESS_TOKEN = 2147483778U,
		SECPKG_ATTR_CERT_TRUST_STATUS = 2147483780U,
		SECPKG_ATTR_CREDS = 2147483776U,
		SECPKG_ATTR_CREDS_2 = 2147483782U,
		SECPKG_ATTR_NEGOTIATION_PACKAGE = 2147483777U,
		SECPKG_ATTR_PACKAGE_INFO = 10U,
		SECPKG_ATTR_SERVER_AUTH_FLAGS = 2147483779U,
		SECPKG_ATTR_SIZES = 0U,
		SECPKG_ATTR_SUBJECT_SECURITY_ATTRIBUTES = 124U,
		SECPKG_ATTR_APP_DATA = 94U,
		SECPKG_ATTR_EAP_PRF_INFO = 101U,
		SECPKG_ATTR_EARLY_START = 105U,
		SECPKG_ATTR_DTLS_MTU = 34U,
		SECPKG_ATTR_KEYING_MATERIAL_INFO = 106U,
		SECPKG_ATTR_ACCESS_TOKEN = 18U,
		SECPKG_ATTR_AUTHORITY = 6U,
		SECPKG_ATTR_CLIENT_SPECIFIED_TARGET = 27U,
		SECPKG_ATTR_CONNECTION_INFO = 90U,
		SECPKG_ATTR_DCE_INFO = 3U,
		SECPKG_ATTR_ENDPOINT_BINDINGS = 26U,
		SECPKG_ATTR_EAP_KEY_BLOCK = 91U,
		SECPKG_ATTR_FLAGS = 14U,
		SECPKG_ATTR_ISSUER_LIST_EX = 89U,
		SECPKG_ATTR_KEY_INFO = 5U,
		SECPKG_ATTR_LAST_CLIENT_TOKEN_STATUS = 30U,
		SECPKG_ATTR_LIFESPAN = 2U,
		SECPKG_ATTR_LOCAL_CERT_CONTEXT = 84U,
		SECPKG_ATTR_LOCAL_CRED = 82U,
		SECPKG_ATTR_NAMES = 1U,
		SECPKG_ATTR_NATIVE_NAMES = 13U,
		SECPKG_ATTR_NEGOTIATION_INFO = 12U,
		SECPKG_ATTR_PASSWORD_EXPIRY = 8U,
		SECPKG_ATTR_REMOTE_CERT_CONTEXT = 83U,
		SECPKG_ATTR_ROOT_STORE = 85U,
		SECPKG_ATTR_SESSION_KEY = 9U,
		SECPKG_ATTR_SESSION_INFO = 93U,
		SECPKG_ATTR_STREAM_SIZES = 4U,
		SECPKG_ATTR_SUPPORTED_SIGNATURES = 102U,
		SECPKG_ATTR_TARGET_INFORMATION = 17U,
		SECPKG_ATTR_UNIQUE_BINDINGS = 25U,
	}
}
