using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProj.DatabaseEntities.Models
{
    public class UserExpenseGoal
    {
        public int Id { get; set; }

        public int UserExpensesListId { get; set; }

        public UserExpensesList UserExpensesList { get; set; }

        public List<UserGoal> UserCategoryGoals { get; set; }

        [Required]
        public DateTime MonthChosenForGoal { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
