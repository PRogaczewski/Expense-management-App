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

        public async Task CreateExpense(UserExpensesModel model)
        {
            if (model is null)
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpense>(model);

            await _expensesModule.CreateExpense(result);
        }

        public async Task<bool> CreateExpensesGoal(UserExpenseGoalDto model)
        {
            if (model is null || model.UserCategoryGoals is null)
                throw new BusinessException("Expenses goal cannot be empty.", 404);

            var result = _mapper.Map<UserExpenseGoal>(model);

            return await _expensesModule.CreateExpensesGoal(result);
        }

        public async Task<UserIncomeDto> GetMonthlyIncome(int id, string year, string month)
        {
            if (int.Parse(year) < 1970 || int.Parse(year) > DateTime.Now.Year ||
                    int.Parse(month) < 1 || int.Parse(month) > 12)
                throw new BusinessException("Wrong date.", 400);

            var monthlyIncome = await _expensesModule.GetMonthlyIncome(id, year, month);

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

        public async Task AddMonthlyIncome(UserIncomeModel model)
        {
            if (model.Income < 0)
                throw new BusinessException("Income cannot be less than zero.", 404);
            else if(model is null)
                throw new BusinessException("Income cannot be empty.", 404);

            var income = _mapper.Map<UserIncome>(model);

            await _expensesModule.AddMonthlyIncome(income);
        }
        #endregion
    }
}
