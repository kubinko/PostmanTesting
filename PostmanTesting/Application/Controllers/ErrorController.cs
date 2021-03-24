using Kros.AspNetCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PostmanTesting.Application.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = StatusCodes.Status500InternalServerError;

            if (exception is NotFoundException)
            {
                code = StatusCodes.Status404NotFound;
            }
            else if (exception is ResourceIsForbiddenException)
            {
                code = StatusCodes.Status403Forbidden;
            }
            else if (exception is RequestConflictException)
            {
                code = StatusCodes.Status409Conflict;
            }

            return StatusCode(code, exception.Message);
        }
    }
}
