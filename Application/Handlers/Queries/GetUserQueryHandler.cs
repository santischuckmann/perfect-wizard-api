using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Application.Queries;

namespace perfect_wizard.Application.Handlers.Queries
{
    public class GetUserQueryHandler: IRequestHandler<GetUserQuery, DTOs.UserDto>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public GetUserQueryHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task<DTOs.UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            Models.User tenant = await _dbService.User.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

            var userDto = _mapper.Map<UserDto>(tenant);

            return userDto;
        }
    }
}
