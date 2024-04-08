using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class CreateUserCommand: IRequest
    {
        public DTOs.UserDto User { get; set; }
    }
}
