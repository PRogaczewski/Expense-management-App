using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Models.ExpensesList
{
    public class UserExpensesListModel
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
