using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.ExpensesList
{
    public class UserExpensesListSeederModel : UserExpensesListModel
    {
        public List<UserExpensesDto> Expenses { get; set; } = new List<UserExpensesDto>();
    }
}
