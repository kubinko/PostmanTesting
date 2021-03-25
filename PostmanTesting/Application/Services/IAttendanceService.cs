using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Interface describing manipulation with attendance.
    /// </summary>
    public interface IAttendanceService
    {
        /// <summary>
        /// Gets all workshop attendees.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Workshop attendees.</returns>
        IEnumerable<GetWorkshopAttendeesQuery.Attendee> GetWorkshopAttendees(GetWorkshopAttendeesQuery query);

        /// <summary>
        /// Gets all person workshops.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Person workshops.</returns>
        IEnumerable<GetPersonWorkshops.Workshop> GetPersonWorkshops(GetPersonWorkshops query);

        /// <summary>
        /// Checks whether invoice was generated for attendee's attendance at workshop.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns><c>True</c>, if invoice was generated; otherwise <c>false</c>.</returns>
        bool CheckInvoiceGenerated(CheckInvoiceGeneratedQuery query);

        /// <summary>
        /// Adds person to workshop.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Created record ID.</returns>
        Task<long> AddPersonToWorkshop(AddPersonToWorkshopCommand command);

        /// <summary>
        /// Removes person from workshop.
        /// </summary>
        /// <param name="command">Command.</param>
        Task RemovePersonFromWorkshop(RemovePersonFromWorkshopCommand command);
    }
}
