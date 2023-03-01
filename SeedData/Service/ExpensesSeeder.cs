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
            var incomes = new List<UserIncome>();
            var goals = new List<UserExpenseGoal>();

            for (int i = 0; i < 150; i++)
            {
                var date = RandomDate(i);
                expenses.Add(new UserExpense()
                {
                    Category = (ExpenseCategories)rand.Next(0, 18),
                    Price = decimal.Round((decimal)(rand.NextDouble() * (1000.99 - 0.02) + 0.02), 2),
                    CreatedDate = date,
                });

                if (i % 4 == 0)
                    incomes.Add(new UserIncome() { Income = decimal.Round((decimal)(rand.NextDouble() * (3200.99 - 200.01) + 50.55), 2), CreatedDate = date });
            }

            for (int i = 0; i < 5; i++)
            {
                var userGoal = new UserExpenseGoal()
                {
                    UserCategoryGoals = new List<UserGoal>()
                    {
                        new UserGoal()
                        {
                            Category = (ExpenseCategories)rand.Next(0, 18),
                            Limit = decimal.Round((decimal)(rand.NextDouble() * (900.99 - 250.01) + 15.55), 2)
                        }
                    },
                    MonthChosenForGoal = DateTime.Now.Month == 08 ? new DateTime(2023, 10, rand.Next(1, 31)) : new DateTime(2023, DateTime.Now.Month, DateTime.Now.Month == 2 ? rand.Next(1, 28) : rand.Next(1, 31)),
                    CreatedDate = DateTime.Now,
                };

                goals.Add(userGoal);
            }

            return new UserExpensesList() { Name = name, Expenses = expenses, UserIncomes = incomes, UserGoals = goals };
        }

        private DateTime RandomDate(int i)
        {
            var rand = new Random();

            switch (i)
            {
                case < 50:
                    return new DateTime(2021, 08, rand.Next(1, 31));
                case < 100:
                    return new DateTime(2023, 08, rand.Next(1, 31));
                case < 150:
                    if(DateTime.Now.Month == 08)
                    {
                        return new DateTime(2023, 10, rand.Next(1, 31));
                    }
                    else
                        return new DateTime(2023, DateTime.Now.Month, DateTime.Now.Month==2 ? rand.Next(1, 28)  : rand.Next(1, 31));
                default:
                    return DateTime.Now;
            }
        }
    }
}
