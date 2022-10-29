using DatabaseProj.DatabaseEntities.Models;
using ServiceProj.Models.Model.ExpensesList;

namespace SeederProj
{
    public interface IExpensesSeeder
    {
        public UserExpensesListSeederModel Seed(UserExpensesListSeederModel model);
    }
}
