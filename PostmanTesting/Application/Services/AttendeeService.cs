using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using PostmanTesting.Domain;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Service for manipulation with attendees.
    /// </summary>
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _repository;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository"><see cref="IAttendeeRepository"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public AttendeeService(IAttendeeRepository repository, IActiveUserInfoService userInfo)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public GetAttendeeQuery.Attendee GetAttendee(GetAttendeeQuery query)
        {
            Attendee attendee = _repository.GetAttendee(query.Id);
            return attendee == null ? null : attendee.Adapt<GetAttendeeQuery.Attendee>();
        }

        /// <inheritdoc/>
        public Task<long> CreateAttendee(CreateAttendeeCommand command)
            => _repository.AddAttendee(command.Adapt<Attendee>());

        /// <inheritdoc/>
        public Task UpdateAttendee(UpdateAttendeeCommand command)
        {
            Attendee attendee = _repository.GetAttendee(command.Id);
            if (attendee == null)
            {
                throw new NotFoundException();
            }
            else if (attendee.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.EditAttendee(command.Adapt<Attendee>());
        }

        /// <inheritdoc/>
        public Task DeleteAttendee(long id)
        {
            Attendee attendee = _repository.GetAttendee(id);
            if (attendee == null)
            {
                throw new NotFoundException();
            }
            else if (attendee.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.DeleteAttendee(id);
        }

        /// <inheritdoc/>
        public Task DeleteAllAttendees()
            => _repository.DeleteAllAttendees();
    }
}