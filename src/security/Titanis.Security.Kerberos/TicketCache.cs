using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Titanis.Security.Kerberos
{

	public record struct TicketKey
	{
		public TicketKey(SecurityPrincipalName spn)
		{
			this.Spn = spn;
		}

		public SecurityPrincipalName Spn { get; }
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

		public TicketInfo? HomeTgt { get; private set; }
		private List<TicketInfo> _tickets = new List<TicketInfo>();
		private ConcurrentDictionary<SecurityPrincipalName, TicketInfo> _ticketsBySpn = new ConcurrentDictionary<SecurityPrincipalName, TicketInfo>();

		public TicketInfo[] GetAllTickets() => this._tickets.ToArray();

		/// <inheritdoc/>
		public TicketInfo? GetTicketFromCache(SecurityPrincipalName spn)
		{
			ArgumentNullException.ThrowIfNull(spn);

			if (this._ticketsBySpn.TryGetValue(spn, out var ticket))
			{
				if (ticket.IsCurrent)
					return ticket;
			}

			return null;
		}

		/// <summary>
		/// Gets the number of tickets in the cache.
		/// </summary>
		public int TicketCount => this._tickets.Count;

		/// <inheritdoc/>
		public void AddTicket(TicketInfo ticket)
		{
			ArgumentNullException.ThrowIfNull(ticket);

			if (ticket.IsCurrent)
				this._ticketsBySpn[ticket.TargetSpn] = ticket;
			this._tickets.Add(ticket);
			if (
				ticket.IsTgt
				&& string.Equals(ticket.TicketRealm, ticket.ServiceInstance, StringComparison.OrdinalIgnoreCase)
				)
				this.HomeTgt = ticket;

			this.OnTicketAdded(ticket.TargetSpn, ticket);
		}

		protected virtual void OnTicketAdded(SecurityPrincipalName spn, TicketInfo ticket)
		{
		}
	}
}
