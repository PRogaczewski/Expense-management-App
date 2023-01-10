using Domain.Categories;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models
{
    public class UserExpense : ExpensesListBaseEntity
    {
        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
