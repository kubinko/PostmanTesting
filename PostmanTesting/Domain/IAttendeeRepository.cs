using System.Threading.Tasks;

namespace PostmanTesting.Domain
{
    /// <summary>
    /// Interface describing repository for attendees.
    /// </summary>
    public interface IAttendeeRepository
    {
        /// <summary>
        /// Creates new attendee.
        /// </summary>
        /// <param name="attendee">Attendee.</param>
        /// <returns>Attendee ID.</returns>
        Task<long> AddAttendee(Attendee attendee);

        /// <summary>
        /// Edits attendee.
        /// </summary>
        /// <param name="attendee">Attendee.</param>
        Task EditAttendee(Attendee attendee);

        /// <summary>
        /// Deletes attendee.
        /// </summary>
        /// <param name="id">Attendee ID.</param>
        Task DeleteAttendee(long id);

        /// <summary>
        /// Deletes all attendees.
        /// </summary>
        Task DeleteAllAttendees();

        /// <summary>
        /// Retrieves attendee with specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Attendee ID.</param>
        /// <returns>Attendee.</returns>
        Attendee GetAttendee(long id);
    }
}
