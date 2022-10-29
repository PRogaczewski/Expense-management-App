using DatabaseProj.DatabaseEntities.Models;
using ExpensesApi.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProj.Database.Models
{
    public class UserExpense
    {
        public int Id { get; set; }

        [Required]
        public ExpenseCategories Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int UserExpensesListId { get; set; }

        public UserExpensesList UserExpensesList { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
