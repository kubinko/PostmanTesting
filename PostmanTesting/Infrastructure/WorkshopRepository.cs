using Kros.KORM;
using Kros.Utils;
using Mapster;
using PostmanTesting.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfrastructureWorkshop = PostmanTesting.Infrastructure.Entities.Workshop;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// Workshop repository.
    /// </summary>
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">KORM database accessor.</param>
        public WorkshopRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc />
        public async Task<long> CreateWorkshop(Workshop workshop)
        {
            var infraWorkshop = workshop.Adapt<InfrastructureWorkshop>();
            await _database.AddAsync(infraWorkshop);

            return infraWorkshop.Id;
        }

        /// <inheritdoc />
        public Task EditWorkshop(Workshop workshop)
        {
            var infraWorkshop = workshop.Adapt<InfrastructureWorkshop>();
            return _database.EditAsync(infraWorkshop, default, nameof(InfrastructureWorkshop.Id),
                nameof(InfrastructureWorkshop.Name), nameof(InfrastructureWorkshop.Description),
                nameof(InfrastructureWorkshop.Date), nameof(InfrastructureWorkshop.Price),
                nameof(InfrastructureWorkshop.AttendeesCount), nameof(InfrastructureWorkshop.LastModifiedBy),
                nameof(InfrastructureWorkshop.LastModifiedTimestamp));
        }

        /// <inheritdoc />
        public Task DeleteWorkshop(long id)
            => _database.DeleteAsync<InfrastructureWorkshop>(id);

        /// <inheritdoc />
        public Task DeleteAllWorkshops()
            => _database.DeleteAsync<InfrastructureWorkshop>("1=1");

        /// <inheritdoc />
        public async Task IncreaseAttendance(long id)
        {
            var workshop = _database.Query<InfrastructureWorkshop>().FirstOrDefault(w => w.Id == id);

            if (workshop != null)
            {
                workshop.AttendeesCount++;
                await _database.EditAsync(workshop, default, nameof(InfrastructureWorkshop.Id),
                    nameof(InfrastructureWorkshop.AttendeesCount));
            }
        }

        /// <inheritdoc />
        public async Task DecreaseAttendance(long id)
        {
            var workshop = _database.Query<InfrastructureWorkshop>().FirstOrDefault(w => w.Id == id);

            if (workshop != null)
            {
                workshop.AttendeesCount--;
                await _database.EditAsync(workshop, default, nameof(InfrastructureWorkshop.Id),
                    nameof(InfrastructureWorkshop.AttendeesCount));
            }
        }

        /// <inheritdoc />
        public Workshop GetWorkshop(long id)
        {
            var workshop = _database.Query<InfrastructureWorkshop>().FirstOrDefault(w => w.Id == id);
            return workshop.Adapt<Workshop>();
        }

        /// <inheritdoc />
        public IEnumerable<Workshop> GetAllWorkshops()
        {
            var workshops = _database.Query<InfrastructureWorkshop>().ToArray();
            return workshops.Select(w => w.Adapt<Workshop>());
        }

        /// <inheritdoc />
        public IEnumerable<Workshop> GetAllUserWorkshops(long userId)
        {
            var workshops = _database.Query<InfrastructureWorkshop>().Where(w => w.CreatedBy == userId).ToArray();
            return workshops.Select(w => w.Adapt<Workshop>());
        }
    }
}
