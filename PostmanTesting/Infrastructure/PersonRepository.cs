using Kros.KORM;
using Kros.Utils;
using Mapster;
using PostmanTesting.Domain;
using System.Linq;
using System.Threading.Tasks;
using InfrastructurePerson = PostmanTesting.Infrastructure.Entities.Person;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// People repository.
    /// </summary>
    public class PersonRepository : IPeopleRepository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">KORM database accessor.</param>
        public PersonRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc/>
        public async Task<long> AddPerson(Person person)
        {
            var infraPerson = person.Adapt<InfrastructurePerson>();
            await _database.AddAsync(infraPerson);

            return infraPerson.Id;
        }

        /// <inheritdoc/>
        public Task EditPerson(Person person)
        {
            var infraPerson = person.Adapt<InfrastructurePerson>();
            return _database.EditAsync(infraPerson, default, nameof(InfrastructurePerson.Id),
                nameof(InfrastructurePerson.Name), nameof(InfrastructurePerson.Surname),
                nameof(InfrastructurePerson.Street), nameof(InfrastructurePerson.PostCode),
                nameof(InfrastructurePerson.City), nameof(InfrastructurePerson.Country),
                nameof(InfrastructurePerson.PhoneNumber), nameof(InfrastructurePerson.Email),
                nameof(InfrastructurePerson.LastModifiedBy), nameof(InfrastructurePerson.LastModifiedTimestamp));
        }

        /// <inheritdoc/>
        public Task DeletePerson(long id)
            => _database.DeleteAsync<InfrastructurePerson>(id);

        /// <inheritdoc/>
        public Task DeleteAllPeople()
            => _database.DeleteAsync<InfrastructurePerson>("1=1");

        /// <inheritdoc/>
        public Person GetPerson(long id)
        {
            var person = _database.Query<InfrastructurePerson>().FirstOrDefault(a => a.Id == id);
            return person.Adapt<Person>();
        }
    }
}
