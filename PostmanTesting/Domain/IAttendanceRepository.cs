using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Domain
{
    /// <summary>
    /// Interface describing repository for attendance.
    /// </summary>
    public interface IAttendanceRepository
    {
        /// <summary>
        /// Adds person to workshop.
        /// </summary>
        /// <param name="workshopId">Workshop ID.</param>
        /// <param name="personId">Person ID.</param>
        /// <returns>New record ID.</returns>
        Task<long> AddPersonToWorkshop(long workshopId, long personId);

        /// <summary>
        /// Removes person from workshop.
        /// </summary>
        /// <param name="workshopId">Workshop ID.</param>
        /// <param name="personId">Person ID.</param>
        Task RemovePersonFromWorkshop(long workshopId, long personId);

        /// <summary>
        /// Checks whether invoice for attendace was generated.
        /// </summary>
        /// <param name="workshopId">Workshop ID.</param>
        /// <param name="personId">Person ID.</param>
        /// <returns><c>True</c>, if invoice was generated; otherwise <c>false</c>.</returns>
        bool CheckInvoiceGenerated(long workshopId, long personId);

        /// <summary>
        /// Gets all workshop attendees.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        /// <returns>Workshop attendees.</returns>
        IEnumerable<Person> GetWorkshopAttendees(long id);

        /// <summary>
        /// Gets all person workshops.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <returns>Person workshops.</returns>
        IEnumerable<Workshop> GetPersonWorkshops(long id);
    }
}
