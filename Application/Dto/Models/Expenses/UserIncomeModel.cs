using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Models.Expenses
{
    public class UserIncomeModel
    {
        public int UserExpensesListId { get; set; }

        [Required]
        public decimal Income { get; set; }
    }
}
