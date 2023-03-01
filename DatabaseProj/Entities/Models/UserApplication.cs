using Domain.Entities.Base;

namespace Domain.Entities.Models
{
    public class UserApplication : BaseEntity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<UserExpensesList> ExpensesLists { get; set; } = new List<UserExpensesList>();
    }
}
