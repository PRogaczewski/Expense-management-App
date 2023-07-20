using Application.Documents.IService;
using Application.Documents.Service.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Documents
{
    public static partial class DocumentServiceRegistration
    {
        public static IServiceCollection DocumentRegistrationService(this IServiceCollection services)
        {
            services.AddTransient<IDocumentService, PdfService>();

            return services;
        }
    }
}
