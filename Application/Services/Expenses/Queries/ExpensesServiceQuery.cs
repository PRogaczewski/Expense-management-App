using Application.Dto.Models.Expenses;
using Application.Exceptions;
using Application.IServices.Expenses.Queries;
using AutoMapper;
using Domain.Modules.Queries;
using Domain.ValueObjects;

namespace Application.Services.Expenses.Queries
{
    public class ExpensesServiceQuery : IExpensesServiceQuery
    {
        private readonly IExpensesModuleQuery _expensesModule;

        private readonly IMapper _mapper;

        public ExpensesServiceQuery(IExpensesModuleQuery expensesValidation, IMapper mapper)
        {
            _expensesModule = expensesValidation;
            _mapper = mapper;
        }

        public async ValueTask<UserIncomeDto> GetMonthlyIncome(int id, string year, string month)
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
    }
}
