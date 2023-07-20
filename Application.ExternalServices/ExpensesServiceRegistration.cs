using Application.ExternalServices.IServices.Query;
using Application.ExternalServices.Models;
using Application.ExternalServices.Services.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ExternalServices
{
    public static partial class ExpensesServiceRegistration
    {
        public static IServiceCollection ApplicationExternalServiceRegistrationService(this IServiceCollection services)
        {
            services.AddScoped<IDocumentServiceQuery<PdfResponse, PdfRequest>, DocumentServiceQuery>();

            return services;
        }
    }
}
