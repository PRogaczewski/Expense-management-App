using Application.Dto.Models.ExpensesList;
using AutoMapper;
using ExpensesApi.Models.ViewModels;

namespace ExpensesApi.Models.Mapper.Home
{
    public class HomeModelsMapper : Profile
    {
        public HomeModelsMapper()
        {
            CreateMap<UserExpensesListDtoList, UserExpensesListViewModel>();

            CreateMap<UserExpensesListDtoModel, UserExpensesListModelViewModel>();
        }
    }
}
