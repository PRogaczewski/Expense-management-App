using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.ExpensesList
{
    public class UserExpensesListDtoModel : UserExpensesListDto
    {
        public List<UserExpensesDto> Expenses { get; set; } = new List<UserExpensesDto>();

        public List<UserExpenseGoalDto> UserGoals { get; set; } = new List<UserExpenseGoalDto>();

        public List<UserIncomeDto> UserIncomes { get; set; } = new List<UserIncomeDto>();
    }
}
