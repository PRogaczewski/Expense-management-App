using Domain.Entities.Models;

namespace Application.Dto.Models.ExpensesList
{
    public class UserExpensesListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserApplicationId { get; set; }
    }
}
