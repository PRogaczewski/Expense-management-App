using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Dto.Models.Helpers;
using Application.Exceptions;
using Application.IServices.ExpensesList;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.ValueObjects;

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

        public async Task<IEnumerable<UserExpensesListDtoList>> GetExpensesLists()
        {
            var userExpensesLists = await _expensesListModule.GetExpensesLists();

            var result = _mapper.Map<List<UserExpensesListDtoList>>(userExpensesLists);

            return result;
        }

        public async Task<UserExpensesListDtoModel> GetExpensesList(int id)
        {
            var userExpensesList = await _expensesListModule.GetExpensesList(id);

            var result = _mapper.Map<UserExpensesListDtoModel>(userExpensesList);

            return result;
        }

        public async Task<IEnumerable<UserExpensesDto>> GetExpensesByDate(DateTimeWithIdRequestModel request)
        {
            var userExpensesList = await _expensesListModule.GetExpensesByDate(request);

            var result = _mapper.Map<IEnumerable<UserExpensesDto>>(userExpensesList.Expenses);

            return result;
        }

        public async Task<IEnumerable<DateComparer>> GetExpensesByDate(ExtendedDateTimeRequestModel request)
        {
            var model = await _expensesListModule.GetExpensesByDate(request);

            var groupedExpensesByDate = model.Expenses.GroupBy(x => new
            {
                Case = (string.IsNullOrEmpty(request.Year) || x.CreatedDate.Year.ToString() == request.Year)
                 && (string.IsNullOrEmpty(request.Month) || x.CreatedDate.Month.ToString() == request.Month) ? Comparer.First :
               (string.IsNullOrEmpty(request.SecondYear) || x.CreatedDate.Year.ToString() == request.SecondYear)
                 && (string.IsNullOrEmpty(request.SecondMonth) || x.CreatedDate.Month.ToString() == request.SecondMonth) ? Comparer.Second : Comparer.None
            })
               .Where(x => x.Key.Case != Comparer.None)
               .Select(x => new DateComparer
               {
                   Case = x.Key.Case,
                   Expenses = x.Select(e => _mapper.Map<UserExpensesDto>(e))
               });

            return groupedExpensesByDate;
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
