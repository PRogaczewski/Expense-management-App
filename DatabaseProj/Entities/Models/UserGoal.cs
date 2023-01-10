using Domain.Categories;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models
{
    public class UserGoal : BaseEntity
    {
        public int UserExpenseGoalId { get; set; }

        public UserExpenseGoal UserExpenseGoal { get; set; }

        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Limit { get; set; }
    }
}
