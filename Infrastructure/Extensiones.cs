using Cedeira.Infraestructura.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace perfect_wizard.Infraestructure
{
    public static class Extensiones
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(options => options.DefaultScheme = AuthenticationConstants.MultiChannelScheme)
                .AddScheme<AuthenticationSchemeOptions, ChannelAuthenticationHandler>(AuthenticationConstants.MultiChannelScheme, options => { });

            services.AddAuthentication(options => options.DefaultScheme = AuthenticationConstants.ApiKeyScheme)
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(AuthenticationConstants.ApiKeyScheme, apikeyOptions => { });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var jwtAppSettings = configuration.GetSection("JwtIssuerOptions");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAppSettings["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwtAppSettings["Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettings["SecretKey"])),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return System.Threading.Tasks.Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IApplicationBuilder UseResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseNormalizerMiddleware>();
        }
    }
}
