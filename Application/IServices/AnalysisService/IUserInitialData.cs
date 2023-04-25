using Application.Dto.Models.ExpensesList;

namespace Application.IServices.AnalysisService
{
    public interface IUserInitialData
    {
        Task<UserExpensesListResponse> GetUserInitialData(int id);
    }
}
