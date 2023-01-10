using Domain.Entities.Models;

namespace Infrastructure.SeedData.Service
{
    public interface IExpensesSeeder
    {
        public UserExpensesList Seed(string name);
    }
}
