using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;

namespace Infrastructure.SeedData.Service
{
    public class UserExpensesListSeederModel : UserExpensesListModel
    {
        public List<UserExpensesDto> Expenses { get; set; } = new List<UserExpensesDto>();
    }
}
