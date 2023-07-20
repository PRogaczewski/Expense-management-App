using Application.Exceptions;
using Application.ExternalServices.IServices.Query;
using Application.ExternalServices.Models;
using Application.IServices.AnalysisService;
using Application.IServices.ExpensesList.Queries;
using Domain.Modules;
using Domain.Modules.Queries;

namespace Application.ExternalServices.Services.Query
{
    public class DocumentServiceQuery : IDocumentServiceQuery<PdfResponse, PdfRequest>
    {
        private readonly IDocumentModuleQuery<PdfResponse, PdfRequest> _documentModule;

        private readonly IUserContextModule _userContext;

        private readonly IExpensesListServiceQuery _expensesListService;

        private readonly IUserExpensesAnalysisService _expensesAnalysisService;

        public DocumentServiceQuery(IDocumentModuleQuery<PdfResponse, PdfRequest> documentModule, IUserContextModule userContext, IExpensesListServiceQuery expensesListService, IUserExpensesAnalysisService expensesAnalysisService)
        {
            _documentModule = documentModule;
            _userContext = userContext;
            _expensesListService = expensesListService;
            _expensesAnalysisService = expensesAnalysisService;
        }

        public async Task<PdfResponse> GetReport(int id)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var expensesList = await _expensesListService.GetExpensesList(id);

            //create report
            var model = new PdfRequest() { Text = "AAAAAAAAAAAAAAAAA", Filename = "Expense report" };

            return await _documentModule.GetReport(model);
        }
    }
}
