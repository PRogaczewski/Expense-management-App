using DatabaseProj.DatabaseEntities.ConnectionInfo;
using ExpensesApi.Models.Categories;
using ServiceProj.Models.Model.Expenses;
using ServiceProj.Models.Model.ExpensesList;

namespace SeederProj
{
    public class ExpensesSeeder : IExpensesSeeder
    {
        public UserExpensesListSeederModel Seed(UserExpensesListSeederModel model)
        {
            model = DataSeeder(model);

            return model;
        }

        private UserExpensesListSeederModel DataSeeder(UserExpensesListSeederModel model)
        {
            var rand = new Random();

            model.Expenses = new List<UserExpensesDto>();

            for (int i = 0; i < 150; i++)
            {
                model.Expenses.Add(new UserExpensesDto()
                {
                    Category = (ExpenseCategories)rand.Next(0, 18),
                    Price = decimal.Round((decimal)(rand.NextDouble() * (500.99 - 0.02) + 0.02), 2),
                    CreatedDate = RandomDate(i),
                });
            }

            return model;
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
                    return new DateTime(2022, 10, rand.Next(1, 31));
                default:
                    return DateTime.Now;
            }
        }
    }
}
