using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace perfect_wizard.Infraestructure.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration _config;
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _config = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string apiKey = _config.GetSection("Settings")["ApiKey"];

            if (!apiKey.Equals(Context.Request.Headers["api_key"]))
                return AuthenticateResult.Fail("Invalid API Key.");

            var idUsuario = Context.Request.Query["idUsuario"].FirstOrDefault();

            if (string.IsNullOrEmpty(idUsuario))
                return AuthenticateResult.Fail("Invalid idUsuario.");

            var claims = new List<Claim>
            {
                new Claim("idUsuario", idUsuario ?? ""),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
