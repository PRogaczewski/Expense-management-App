using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProj.DatabaseEntities.Models
{
    public class UserGoal
    {
        public int Id { get; set; }

        public int UserExpenseGoalId { get; set; }

        public UserExpenseGoal UserExpenseGoal { get; set; }

        public ExpenseCategories Category { get; set; }

        public decimal Limit { get; set; }
    }
}
