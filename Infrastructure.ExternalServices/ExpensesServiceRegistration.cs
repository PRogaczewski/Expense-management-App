using Application.ExternalServices.Models;
using Domain.Modules.Queries;
using Infrastructure.ExternalServices.HttpClients;
using Infrastructure.ExternalServices.Repositories.Query;
using Infrastructure.ExternalServices.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ExternalServices
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection InfrastructureExternalServiceRegistrationService(this IServiceCollection services)
        {
            services.AddTransient<IDocumentModuleQuery<PdfResponse, PdfRequest>, ReportRepositoryQuery>();
            services.AddTransient(typeof(IExternalServiceManagerService<,>), typeof(ExternalServiceManagerService<,>));

            services.AddHttpClient<PdfServiceHttpClient>();

            return services;
        }
    }
}
