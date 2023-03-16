using Application.Dto.Models.ExpensesList;

namespace Application.IServices.AnalysisService
{
    public interface IUserInitialData
    {
        public UserExpensesListResponse GetUserInitialData(int id);
    }
}
