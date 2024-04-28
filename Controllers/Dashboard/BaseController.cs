using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using perfect_wizard.Infrastructure;

namespace perfect_wizard.Controllers.Dashboard
{
    [ApiController]
    [GlobalHeaders("endpoint_id", "wizard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected string UserId => User.Claims?.FirstOrDefault(x => x.Type.Equals("user_id", StringComparison.OrdinalIgnoreCase))?.Value;
    }
}
