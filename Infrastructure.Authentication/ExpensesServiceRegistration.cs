using Application.Authentication.IServices;
using Domain.Entities.Models;
using Domain.Modules;
using Infrastructure.Authentication.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authentication
{
    public static class ExpensesServiceRegistration
    {
        public static IServiceCollection InfrastructureAuthenticationRegistrationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthenticationManagerService<UserApplication>, AuthenticationManagerService>();
            services.AddTransient<IAuthenticationModule, AuthenticationModule>();
            services.AddScoped<IUserContextModule, UserContextService>();
            services.AddScoped<IPasswordHasher<UserApplication>, PasswordHasher<UserApplication>>();
            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JsonWebTokenConfig:Issuer"],
                    ValidAudience = configuration["JsonWebTokenConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebTokenConfig:Key"]))
                };
            });

            return services;
        }
    }
}
