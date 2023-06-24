using Application.Dto.Models.Expenses;
using Application.Exceptions;
using Application.IServices.Expenses.Commands;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules.Commands;

namespace Application.Services.Expenses.Commands
{
    public class ExpensesServiceCommand : IExpensesServiceCommand
    {
        private readonly IExpensesModuleCommand _expensesModule;

        private readonly IMapper _mapper;

        public ExpensesServiceCommand(IExpensesModuleCommand expensesValidation, IMapper mapper)
        {
            _expensesModule = expensesValidation;
            _mapper = mapper;
        }

        public async Task CreateExpense(UserExpensesModel model)
        {
            if (model is null)
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpense>(model);

            await _expensesModule.CreateExpense(result);
        }

        public async Task CreateExpensesGoal(UserExpenseGoalDto model)
        {
            if (model is null || model.UserCategoryGoals is null)
                throw new BusinessException("Expenses goal cannot be empty.", 404);

            var result = _mapper.Map<UserExpenseGoal>(model);

            await _expensesModule.CreateExpensesGoal(result);
        }

        public async Task AddMonthlyIncome(UserIncomeModel model)
        {
            if (model.Income < 0)
                throw new BusinessException("Income cannot be less than zero.", 404);
            else if (model is null)
                throw new BusinessException("Income cannot be empty.", 404);

            var income = _mapper.Map<UserIncome>(model);

            await _expensesModule.AddMonthlyIncome(income);
        }
    }
}
