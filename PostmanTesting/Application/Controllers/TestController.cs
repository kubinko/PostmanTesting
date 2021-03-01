using Kros.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PostmanTesting.Application.Controllers
{
    /// <summary>
    /// Test controller.
    /// </summary>
    public class TestController : ApiBaseController
    {
        /// <summary>
        /// Test endpoint for unauthorized user.
        /// </summary>
        /// <response code="200">Ok.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult TestUnauthorizedUser()
            => Ok("Hello unauthorized world!");
    }
}
