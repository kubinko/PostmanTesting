using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Interface describing manipulation with people.
    /// </summary>
    public interface IPeopleService
    {
        /// <summary>
        /// Gets person.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Person; <c>null</c>, if not found.</returns>
        GetPersonQuery.Person GetPerson(GetPersonQuery query);

        /// <summary>
        /// Creates person.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Created person ID.</returns>
        Task<long> CreatePerson(CreatePersonCommand command);

        /// <summary>
        /// Updates person.
        /// </summary>
        /// <param name="command">Command.</param>
        Task UpdatePerson(UpdatePersonCommand command);

        /// <summary>
        /// Deletes person.
        /// </summary>
        /// <param name="id">Person ID.</param>
        Task DeletePerson(long id);

        /// <summary>
        /// Deletes all people.
        /// </summary>
        Task DeleteAllPeople();
    }
}
