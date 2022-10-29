using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.Expenses
{
    public class UserExpenseGoalDto
    {
        public int UserExpensesListId { get; set; }

        [Required]
        public List<UserGoalDto> UserCategoryGoals { get; set; }

        [Required]
        public DateTime MonthChosenForGoal { get; set; }
    }
}
