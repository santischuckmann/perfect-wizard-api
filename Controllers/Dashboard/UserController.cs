using MediatR;
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
        public async Task<IActionResult> CreateUser([FromBody] Application.DTOs.UserDto user)
        {
            await _mediator.Send(new Application.Commands.CreateUserCommand() { User = user });

            return Ok();
        }
        public async Task<IActionResult> Login([FromBody] Application.DTOs.LoginUserDto user)
        {
            var result = await _mediator.Send(new Application.Commands.LoginCommand() { User = user });

            return Ok(result);
        }
    }
}
