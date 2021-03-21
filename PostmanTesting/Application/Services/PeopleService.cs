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
    /// Service for manipulation with people.
    /// </summary>
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _repository;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository"><see cref="IPeopleRepository"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public PeopleService(IPeopleRepository repository, IActiveUserInfoService userInfo)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public GetPersonQuery.Person GetPerson(GetPersonQuery query)
        {
            Person person = _repository.GetPerson(query.Id);
            return person == null ? null : person.Adapt<GetPersonQuery.Person>();
        }

        /// <inheritdoc/>
        public Task<long> CreatePerson(CreatePersonCommand command)
            => _repository.AddPerson(command.Adapt<Person>());

        /// <inheritdoc/>
        public Task UpdatePerson(UpdatePersonCommand command)
        {
            Person person = _repository.GetPerson(command.Id);
            if (person == null)
            {
                throw new NotFoundException();
            }
            else if (person.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.EditPerson(command.Adapt<Person>());
        }

        /// <inheritdoc/>
        public Task DeletePerson(long id)
        {
            Person person = _repository.GetPerson(id);
            if (person == null)
            {
                throw new NotFoundException();
            }
            else if (person.CreatedBy != _userInfo.UserId && !_userInfo.IsAdmin)
            {
                throw new ResourceIsForbiddenException();
            }

            return _repository.DeletePerson(id);
        }

        /// <inheritdoc/>
        public Task DeleteAllPeople()
            => _repository.DeleteAllPeople();
    }
}