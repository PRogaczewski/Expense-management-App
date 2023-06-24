using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.ExpensesList.Commands;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules.Commands;

namespace Application.Services.ExpensesList.Commands
{
    public class ExpensesListServiceCommand : IExpensesListServiceCommand
    {
        private readonly IExpensesListModuleCommand _expensesListModule;

        private readonly IMapper _mapper;

        public ExpensesListServiceCommand(IExpensesListModuleCommand expensesListModule, IMapper mapper)
        {
            _expensesListModule = expensesListModule;
            _mapper = mapper;
        }

        public async Task CreateExpensesList(UserExpensesListModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Name))
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpensesList>(model);

            await _expensesListModule.CreateExpensesList(result);
        }

        public async Task UpdateExpensesList(UserExpensesListModel model, int id)
        {
            if (model is null || string.IsNullOrEmpty(model.Name))
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpensesList>(model);

            await _expensesListModule.UpdateExpensesList(result, id);
        }

        public async Task DeleteExpensesList(int id)
        {
            await _expensesListModule.DeleteExpensesList(id);
        }
    }
}
