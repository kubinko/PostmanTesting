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
        public bool CheckInvoiceGenerated(long workshopId, long personId)
        {
            var attendance = _database.Query<Attendance>()
                .Where(a => a.WorkshopId == workshopId && a.PersonId == personId)
                .FirstOrDefault();
            return (attendance != null && attendance.InvoiceGenerated);
        }

        /// <inheritdoc/>
        public IEnumerable<Domain.Person> GetWorkshopAttendees(long id)
        {
            var attendance = _database.Query<Attendance>().Where(a => a.WorkshopId == id).ToArray();
            string condition = $"ID IN ({(attendance.Any() ? string.Join(',', attendance.Select(a => a.PersonId)) : " - 1")})";
            var people = _database.Query<Entities.Person>()
                .Where(condition)
                .ToArray();

            return people.Select(p => p.Adapt<Domain.Person>());
        }

        /// <inheritdoc/>
        public IEnumerable<Domain.Workshop> GetPersonWorkshops(long id)
        {
            var attendance = _database.Query<Attendance>().Where(a => a.PersonId == id).ToArray();
            string condition = $"ID IN ({(attendance.Any() ? string.Join(',', attendance.Select(a => a.WorkshopId)) : "-1")})";
            var workshops = _database.Query<Entities.Workshop>()
                .Where(condition)
                .ToArray();

            return workshops.Select(p => p.Adapt<Domain.Workshop>());
        }
    }
}
