using Kros.AspNetCore;
using Kros.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostmanTesting.Application.Commands;
using PostmanTesting.Application.Queries;
using PostmanTesting.Application.Services;
using PostmanTesting.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostmanTesting.Application.Controllers
{
    /// <summary>
    /// Controller for workshops.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class WorkshopController : ApiBaseController
    {
        private readonly IWorkshopService _workshopService;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="workshopService"><see cref="IWorkshopService"/>.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public WorkshopController(IWorkshopService workshopService, IActiveUserInfoService userInfo)
        {
            _workshopService = Check.NotNull(workshopService, nameof(workshopService));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <summary>
        /// Gets workshop by id.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user doesn't have permission for workshop with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If workshop with id <paramref name="id"/> doesn't exist.</response>
        [HttpGet("{id}", Name = nameof(GetWorkshop))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWorkshopQuery.Workshop))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWorkshop(long id)
        {
            GetWorkshopQuery.Workshop workshop = _workshopService.GetWorkshop(new GetWorkshopQuery(id));
            if (workshop != null)
            {
                if (workshop.CreatedBy == _userInfo.UserId || _userInfo.IsAdmin)
                {
                    return Ok(workshop);
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
        /// Gets all workshops.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="403">Forbidden if user is not admin.</response>
        [HttpGet(Name = nameof(GetAllWorkshops))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllWorkshopsQuery.Workshop>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public IEnumerable<GetAllWorkshopsQuery.Workshop> GetAllWorkshops()
            => _workshopService.GetAllWorkshops();

        /// <summary>
        /// Gets all user's workshops.
        /// </summary>
        /// <response code="200">Ok.</response>
        [HttpGet("mine", Name = nameof(GetAllUserWorkshops))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllUserWorkshopsQuery.Workshop>))]
        public IEnumerable<GetAllUserWorkshopsQuery.Workshop> GetAllUserWorkshops()
            => _workshopService.GetAllUserWorkshops(new GetAllUserWorkshopsQuery(_userInfo.UserId));

        /// <summary>
        /// Creates new workshop.
        /// </summary>
        /// <param name="command">Data for creating workshop.</param>
        /// <response code="201">Created. Workshop ID in body.</response>
        [HttpPost(Name = nameof(CreateWorkshop))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(long))]
        public async Task<IActionResult> CreateWorkshop(CreateWorkshopCommand command)
        {
            long id = await _workshopService.CreateWorkshop(command);
            return Created(Url.Link(nameof(GetWorkshop), new { id }), new { Id = id });
        }

        /// <summary>
        /// Updates workshop.
        /// </summary>
        /// <param name="id">Workshop id.</param>
        /// <param name="command">Data for updating workshop.</param>
        /// <response code="200">Updated.</response>
        /// <response code="403">Forbidden when user doesn't have permission for workshop with <paramref name="id"/>.</response>
        /// <response code="404">If workshop with <paramref name="id"/> doesn't exist.</response>
        [HttpPut("{id}", Name = nameof(UpdateWorkshop))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWorkshop(long id, UpdateWorkshopCommand command)
        {
            command.Id = id;
            await _workshopService.UpdateWorkshop(command);
            return Ok();
        }

        /// <summary>
        /// Deletes workshop.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden when user doesn't have permission for workshop with <paramref name="id"/>.</response>
        /// <response code="404">If workshop with <paramref name="id"/> doesn't exist.</response>
        [HttpDelete("{id}", Name = nameof(DeleteWorkshop))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkshop(long id)
        {
            await _workshopService.DeleteWorkshop(id);
            return NoContent();
        }

        /// <summary>
        /// Delete all workshops.
        /// </summary>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden if user is not admin.</response>
        [HttpDelete("all", Name = nameof(DeleteAllWorkshops))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> DeleteAllWorkshops()
        {
            await _workshopService.DeleteAllWorkshops();
            return NoContent();
        }
    }
}
