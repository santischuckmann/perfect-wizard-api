using MediatR;

namespace perfect_wizard.Application.Queries
{
    public class GetUserQuery: IRequest<DTOs.UserDto>
    {
        public string UserId { get; set; }
    }
}
