using System.Net;
using System.Net.Sockets;
using System.Threading;
using Titanis.IO;

namespace Titanis.Ssh
{
	public class SshClient
	{
		public const int Port = 22;

		public async Task<SshConnection> ConnectTo(IPEndPoint remoteEP, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(remoteEP);

			Socket? s = new Socket(remoteEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				await s.ConnectAsync(remoteEP, cancellationToken).ConfigureAwait(false);
				var stream = new NetworkStream(s, true);
				s = null;
				try
				{
					ByteStreamReader reader = new ByteStreamReader(stream, false);

					ByteWriter writer = new ByteWriter();
					VersionString myver = new VersionString(new Version(2, 0), "Titanis.SSH", null);
					writer.WritePduStruct(myver);

					await stream.WriteAsync(writer.GetData(), cancellationToken).ConfigureAwait(false);

					var sshver = reader.ReadPduStruct<VersionString>();

					SshConnection conn = new SshConnection(stream);
					stream = null;

					conn.Start();

					return conn;
				}
				finally
				{
					stream?.Dispose();
				}
			}
			finally
			{
				s?.Dispose();
			}

			throw new NotImplementedException();
		}
	}
}
