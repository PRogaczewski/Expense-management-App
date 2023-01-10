using Application.Dto.Models.Expenses;
using Application.Exceptions;
using Application.IServices.Expenses;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;

namespace Application.Services.ExpensesList
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesModule _expensesModule;

        private readonly IMapper _mapper;

        public ExpensesService(IExpensesModule expensesValidation, IMapper mapper)
        {
            _expensesModule = expensesValidation;
            _mapper = mapper;
        }

        #region ExpensesService
        //public UserExpensesDto GetExpensesList(int id)
        //{
        //    var expensesList = _expensesModule.GetExpensesList(id);

        //    var result = _mapper.Map<UserExpensesDto>(expensesList);

        //    return result;
        //}

        public void CreateExpense(UserExpensesModel model)
        {
            if (model is null)
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpense>(model);

            _expensesModule.CreateExpense(result);
        }

        public bool CreateExpensesGoal(UserExpenseGoalDto model)
        {
            if (model is null || model.UserCategoryGoals is null)
                throw new BusinessException("Expenses goal cannot be empty.", 404);

            var result = _mapper.Map<UserExpenseGoal>(model);

            return _expensesModule.CreateExpensesGoal(result);
        }

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month)
        {
            if (int.Parse(year) < 1970 || int.Parse(year) > DateTime.Now.Year ||
                    int.Parse(month) < 1 || int.Parse(month) > 12)
                throw new BusinessException("Wrong date.", 400);

            var monthlyIncome = _expensesModule.GetMonthlyIncome(id, year, month);

            var result = _mapper.Map<UserIncomeDto>(monthlyIncome);

            if (result is null)
            {
                result = new UserIncomeDto()
                {
                    UserExpensesListId = id,
                    Income = 0.0m,
                };
            }

            return result;
        }

        public void AddMonthlyIncome(UserIncomeModel model)
        {
            if (model.Income < 0)
                throw new BusinessException("Income cannot be less than zero.", 404);
            else if(model is null)
                throw new BusinessException("Income cannot be empty.", 404);

            var income = _mapper.Map<UserIncome>(model);

            _expensesModule.AddMonthlyIncome(income);
        }
        #endregion
    }
}
