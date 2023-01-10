using ServiceProj.DbService.Expenses;
using ServiceProj.Models.Model.Expenses;
using ServiceProj.ValidationService.Exceptions;
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
                throw new NotFoundException("Expenses list not found.");

            return models;
        }

        public void CreateExpensesList(UserExpensesModel model)
        {
            if (model is null)
                throw new BusinessException("Expense cannot be empty.", 404);

            _service.CreateExpensesList(model);
        }

        public bool CreateExpensesGoal(UserExpenseGoalDto model)
        {
            if (model is null || model.UserCategoryGoals is null)
                throw new BusinessException("Expenses goal cannot be empty.", 404);

           return _service.CreateExpensesGoal(model);
        }

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month)
        {
            if (int.Parse(year) < 1970 || int.Parse(year) > DateTime.Now.Year ||
                    int.Parse(month) < 1 || int.Parse(month) > 12)
                throw new BusinessException("Something went wring with date.", 400);

            var income = _service.GetMonthlyIncome(id, year, month);

            if (income is null)
            {
                income = new UserIncomeDto()
                {
                    UserExpensesListId = id,
                    Income = 0.0m,
                };
            }

            return income;
        }

        public void AddMonthlyIncome(UserIncomeModel income)
        {
            if (income.Income < 0)
                throw new BusinessException("Income cannot be less than zero.", 404);

            _service.AddMonthlyIncome(income); 
        }
    }
}
