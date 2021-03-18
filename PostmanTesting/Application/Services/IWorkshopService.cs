using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Interface describing manipulation with workshops.
    /// </summary>
    public interface IWorkshopService
    {
        /// <summary>
        /// Gets workshop.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Workshop; <c>null</c>, if not found.</returns>
        GetWorkshopQuery.Workshop GetWorkshop(GetWorkshopQuery query);

        /// <summary>
        /// Gets all workshops.
        /// </summary>
        /// <returns>Collection of workshops.</returns>
        IEnumerable<GetAllWorkshopsQuery.Workshop> GetAllWorkshops();

        /// <summary>
        /// Gets all user's workshops.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Collection of workshops.</returns>
        IEnumerable<GetAllUserWorkshopsQuery.Workshop> GetAllUserWorkshops(GetAllUserWorkshopsQuery query);

        /// <summary>
        /// Creates workshop.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Created workshop ID.</returns>
        Task<long> CreateWorkshop(CreateWorkshopCommand command);

        /// <summary>
        /// Updates workshop.
        /// </summary>
        /// <param name="command">Command.</param>
        Task UpdateWorkshop(UpdateWorkshopCommand command);

        /// <summary>
        /// Deletes workshop.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        Task DeleteWorkshop(long id);

        /// <summary>
        /// Deletes all workshops.
        /// </summary>
        Task DeleteAllWorkshops();
    }
}
