using System.Threading.Tasks;

namespace PostmanTesting.Domain
{
    /// <summary>
    /// Interface describing repository for people.
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// Creates new person.
        /// </summary>
        /// <param name="person">Person.</param>
        /// <returns>Person ID.</returns>
        Task<long> AddPerson(Person person);

        /// <summary>
        /// Edits person.
        /// </summary>
        /// <param name="person">Person.</param>
        Task EditPerson(Person person);

        /// <summary>
        /// Deletes person.
        /// </summary>
        /// <param name="id">Person ID.</param>
        Task DeletePerson(long id);

        /// <summary>
        /// Deletes all people.
        /// </summary>
        Task DeleteAllPeople();

        /// <summary>
        /// Retrieves person with specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <returns>Person.</returns>
        Person GetPerson(long id);
    }
}
