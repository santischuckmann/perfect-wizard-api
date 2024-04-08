using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class LoginCommand: IRequest<DTOs.UserTokenDto>
    {
        public DTOs.LoginUserDto User { get; set; }
    }
}
