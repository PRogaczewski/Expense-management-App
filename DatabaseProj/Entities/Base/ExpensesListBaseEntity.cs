using Domain.Entities.Models;

namespace Domain.Entities.Base
{
    public abstract class ExpensesListBaseEntity : BaseEntity
    {
        public int UserExpensesListId { get; set; }

        public UserExpensesList UserExpensesList { get; set; }
    }
}
