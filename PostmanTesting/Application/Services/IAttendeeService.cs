using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Interface describing manipulation with attendees.
    /// </summary>
    public interface IAttendeeService
    {
        /// <summary>
        /// Gets attendee.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Attendee; <c>null</c>, if not found.</returns>
        GetAttendeeQuery.Attendee GetAttendee(GetAttendeeQuery query);

        /// <summary>
        /// Creates attendee.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Created attendee ID.</returns>
        Task<long> CreateAttendee(CreateAttendeeCommand command);

        /// <summary>
        /// Updates attendee.
        /// </summary>
        /// <param name="command">Command.</param>
        Task UpdateAttendee(UpdateAttendeeCommand command);

        /// <summary>
        /// Deletes attendee.
        /// </summary>
        /// <param name="id">Attendee ID.</param>
        Task DeleteAttendee(long id);

        /// <summary>
        /// Deletes all attendees.
        /// </summary>
        Task DeleteAllAttendees();
    }
}
