using DTO;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppProjectManagement.Infrastructure
{
    internal static class CookieOptionsBuilder
    {
        internal static void Configure(CookieAuthenticationOptions options, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(AuthDto.CookieSettings)).Get<AuthDto.CookieSettings>();

            //options.LoginPath = "/Account/Authorization";
            //options.AccessDeniedPath = "/Error";
            options.Cookie.Name = "Identity";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(settings!.ExpireTimeSpanMinutes);
            options.SlidingExpiration = settings.SlidingExpiration;
            options.Cookie = new CookieBuilder
            {
                HttpOnly = settings.HttpOnly,
                SecurePolicy = Enum.Parse<CookieSecurePolicy>(settings.SecurePolicy),
                SameSite = Enum.Parse<SameSiteMode>(settings.SameSite)
            };
        }
    }
}
