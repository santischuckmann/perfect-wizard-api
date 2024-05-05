using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace perfect_wizard.Controllers.Dashboard
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Application.DTOs.UserDtoCreation user)
        {
            await _mediator.Send(new Application.Commands.CreateUserCommand() { User = user });

            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Application.DTOs.LoginUserDto user)
        {
            var result = await _mediator.Send(new Application.Commands.LoginCommand() { User = user });

            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            string userId = User.Claims?.FirstOrDefault(x => x.Type.Equals("user_id", StringComparison.OrdinalIgnoreCase))?.Value;
            var result = await _mediator.Send(new Application.Queries.GetUserQuery() { UserId = userId });

            return Ok(result);
        }
    }
}
