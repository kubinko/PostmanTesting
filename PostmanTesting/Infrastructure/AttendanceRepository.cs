using Kros.KORM;
using Kros.Utils;
using Mapster;
using PostmanTesting.Domain;
using PostmanTesting.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// Attendance repository.
    /// </summary>
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">KORM database accessor.</param>
        public AttendanceRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc/>
        public async Task<long> AddPersonToWorkshop(long workshopId, long personId)
        {
            var attendance = new Attendance() { WorkshopId = workshopId, PersonId = personId };
            await _database.AddAsync(attendance);

            return attendance.Id;
        }

        /// <inheritdoc/>
        public Task RemovePersonFromWorkshop(long workshopId, long personId)
            => _database.DeleteAsync<Attendance>(a => a.WorkshopId == workshopId && a.PersonId == personId);

        /// <inheritdoc/>
        public IEnumerable<Domain.Person> GetWorkshopAttendees(long id)
        {
            var attendance = _database.Query<Attendance>().Where(a => a.WorkshopId == id).ToArray();
            var people = _database.Query<Entities.Person>()
                .Where($"ID IN (@1)", attendance.Any() ? string.Join(',', attendance.Select(a => a.PersonId)) : "-1")
                .ToArray();

            return people.Select(p => p.Adapt<Domain.Person>());
        }

        /// <inheritdoc/>
        public IEnumerable<Domain.Workshop> GetPersonWorkshops(long id)
        {
            var attendance = _database.Query<Attendance>().Where(a => a.WorkshopId == id).ToArray();
            var workshops = _database.Query<Entities.Workshop>()
                .Where($"ID IN (@1)", attendance.Any() ? string.Join(',', attendance.Select(a => a.WorkshopId)) : "-1")
                .ToArray();

            return workshops.Select(p => p.Adapt<Domain.Workshop>());
        }
    }
}
