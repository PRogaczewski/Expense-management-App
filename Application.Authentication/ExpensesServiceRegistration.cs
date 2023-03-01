using Application.Authentication.IServices;
using Application.Authentication.Models;
using Application.Authentication.Models.Mapper;
using Application.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Authentication
{
    public static class ExpensesServiceRegistration
    {
        public static IServiceCollection ApplicationAuthenticationRegistrationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AuthenticationMapper).Assembly);
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.Configure<WebToken>(configuration.GetSection("JsonWebTokenConfig"));

            return services;
        }
    }
}
