using Application.ExternalServices.Models;
using Domain.Modules.Queries;
using Infrastructure.ExternalServices.HttpClients;
using Infrastructure.ExternalServices.Service;

namespace Infrastructure.ExternalServices.Repositories.Query
{
    public class ReportRepositoryQuery : IDocumentModuleQuery<PdfResponse, PdfRequest>
    {
        private readonly IExternalServiceManagerService<PdfResponse, PdfServiceHttpClient> _managerService;

        public ReportRepositoryQuery(IExternalServiceManagerService<PdfResponse, PdfServiceHttpClient> managerService)
        {
            _managerService = managerService;
        }

        public async Task<PdfResponse> GetReport(PdfRequest model)
        {
            var response = await _managerService.CreateData(model);

            return response.Data;
        }
    }
}
