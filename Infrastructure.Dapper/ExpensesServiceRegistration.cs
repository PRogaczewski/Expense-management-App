using Domain.Modules;
using Infrastructure.Dapper.Repositories.Expenses;
using Infrastructure.Dapper.Repositories.ExpensesList;
using Infrastructure.SeedData.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dapper
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection InfrastructureRegistrationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExpensesModule, ExpensesRepository>();
            services.AddTransient<IExpensesListModule, ExpensesListRepository>();
            services.AddTransient<IExpensesSeeder, ExpensesSeeder>();

            return services;
        }
    }
}
