using Kros.KORM;
using Kros.Utils;
using Mapster;
using PostmanTesting.Domain;
using System.Linq;
using System.Threading.Tasks;
using InfrastructureAttendee = PostmanTesting.Infrastructure.Entities.Attendee;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// Attendee repository.
    /// </summary>
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">KORM database accessor.</param>
        public AttendeeRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc/>
        public async Task<long> AddAttendee(Attendee attendee)
        {
            var infraAttendee = attendee.Adapt<InfrastructureAttendee>();
            await _database.AddAsync(infraAttendee);

            return infraAttendee.Id;
        }

        /// <inheritdoc/>
        public Task EditAttendee(Attendee attendee)
        {
            var infraAttendee = attendee.Adapt<InfrastructureAttendee>();
            return _database.EditAsync(infraAttendee, default, nameof(InfrastructureAttendee.Id),
                nameof(InfrastructureAttendee.Name), nameof(InfrastructureAttendee.Surname),
                nameof(InfrastructureAttendee.Street), nameof(InfrastructureAttendee.PostCode),
                nameof(InfrastructureAttendee.City), nameof(InfrastructureAttendee.Country),
                nameof(InfrastructureAttendee.PhoneNumber), nameof(InfrastructureAttendee.Email),
                nameof(InfrastructureAttendee.LastModifiedBy), nameof(InfrastructureAttendee.LastModifiedTimestamp));
        }

        /// <inheritdoc/>
        public Task DeleteAttendee(long id)
            => _database.DeleteAsync<InfrastructureAttendee>(id);

        /// <inheritdoc/>
        public Task DeleteAllAttendees()
            => _database.DeleteAsync<InfrastructureAttendee>("1=1");

        /// <inheritdoc/>
        public Attendee GetAttendee(long id)
        {
            var attendee = _database.Query<InfrastructureAttendee>().FirstOrDefault(a => a.Id == id);
            return attendee.Adapt<Attendee>();
        }
    }
}
