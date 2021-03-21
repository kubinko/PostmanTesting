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
    /// Service for manipulation with attendance.
    /// </summary>
    public class AttendanceService : IAttendanceService
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="workshopRepository"><see cref="IWorkshopRepository"/>.</param>
        /// <param name="peopleRepository"><see cref="IPeopleRepository"/>.</param>
        /// <param name="attendanceRepository"><see cref="IAttendanceRepository"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public AttendanceService(
            IWorkshopRepository workshopRepository,
            IPeopleRepository peopleRepository,
            IAttendanceRepository attendanceRepository,
            IActiveUserInfoService userInfo)
        {
            _workshopRepository = Check.NotNull(workshopRepository, nameof(workshopRepository));
            _peopleRepository = Check.NotNull(peopleRepository, nameof(peopleRepository));
            _attendanceRepository = Check.NotNull(attendanceRepository, nameof(attendanceRepository));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public IEnumerable<GetWorkshopAttendeesQuery.Attendee> GetWorkshopAttendees(GetWorkshopAttendeesQuery query)
        {
            var workshop = _workshopRepository.GetWorkshop(query.Id);

            if (workshop == null)
            {
                throw new NotFoundException();
            }
            else if (workshop.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _attendanceRepository.GetWorkshopAttendees(query.Id)
                .Select(a => a.Adapt<GetWorkshopAttendeesQuery.Attendee>());
        }

        /// <inheritdoc/>
        public IEnumerable<GetPersonWorkshops.Workshop> GetPersonWorkshops(GetPersonWorkshops query)
        {
            var person = _peopleRepository.GetPerson(query.Id);

            if (person == null)
            {
                throw new NotFoundException();
            }
            else if (person.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _attendanceRepository.GetPersonWorkshops(query.Id)
                .Select(w => w.Adapt<GetPersonWorkshops.Workshop>());
        }

        /// <inheritdoc/>
        public Task<long> AddPersonToWorkshop(AddPersonToWorkshopCommand command)
        {
            var person = _peopleRepository.GetPerson(command.PersonId);
            var workshop = _workshopRepository.GetWorkshop(command.WorkshopId);

            if (person == null || workshop == null)
            {
                throw new NotFoundException();
            }
            else if ((person.CreatedBy != _userInfo.UserId || workshop.CreatedBy != _userInfo.UserId) && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _attendanceRepository.AddPersonToWorkshop(command.WorkshopId, command.PersonId);
        }

        /// <inheritdoc/>
        public Task RemovePersonFromWorkshop(RemovePersonFromWorkshopCommand command)
        {
            var person = _peopleRepository.GetPerson(command.PersonId);
            var workshop = _workshopRepository.GetWorkshop(command.WorkshopId);

            if (person == null || workshop == null)
            {
                throw new NotFoundException();
            }
            else if ((person.CreatedBy != _userInfo.UserId || workshop.CreatedBy != _userInfo.UserId) && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _attendanceRepository.RemovePersonFromWorkshop(command.WorkshopId, command.PersonId);
        }
    }
}
