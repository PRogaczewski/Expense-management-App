using Domain.Modules.Commands;
using Domain.Modules.Queries;
using Infrastructure.EF.Database;
using Infrastructure.EF.Repositories.Expenses.Commands;
using Infrastructure.EF.Repositories.Expenses.Queries;
using Infrastructure.EF.Repositories.ExpensesList.Commands;
using Infrastructure.EF.Repositories.ExpensesList.Queries;
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
            services.AddTransient<ExpensesRepositoryQuery>();
            services.AddTransient<IExpensesModuleQuery, CachedExpensesRepositoryQuery>();

            //services.AddTransient<IExpensesModuleQuery, ExpensesRepositoryQuery>();
            services.AddTransient<IExpensesModuleCommand, ExpensesRepositoryCommand>();
            services.AddTransient<IExpensesListModuleQuery, ExpensesListRepositoryQuery>();
            services.AddTransient<IExpensesListModuleCommand, ExpensesListRepositoryCommand>();
            services.AddTransient<IExpensesSeeder, ExpensesSeeder>();

            return services;
        }
    }
}
