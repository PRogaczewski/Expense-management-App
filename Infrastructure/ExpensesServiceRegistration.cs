using Domain.Modules;
using Infrastructure.EF.Database;
using Infrastructure.EF.Repositories.Expenses;
using Infrastructure.EF.Repositories.ExpensesList;
using Infrastructure.SeedData.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EF
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection InfrastructureRegistrationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ExpenseDbString"), builder => 
            {
                builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            }));
            services.AddTransient<IExpensesModule, ExpensesRepository>();
            services.AddTransient<IExpensesListModule, ExpensesListRepository>();
            services.AddTransient<IExpensesSeeder, ExpensesSeeder>();

            return services;
        }
    }
}
