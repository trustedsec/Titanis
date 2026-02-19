using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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
		public TicketCache() : this(null)
		{

		}
		/// <summary>
		/// Initializes a new <see cref="TicketCache"/>.
		/// </summary>
		/// <param name="backingTicketList">Ticket list backing the cache</param>
		/// <remarks>
		/// The implementation uses <paramref name="backingTicketList"/> as its backing list.
		/// It must not be read-only.
		/// Callers can provide a backing list to receive notifications when tickets
		/// are added and removed from the cache.
		/// </remarks>
		public TicketCache(IList<TicketInfo>? backingTicketList)
		{
			if (backingTicketList != null && backingTicketList.IsReadOnly)
				throw new ArgumentException($"The ticket list cannot be read-only.", nameof(backingTicketList));

			this._tickets = backingTicketList ?? new List<TicketInfo>();
		}

		public TicketInfo? HomeTgt { get; private set; }
		private IList<TicketInfo> _tickets;

		public TicketInfo[] GetAllTickets() => this._tickets.ToArray();



		/// <inheritdoc/>
		public TicketInfo? GetTicketFromCache(SecurityPrincipalName spn, string? clientName)
		{
			ArgumentNullException.ThrowIfNull(spn);

			var ticket = this._tickets.FirstOrDefault(r => r.IsCurrent && spn.Equals(r.TargetSpn) && (clientName is null || clientName.Equals(r.ClientName, StringComparison.OrdinalIgnoreCase)));
			return ticket;
		}

		/// <summary>
		/// Gets the number of tickets in the cache.
		/// </summary>
		public int TicketCount => this._tickets.Count;

		/// <inheritdoc/>
		public void AddTicket(TicketInfo ticket)
		{
			ArgumentNullException.ThrowIfNull(ticket);

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
