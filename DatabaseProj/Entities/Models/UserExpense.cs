using Domain.Categories;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Models
{
    public class UserExpense : ExpensesListBaseEntity
    {
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
