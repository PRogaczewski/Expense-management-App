using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Models.Expenses
{
    public class UserExpenseGoalDto
    {
        [Required]
        public int UserExpensesListId { get; set; }

        [Required]
        public List<UserGoalDto> UserCategoryGoals { get; set; }

        [Required]
        public DateTime MonthChosenForGoal { get; set; }
    }
}
