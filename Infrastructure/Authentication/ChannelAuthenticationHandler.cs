using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace perfect_wizard.Infraestructure.Authentication
{
    public class ChannelAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ChannelAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var channelId = Context.Request.Headers["id_canal"];

            if (string.IsNullOrEmpty(channelId))
                AuthenticateResult.Fail("Authentication failed.");

            var authenticationScheme = GetAuthenticationScheme(channelId);
            if (authenticationScheme == null)
                AuthenticateResult.Fail("Authentication failed.");

            var result = await Context.AuthenticateAsync(authenticationScheme);

            if (result.Succeeded)
            {
                return AuthenticateResult.Success(result.Ticket);
            }

            return AuthenticateResult.Fail(result.Failure?.Message ?? "Authentication failed.");
        }

        private string? GetAuthenticationScheme(string channelId)
        {
            if (channelId.Equals("spa", StringComparison.OrdinalIgnoreCase))
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }
            else if (channelId.Equals("desktop", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticationConstants.ApiKeyScheme;
            }

            return null;
        }
    }
}
