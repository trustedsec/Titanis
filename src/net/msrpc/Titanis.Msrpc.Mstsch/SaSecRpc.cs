namespace SaSecRpc
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), GuidAttribute("378e52b0-c0a9-11cf-822d-00aa0051e40f"), RpcVersionAttribute(1, 0)]
	public partial interface sasec
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SASetAccountInformation(string Handle, string pwszJobName, string pwszAccount, string pwszPassword, uint dwJobFlags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SASetNSAccountInformation(string Handle, string pwszAccount, string pwszPassword, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SAGetNSAccountInformation(string Handle, uint ccBufferSize, char[] wszBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SAGetAccountInformation(string Handle, string pwszJobName, uint ccBufferSize, char[] wszBuffer, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), IidAttribute("378e52b0-c0a9-11cf-822d-00aa0051e40f")]
	public partial class sasecClientProxy : Titanis.DceRpc.Client.RpcClientProxy, sasec, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SASetAccountInformation(string Handle, string pwszJobName, string pwszAccount, string pwszPassword, uint dwJobFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(Handle is null);
			if (Handle is not null)
				encoder.WriteWideCharString(Handle);
			encoder.WriteWideCharString(pwszJobName);
			encoder.WriteWideCharString(pwszAccount);
			encoder.WriteUniqueReferentId(pwszPassword is null);
			if (pwszPassword is not null)
				encoder.WriteWideCharString(pwszPassword);
			encoder.WriteValue(dwJobFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SASetNSAccountInformation(string Handle, string pwszAccount, string pwszPassword, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(Handle is null);
			if (Handle is not null)
				encoder.WriteWideCharString(Handle);
			encoder.WriteUniqueReferentId(pwszAccount is null);
			if (pwszAccount is not null)
				encoder.WriteWideCharString(pwszAccount);
			encoder.WriteUniqueReferentId(pwszPassword is null);
			if (pwszPassword is not null)
				encoder.WriteWideCharString(pwszPassword);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SAGetNSAccountInformation(string Handle, uint ccBufferSize, char[] wszBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(Handle is null);
			if (Handle is not null)
				encoder.WriteWideCharString(Handle);
			encoder.WriteValue(ccBufferSize);
			encoder.WriteArrayHeader(wszBuffer);
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			wszBuffer = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				elem_0 = decoder.ReadWideChar();
				wszBuffer[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SAGetAccountInformation(string Handle, string pwszJobName, uint ccBufferSize, char[] wszBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(Handle is null);
			if (Handle is not null)
				encoder.WriteWideCharString(Handle);
			encoder.WriteWideCharString(pwszJobName);
			encoder.WriteValue(ccBufferSize);
			encoder.WriteArrayHeader(wszBuffer);
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				encoder.WriteValue(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			wszBuffer = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				elem_0 = decoder.ReadWideChar();
				wszBuffer[i] = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(sasec);
		private static Guid _interfaceUuid = new Guid("378e52b0-c0a9-11cf-822d-00aa0051e40f");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial class sasecStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SASetAccountInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string Handle;
			string pwszJobName;
			string pwszAccount;
			string pwszPassword;
			uint dwJobFlags;
			if (decoder.ReadReferentId() == 0)
				Handle = null;
			else
				Handle = decoder.ReadWideCharString();
			pwszJobName = decoder.ReadWideCharString();
			pwszAccount = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				pwszPassword = null;
			else
				pwszPassword = decoder.ReadWideCharString();
			dwJobFlags = decoder.ReadUInt32();
			var invokeTask = this._obj.SASetAccountInformation(Handle, pwszJobName, pwszAccount, pwszPassword, dwJobFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SASetNSAccountInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string Handle;
			string pwszAccount;
			string pwszPassword;
			if (decoder.ReadReferentId() == 0)
				Handle = null;
			else
				Handle = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				pwszAccount = null;
			else
				pwszAccount = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				pwszPassword = null;
			else
				pwszPassword = decoder.ReadWideCharString();
			var invokeTask = this._obj.SASetNSAccountInformation(Handle, pwszAccount, pwszPassword, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SAGetNSAccountInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string Handle;
			uint ccBufferSize;
			char[] wszBuffer;
			if (decoder.ReadReferentId() == 0)
				Handle = null;
			else
				Handle = decoder.ReadWideCharString();
			ccBufferSize = decoder.ReadUInt32();
			wszBuffer = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				elem_0 = decoder.ReadWideChar();
				wszBuffer[i] = elem_0;
			}

			var invokeTask = this._obj.SAGetNSAccountInformation(Handle, ccBufferSize, wszBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(wszBuffer);
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SAGetAccountInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string Handle;
			string pwszJobName;
			uint ccBufferSize;
			char[] wszBuffer;
			if (decoder.ReadReferentId() == 0)
				Handle = null;
			else
				Handle = decoder.ReadWideCharString();
			pwszJobName = decoder.ReadWideCharString();
			ccBufferSize = decoder.ReadUInt32();
			wszBuffer = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				elem_0 = decoder.ReadWideChar();
				wszBuffer[i] = elem_0;
			}

			var invokeTask = this._obj.SAGetAccountInformation(Handle, pwszJobName, ccBufferSize, wszBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(wszBuffer);
			for (int i = 0; i < wszBuffer.Length; i++)
			{
				char elem_0 = wszBuffer[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("378e52b0-c0a9-11cf-822d-00aa0051e40f");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private sasec _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public sasecStub(sasec obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_SASetAccountInformation, this.Invoke_SASetNSAccountInformation, this.Invoke_SAGetNSAccountInformation, this.Invoke_SAGetAccountInformation};
		}
	}
}