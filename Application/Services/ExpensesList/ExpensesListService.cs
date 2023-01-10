using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.ExpensesList;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;

namespace Application.Services.ExpensesList
{
    public class ExpensesListService : IExpensesListService
    {
        private readonly IExpensesListModule _expensesListModule;

        private readonly IMapper _mapper;

        public ExpensesListService(IExpensesListModule expensesListModule, IMapper mapper)
        {
            _expensesListModule = expensesListModule;
            _mapper = mapper;
        }

        public IEnumerable<string> GetCategories()
        {
            return _expensesListModule.GetCategories();
        }

        public IEnumerable<UserExpensesListDtoList> GetExpensesLists()
        {
            var userExpensesLists = _expensesListModule.GetExpensesLists();

            var result = _mapper.Map<List<UserExpensesListDtoList>>(userExpensesLists);

            return result;
        }

        public UserExpensesListDtoModel GetExpensesList(int id)
        {
            var userExpensesList = _expensesListModule.GetExpensesList(id);


            var result = _mapper.Map<UserExpensesListDtoModel>(userExpensesList);

            return result;
        }

        public void CreateExpensesList(UserExpensesListModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Name))
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpensesList>(model);

            _expensesListModule.CreateExpensesList(result);
        }

        public void UpdateExpensesList(UserExpensesListModel model, int id)
        {
            if (model is null || string.IsNullOrEmpty(model.Name))
                throw new BusinessException("Expenses list cannot be empty.", 404);

            var result = _mapper.Map<UserExpensesList>(model);

            _expensesListModule.UpdateExpensesList(result, id);
        }

        public void DeleteExpensesList(int id)
        {
            _expensesListModule.DeleteExpensesList(id);
        }
    }
}
