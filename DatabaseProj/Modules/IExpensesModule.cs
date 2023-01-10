using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public interface IExpensesModule
    {
        //public UserExpense GetExpensesList(int id);

        public void CreateExpense(UserExpense model);

        public bool CreateExpensesGoal(UserExpenseGoal model);

        public UserIncome GetMonthlyIncome(int id, string year, string month);

        public void AddMonthlyIncome(UserIncome income);
    }
}
