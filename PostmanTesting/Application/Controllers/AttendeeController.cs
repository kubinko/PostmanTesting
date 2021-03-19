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
    /// Controller for attendees.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AttendeeController : ApiBaseController
    {
        private readonly IAttendeeService _attendeeService;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="attendeeService"><see cref="IAttendeeService"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public AttendeeController(IAttendeeService attendeeService, IActiveUserInfoService userInfo)
        {
            _attendeeService = Check.NotNull(attendeeService, nameof(attendeeService));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <summary>
        /// Gets attendee by id.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user doesn't have permission for attendee with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If attendee with id <paramref name="id"/> doesn't exist.</response>
        [HttpGet("{id}", Name = nameof(GetAttendee))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAttendeeQuery.Attendee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAttendee(long id)
        {
            GetAttendeeQuery.Attendee attendee = _attendeeService.GetAttendee(new GetAttendeeQuery(id));
            if (attendee != null)
            {
                if (attendee.CreatedBy == _userInfo.UserId || _userInfo.IsAdmin)
                {
                    return Ok(attendee);
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
        /// Creates new attendee.
        /// </summary>
        /// <param name="command">Data for creating attendee.</param>
        /// <response code="201">Created. Attendee ID in body.</response>
        [HttpPost(Name = nameof(CreateAttendee))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(long))]
        public async Task<IActionResult> CreateAttendee(CreateAttendeeCommand command)
        {
            long id = await _attendeeService.CreateAttendee(command);
            return Created(Url.Link(nameof(GetAttendee), new { id }), new { Id = id });
        }

        /// <summary>
        /// Updates attendee.
        /// </summary>
        /// <param name="id">Attendee id.</param>
        /// <param name="command">Data for updating attendee.</param>
        /// <response code="200">Updated.</response>
        /// <response code="403">Forbidden when user doesn't have permission for attendee with <paramref name="id"/>.</response>
        /// <response code="404">If attendee with <paramref name="id"/> doesn't exist.</response>
        [HttpPut("{id}", Name = nameof(UpdateAttendee))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttendee(long id, UpdateAttendeeCommand command)
        {
            command.Id = id;
            await _attendeeService.UpdateAttendee(command);
            return Ok();
        }

        /// <summary>
        /// Deletes attendee.
        /// </summary>
        /// <param name="id">Attendee id.</param>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden when user doesn't have permission for attendee with <paramref name="id"/>.</response>
        /// <response code="404">If attendee with <paramref name="id"/> doesn't exist.</response>
        [HttpDelete("{id}", Name = nameof(DeleteAttendee))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAttendee(long id)
        {
            await _attendeeService.DeleteAttendee(id);
            return NoContent();
        }

        /// <summary>
        /// Delete all attendees.
        /// </summary>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden if user is not admin.</response>
        [HttpDelete("all", Name = nameof(DeleteAllAttendees))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> DeleteAllAttendees()
        {
            await _attendeeService.DeleteAllAttendees();
            return NoContent();
        }
    }
}
