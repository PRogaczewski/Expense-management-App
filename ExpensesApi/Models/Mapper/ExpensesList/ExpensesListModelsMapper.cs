using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using AutoMapper;
using Domain.Categories;
using ExpensesApi.Models.ViewModels;

namespace ExpensesApi.Models.Mapper.ExpensesList
{
    public class ExpensesListModelsMapper : Profile
    {
        public ExpensesListModelsMapper()
        {
            CreateMap<UserExpensesListResponse, UserExpensesSummaryViewModel>();

            CreateMap<UserExpensesListDtoModel, UserExpensesListModelViewModel>();

            CreateMap<UserExpensesDto, UserExpensesViewModel>()
                .ForMember(u => u.Category, e => e.MapFrom(o => o.Category.GetEnumDisplayName()));
        }
    }
}
