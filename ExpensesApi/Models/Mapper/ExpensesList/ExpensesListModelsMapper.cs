using Application.Dto.Models.ExpensesList;
using AutoMapper;
using ExpensesApi.Models.ViewModels;

namespace ExpensesApi.Models.Mapper.ExpensesList
{
    public class ExpensesListModelsMapper : Profile
    {
        public ExpensesListModelsMapper()
        {
            CreateMap<UserExpensesListResponse, UserExpensesSummaryViewModel>();

            CreateMap<UserExpensesListDtoModel, UserExpensesListModelViewModel>();
        }
    }
}
