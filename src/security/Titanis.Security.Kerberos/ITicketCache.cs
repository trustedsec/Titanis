using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Provides functionality as a cache for Kerberos tickets.
	/// </summary>
	/// <seealso cref="TicketCache"/>
	public interface ITicketCache
	{
		/// <summary>
		/// Adds a ticket to the cache.
		/// </summary>
		/// <param name="ticket">Ticket</param>
		void AddTicket(TicketInfo ticket);

		/// <summary>
		/// Gets a ticket for the specified realm and service.
		/// </summary>
		/// <param name="spn">SPN on ticket</param>
		/// <returns>A <see cref="TicketInfo"/> containing the requested ticket,
		/// if found; otherwise, <see langword="null"/>.</returns>
		TicketInfo? GetTicketFromCache(SecurityPrincipalName spn);

		
	}
}
