using Domain.Categories;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Models.Expenses
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
