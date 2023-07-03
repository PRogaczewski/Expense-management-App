using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Dto.Models.Helpers;
using Application.IServices.ExpensesList.Queries;
using AutoMapper;
using Domain.Modules.Queries;
using Domain.ValueObjects;

namespace Application.Services.ExpensesList.Queries
{
    public class ExpensesListServiceQuery : IExpensesListServiceQuery
    {
        private readonly IExpensesListModuleQuery _expensesListModule;

        private readonly IMapper _mapper;

        public ExpensesListServiceQuery(IExpensesListModuleQuery expensesListModule, IMapper mapper)
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
    }
}
