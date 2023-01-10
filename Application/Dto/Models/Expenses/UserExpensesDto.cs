using Domain.Categories;

namespace Application.Dto.Models.Expenses
{
    public class UserExpensesDto
    {
        public int Id { get; set; }

        public ExpenseCategories Category { get; set; }

        public decimal Price { get; set; }

        public int UserExpensesListId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
