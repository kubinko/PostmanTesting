using Kros.AspNetCore;
using Kros.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using PostmanTesting.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Controllers
{
    /// <summary>
    /// Controller for attendance.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AttendanceController : ApiBaseController
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="attendanceService"><see cref="IAttendanceService"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public AttendanceController(IAttendanceService attendanceService, IActiveUserInfoService userInfo)
        {
            _attendanceService = Check.NotNull(attendanceService, nameof(attendanceService));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <summary>
        /// Gets workshop attendees.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user doesn't have permission for workshop with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If workshop with <paramref name="id"/> doesn't exist.</response>
        [HttpGet("attendees/{id}", Name = nameof(GetWorkshopAttendees))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWorkshopAttendeesQuery.Attendee))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<GetWorkshopAttendeesQuery.Attendee> GetWorkshopAttendees(long id)
            => _attendanceService.GetWorkshopAttendees(new GetWorkshopAttendeesQuery(id));

        /// <summary>
        /// Gets person workshops.
        /// </summary>
        /// <param name="id">Person ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user doesn't have permission for person with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If person with <paramref name="id"/> doesn't exist.</response>
        [HttpGet("workshops/{id}", Name = nameof(GetPersonWorkshops))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPersonWorkshops.Workshop))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<GetPersonWorkshops.Workshop> GetPersonWorkshops(long id)
            => _attendanceService.GetPersonWorkshops(new GetPersonWorkshops(id));

        /// <summary>
        /// Adds person to workshop.
        /// </summary>
        /// <param name="command">Data for adding person to workshop.</param>
        /// <response code="201">Created. Record ID in body.</response>
        /// <response code="403">Forbidden when user doesn't have permission for specified person or workshop.</response>
        /// <response code="404">If specified person or workshop doesn't exist.</response>
        [HttpPost(Name = nameof(AddPersonToWorkshop))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPersonToWorkshop(AddPersonToWorkshopCommand command)
        {
            long id = await _attendanceService.AddPersonToWorkshop(command);
            return Created(Url.Link(nameof(GetWorkshopAttendees), new { id = command.WorkshopId }), new { Id = id });
        }

        /// <summary>
        /// Removes person from workshop.
        /// </summary>
        /// <param name="command">Data for removing person from workshop.</param>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden when user doesn't have permission for specified person or workshop.</response>
        /// <response code="404">If specified person or workshop doesn't exist.</response>
        [HttpDelete(Name = nameof(RemovePersonFromWorkshop))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemovePersonFromWorkshop(RemovePersonFromWorkshopCommand command)
        {
            await _attendanceService.RemovePersonFromWorkshop(command);
            return NoContent();
        }
    }
}
