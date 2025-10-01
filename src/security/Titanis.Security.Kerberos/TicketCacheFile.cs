using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	public class TicketCacheFile : TicketCache
	{
		public TicketCacheFile(
			string fileName,
			KerberosClient krb
			)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);
			ArgumentNullException.ThrowIfNull(krb);

			this._fileName = fileName;
			this._krb = krb;

			if (File.Exists(fileName))
			{
				var info = new FileInfo(fileName);
				this._modDate = info.LastWriteTimeUtc;
				byte[] bytes = File.ReadAllBytes(fileName);
				var tickets = krb.LoadTicketsFromFile(bytes, out var format);
				this._format = format;

				foreach (var ticket in tickets)
				{
					this.AddTicket(ticket);
				}
			}
			else
			{
				this._format = KerberosClient.GetFormatFromFileName(fileName);
			}

			this._loaded = true;
		}

		private readonly string _fileName;
		private readonly KerberosClient _krb;
		private DateTime _modDate;
		private readonly KerberosFileFormat _format;
		private readonly bool _loaded;

		protected sealed override void OnTicketAdded(SecurityPrincipalName spn, TicketInfo ticket)
		{
			base.OnTicketAdded(spn, ticket);
			if (_loaded)
				this.SaveToFile();
		}

		private void SaveToFile()
		{
			if (this._modDate != default)
			{
				var info = new FileInfo(this._fileName);
				if (info.LastWriteTimeUtc != this._modDate)
					return;
			}

			var bytes = this._krb.ExportTickets(this.GetAllTickets(), this._format);
			File.WriteAllBytes(this._fileName, bytes);
			this._modDate = new FileInfo(this._fileName).LastWriteTimeUtc;
		}
	}
}
