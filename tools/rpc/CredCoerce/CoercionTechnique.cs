using System.Net.Sockets;
using System.Threading;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;
using Titanis.Net;
using Titanis.Smb2;

namespace Titanis.CredCoerce
{
	abstract class CoercionTechnique
	{
		public CoercionTechnique()
		{
		}

		public abstract Task Execute(CoercionContext context, CancellationToken cancellationToken);
	}

	class CoercionContext
	{
		internal CoercionContext(
			Program command,
			IClientCredentialService credService,
			Smb2Client smbClient,
			RpcClient rpcClient,
			EpmClient epmClient,
			ILog log
			)
		{
			Command = command;
			this.CredService = credService;
			this.SmbClient = smbClient;
			this.RpcClient = rpcClient;
			this.EpmClient = epmClient;
			this.Log = log;
		}

		public Program Command { get; }
		public string ServerName => this.Command.ServerName;
		public string VictimPath => this.Command.VictimPath;
		public IClientCredentialService CredService { get; }
		public Smb2Client SmbClient { get; }
		public RpcClient RpcClient { get; }
		public EpmClient EpmClient { get; }
		public ILog Log { get; }

		private Dictionary<Type, object> _rpcServices = new Dictionary<Type, object>();
		public T? TryGetService<T>()
			where T : class
		{
			this._rpcServices.TryGetValue(typeof(T), out var service);
			return (T)service;
		}
		public void AddService<T>(T service)
			where T : RpcServiceClient
		{
			this._rpcServices.Add(typeof(T), service);
		}
	}
}
