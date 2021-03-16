using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Domain
{
    /// <summary>
    /// Interface describing repository for workshops.
    /// </summary>
    public interface IWorkshopRepository
    {
        /// <summary>
        /// Creates new workshop.
        /// </summary>
        /// <param name="workshop">Workshop.</param>
        /// <returns>Workshop ID.</returns>
        Task<long> CreateWorkshop(Workshop workshop);

        /// <summary>
        /// Edits workshop.
        /// </summary>
        /// <param name="workshop">Workshop.</param>
        Task EditWorkshop(Workshop workshop);

        /// <summary>
        /// Deletes workshop.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        Task DeleteWorkshop(long id);

        /// <summary>
        /// Deletes all workshops.
        /// </summary>
        Task DeleteAllWorkshops();

        /// <summary>
        /// Increases attendance by 1 for workshop with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        Task IncreaseAttendance(long id);

        /// <summary>
        /// Decreases attendance by 1 for workshop with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        Task DecreaseAttendance(long id);

        /// <summary>
        /// Retrieves workshop with specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        /// <returns>Workshop.</returns>
        Workshop GetWorkshop(long id);

        /// <summary>
        /// Retrieves all workshops.
        /// </summary>
        /// <returns>Workshops.</returns>
        IEnumerable<Workshop> GetAllWorkshops();

        /// <summary>
        /// Retrieves all workshops created by user with <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>Workshops.</returns>
        IEnumerable<Workshop> GetAllUserWorkshops(long userId);
    }
}
