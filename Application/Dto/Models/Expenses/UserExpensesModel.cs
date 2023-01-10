using Domain.Categories;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Models.Expenses
{
    public class UserExpensesModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int UserExpensesListId { get; set; }
    }
}
