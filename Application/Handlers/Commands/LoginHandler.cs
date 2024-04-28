using MediatR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
using perfect_wizard.Application.DTOs;
using ServiceStack;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, DTOs.UserTokenDto>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IConfiguration _config;
        public LoginHandler(Infrastructure.MongoDBService mongoDBService, IConfiguration config)
        {
            _dbService = mongoDBService;
            _config = config; 
        }
        public async Task<UserTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.User;

            if (userDto.Identifier.IsNullOrEmpty())
                throw new Exception("A username or email must be indicated");

            Models.User user = await _dbService.User.Find(
                Builders<Models.User>.Filter.Or(
                    Builders<Models.User>.Filter.Eq(u => u.username, request.User.Identifier),
                    Builders<Models.User>.Filter.Eq(u => u.email, request.User.Identifier)
                )
            ).FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new Exception($"User with given " +
                $"{(IsIdentifierAnEmail(request.User.Identifier) ? "email" : "username")} is not registered");

            Check(user.password, userDto.Password);

            var userTokenDto = GenerateToken(user);

            return userTokenDto;
        }

        private static bool IsIdentifierAnEmail(string identifier)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            Match match = regex.Match(identifier);

            return match.Success;
        }

        private DTOs.UserTokenDto GenerateToken(Models.User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtIssuerOptions:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new List<Claim>
            {
                new Claim("user_id", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username),
                new Claim(JwtRegisteredClaimNames.Name, user.name),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            var expiration = DateTime.UtcNow.AddDays(3);

            var payload = new JwtPayload(
                _config["JwtIssuerOptions:Issuer"],
                _config["JwtIssuerOptions:Audience"],
                claims,
                DateTime.Now,
                expiration
            );

            return new UserTokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, payload)),
            };
        }

        private static bool Check(string hash, string password)
        {
            string hashed = Utils.Hash(password);

            if (hash != hashed)
                throw new Exception("Incorrect password");

            return true;
        }
    }
}

