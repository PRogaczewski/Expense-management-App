using Domain.Categories;
using Domain.Entities.Models;

namespace Infrastructure.SeedData.Service
{
    public class ExpensesSeeder : IExpensesSeeder
    {
        public UserExpensesList Seed(string name)
        {
            var model = DataSeeder(name);

            return model;
        }

        private UserExpensesList DataSeeder(string name)
        {
            var rand = new Random();

            var expenses = new List<UserExpense>();

            for (int i = 0; i < 150; i++)
            {
                expenses.Add(new UserExpense()
                {
                    Category = (ExpenseCategories)rand.Next(0, 18),
                    Price = decimal.Round((decimal)(rand.NextDouble() * (1000.99 - 0.02) + 0.02), 2),
                    CreatedDate = RandomDate(i),
                });
            }

            return new UserExpensesList() { Name = name, Expenses = expenses };
        }

        private DateTime RandomDate(int i)
        {
            var rand = new Random();

            switch (i)
            {
                case < 50:
                    return new DateTime(2021, 08, rand.Next(1, 31));
                case < 100:
                    return new DateTime(2022, 08, rand.Next(1, 31));
                case < 150:
                    if(DateTime.Now.Month == 08)
                    {
                        return new DateTime(2022, 10, rand.Next(1, 31));
                    }
                    else
                        return new DateTime(2022, DateTime.Now.Month, rand.Next(1, 31));
                default:
                    return DateTime.Now;
            }
        }
    }
}
