using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using PostmanTesting.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Service for manipulation with workshops.
    /// </summary>
    public class WorkshopService : IWorkshopService
    {
        private readonly IWorkshopRepository _repository;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository"><see cref="IWorkshopRepository"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public WorkshopService(IWorkshopRepository repository, IActiveUserInfoService userInfo)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public GetWorkshopQuery.Workshop GetWorkshop(GetWorkshopQuery query)
        {
            Workshop workshop = _repository.GetWorkshop(query.Id);
            return workshop == null ? null : workshop.Adapt<GetWorkshopQuery.Workshop>();
        }

        /// <inheritdoc/>
        public IEnumerable<GetAllWorkshopsQuery.Workshop> GetAllWorkshops()
        {
            IEnumerable<Workshop> workshops = _repository.GetAllWorkshops();
            return workshops.Select(w => w.Adapt<GetAllWorkshopsQuery.Workshop>());
        }

        /// <inheritdoc/>
        public IEnumerable<GetAllUserWorkshopsQuery.Workshop> GetAllUserWorkshops(GetAllUserWorkshopsQuery query)
        {
            IEnumerable<Workshop> workshops = _repository.GetAllUserWorkshops(query.Id);
            return workshops.Select(w => w.Adapt<GetAllUserWorkshopsQuery.Workshop>());
        }

        /// <inheritdoc/>
        public Task<long> CreateWorkshop(CreateWorkshopCommand command)
            => _repository.CreateWorkshop(command.Adapt<Workshop>());

        /// <inheritdoc/>
        public Task UpdateWorkshop(UpdateWorkshopCommand command)
        {
            Workshop workshop = _repository.GetWorkshop(command.Id);
            if (workshop == null)
            {
                throw new NotFoundException();
            }
            else if (workshop.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.EditWorkshop(command.Adapt<Workshop>());
        }

        /// <inheritdoc/>
        public Task DeleteWorkshop(long id)
        {
            Workshop workshop = _repository.GetWorkshop(id);
            if (workshop == null)
            {
                throw new NotFoundException();
            }
            else if (workshop.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.DeleteWorkshop(id);
        }

        /// <inheritdoc/>
        public Task DeleteAllWorkshops()
            => _repository.DeleteAllWorkshops();
    }
}
