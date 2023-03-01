using Domain.Entities.Base;

namespace Domain.Entities.Models
{
    public class UserExpensesList : BaseEntity
    {
        public string Name { get; set; }

        public List<UserExpense> Expenses { get; set; } = new List<UserExpense>();

        public List<UserExpenseGoal> UserGoals { get; set; } = new List<UserExpenseGoal>();

        public List<UserIncome> UserIncomes { get; set; } = new List<UserIncome>();

        public int UserApplicationId;

        public UserApplication UserApplication;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
