using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models
{
    public class UserExpensesList : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public List<UserExpense> Expenses { get; set; } = new List<UserExpense>();

        public List<UserExpenseGoal> UserGoals { get; set; } = new List<UserExpenseGoal>();

        public List<UserIncome> UserIncomes { get; set; } = new List<UserIncome>();

        public int UserApplicationId;

        public UserApplication UserApplication;

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
