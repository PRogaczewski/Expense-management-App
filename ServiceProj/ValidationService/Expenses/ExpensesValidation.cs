using ServiceProj.DbService.Expenses;
using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.ValidationService.Expenses
{
    public class ExpensesValidation : IExpensesValidation
    {
        private readonly IExpensesService _service;
        public ExpensesValidation(IExpensesService service)
        {
            _service = service;
        }

        public UserExpensesDto GetExpensesList(int id)
        {
            var models = _service.GetExpensesList(id);

            if (models is null)
                throw new Exception();

            return models;
        }

        public void CreateExpensesList(UserExpensesModel model)
        {
            if (model is null)
                throw new Exception();

            _service.CreateExpensesList(model);
        }

        public void CreateExpensesGoal(UserExpenseGoalDto model)
        {
            if (model is null || model.UserCategoryGoals is null)
                throw new Exception();

            _service.CreateExpensesGoal(model);
        }

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month)
        {
            try
            {
                if(int.Parse(year) < 1970 || int.Parse(year) > DateTime.Now.Year ||
                    int.Parse(month) < 1 || int.Parse(month) > 12)
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }

            var income = _service.GetMonthlyIncome(id, year, month);

            if (income is null)
                throw new Exception();

            return income;
        }

        public void AddMonthlyIncome(UserIncomeModel income)
        {
            if (income.Income < 0)
                throw new Exception();

            _service.AddMonthlyIncome(income);
        }
    }
}
