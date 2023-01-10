using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models
{
    public class UserExpenseGoal : ExpensesListBaseEntity
    {
        public List<UserGoal> UserCategoryGoals { get; set; }

        [Required]
        public DateTime MonthChosenForGoal { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
