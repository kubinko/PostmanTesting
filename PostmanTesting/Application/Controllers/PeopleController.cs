using Kros.AspNetCore;
using Kros.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using PostmanTesting.Application.Services;
using PostmanTesting.Infrastructure;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Controllers
{
    /// <summary>
    /// Controller for people.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PeopleController : ApiBaseController
    {
        private readonly IPeopleService _peopleService;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="peopleService"><see cref="IPeopleService"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public PeopleController(IPeopleService peopleService, IActiveUserInfoService userInfo)
        {
            _peopleService = Check.NotNull(peopleService, nameof(peopleService));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <summary>
        /// Gets person by ID.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user doesn't have permission for person with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If person with id <paramref name="id"/> doesn't exist.</response>
        [HttpGet("{id}", Name = nameof(GetPerson))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPersonQuery.Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPerson(long id)
        {
            GetPersonQuery.Person person = _peopleService.GetPerson(new GetPersonQuery(id));
            if (person != null)
            {
                if (person.CreatedBy == _userInfo.UserId || _userInfo.IsAdmin)
                {
                    return Ok(person);
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates new person.
        /// </summary>
        /// <param name="command">Data for creating person.</param>
        /// <response code="201">Created. Person ID in body.</response>
        [HttpPost(Name = nameof(CreatePerson))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(long))]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
        {
            long id = await _peopleService.CreatePerson(command);
            return Created(Url.Link(nameof(GetPerson), new { id }), new { Id = id });
        }

        /// <summary>
        /// Updates person.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <param name="command">Data for updating person.</param>
        /// <response code="200">Updated.</response>
        /// <response code="403">Forbidden when user doesn't have permission for person with <paramref name="id"/>.</response>
        /// <response code="404">If person with <paramref name="id"/> doesn't exist.</response>
        [HttpPut("{id}", Name = nameof(UpdatePerson))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePerson(long id, UpdatePersonCommand command)
        {
            command.Id = id;
            await _peopleService.UpdatePerson(command);
            return Ok();
        }

        /// <summary>
        /// Deletes person.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden when user doesn't have permission for person with <paramref name="id"/>.</response>
        /// <response code="404">If person with <paramref name="id"/> doesn't exist.</response>
        [HttpDelete("{id}", Name = nameof(DeletePerson))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(long id)
        {
            await _peopleService.DeletePerson(id);
            return NoContent();
        }

        /// <summary>
        /// Delete all people.
        /// </summary>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden if user is not admin.</response>
        [HttpDelete("all", Name = nameof(DeleteAllPeople))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> DeleteAllPeople()
        {
            await _peopleService.DeleteAllPeople();
            return NoContent();
        }
    }
}
