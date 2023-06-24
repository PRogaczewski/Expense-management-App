using Application.Dto.Mapper;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses.Commands;
using Application.IServices.Expenses.Queries;
using Application.IServices.ExpensesList.Commands;
using Application.IServices.ExpensesList.Queries;
using Application.Services.AnalysisService;
using Application.Services.Expenses.Commands;
using Application.Services.Expenses.Queries;
using Application.Services.ExpensesList.Commands;
using Application.Services.ExpensesList.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection ApplicationRegistrationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ExpensesMapper).Assembly);
            services.AddTransient<IExpensesServiceQuery, ExpensesServiceQuery>();
            services.AddTransient<IExpensesServiceCommand, ExpensesServiceCommand>();
            services.AddTransient<IExpensesListServiceQuery, ExpensesListServiceQuery>();
            services.AddTransient<IExpensesListServiceCommand, ExpensesListServiceCommand>();
            services.AddTransient<IUserExpensesAnalysisService, UserExpensesAnalysisService>();
            services.AddTransient<IUserInitialData, UserInitialDataFacade>();

            return services;
        }
    }
}
