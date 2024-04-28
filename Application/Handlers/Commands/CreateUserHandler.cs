using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using perfect_wizard.Application.Commands;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Models;
using AutoMapper;
using ServiceStack;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class CreateUserHandler: IRequestHandler<CreateUserCommand>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public CreateUserHandler(Infrastructure.MongoDBService dbService, IMapper mapper)
        {
            _dbService = dbService;
            _mapper = mapper;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            IsValidUser(request.User);

            Models.User user = _mapper.Map<User>(request.User);
            user.password = Utils.Hash(user.password);

            await _dbService.User.InsertOneAsync(user, new MongoDB.Driver.InsertOneOptions { }, cancellationToken);
        }

        private static void IsValidUser(DTOs.UserDto user)
        {
            if (user.Password.IsNullOrEmpty())
                throw new Exception("Password cannot be empty");

            if (user.Email.IsNullOrEmpty())
                throw new Exception("Email cannot be empty");

            if (user.Username.IsNullOrEmpty())
                throw new Exception("Username cannot be empty");
        }
    }
}
