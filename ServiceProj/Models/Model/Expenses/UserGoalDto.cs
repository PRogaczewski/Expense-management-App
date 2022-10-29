using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.Expenses
{
    public class UserGoalDto
    {
        //public int UserExpenseGoalId { get; set; }

        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Limit { get; set; }
    }
}
