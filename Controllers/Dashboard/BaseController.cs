using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using perfect_wizard.Infrastructure;

namespace perfect_wizard.Controllers.Dashboard
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "bearer")]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected string UserId => User.Claims?.FirstOrDefault(x => x.Type.Equals("userId", StringComparison.OrdinalIgnoreCase))?.Value;
    }
}
