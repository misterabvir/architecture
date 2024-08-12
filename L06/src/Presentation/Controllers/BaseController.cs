using Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult Problem(Error error)
        {
            return Problem(statusCode: (int)error.Code, title: error.Description);
        }
    }
}