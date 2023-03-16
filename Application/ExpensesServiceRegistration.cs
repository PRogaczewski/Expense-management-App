using Application.Dto.Mapper;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses;
using Application.IServices.ExpensesList;
using Application.Services.AnalysisService;
using Application.Services.ExpensesList;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection ApplicationRegistrationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ExpensesMapper).Assembly);
            services.AddTransient<IExpensesService, ExpensesService>();
            services.AddTransient<IExpensesListService, ExpensesListService>();
            services.AddTransient<IUserExpensesAnalysisService, UserExpensesAnalysisService>();
            services.AddTransient<IUserInitialData, UserInitialDataFacade>();

            return services;
        }
    }
}
