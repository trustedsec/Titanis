using System;
using System.Collections.Generic;

namespace Titanis.Security.Kerberos
{

	record struct TicketKey
	{
		public TicketKey(ServicePrincipalName spn)
		{
			this.Spn = spn;
		}

		public ServicePrincipalName Spn { get; }
	}

	/// <summary>
	/// Implements <see cref="ITicketCache"/> as a simple dictionary.
	/// </summary>
	public class TicketCache : ITicketCache
	{
		/// <summary>
		/// Initializes a new <see cref="TicketCache"/>.
		/// </summary>
		public TicketCache()
		{

		}

		private Dictionary<TicketKey, TicketInfo> _tickets = new Dictionary<TicketKey, TicketInfo>();

		/// <inheritdoc/>
		public TicketInfo? GetTicketFromCache(ServicePrincipalName spn)
		{
			ArgumentNullException.ThrowIfNull(spn);

			this._tickets.TryGetValue(new TicketKey(spn), out var ticket);
			return ticket;
		}

		/// <inheritdoc/>
		public void AddTicket(ServicePrincipalName spn, TicketInfo ticket)
		{
			this._tickets[new TicketKey(spn)] = ticket;
		}

		public TicketInfo Import(byte[] ticketBytes, byte[] encryptionKey)
		{
			ArgumentNullException.ThrowIfNull(ticketBytes);
			ArgumentNullException.ThrowIfNull(encryptionKey);

			throw new NotImplementedException();
		}
	}
}
