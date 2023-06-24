using Domain.Entities.Models;

namespace Domain.Modules.Queries
{
    public interface IExpensesModuleQuery
    {
        Task<UserIncome> GetMonthlyIncome(int id, string year, string month);    
    }
}
