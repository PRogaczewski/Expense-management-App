using Domain.Entities.Base;
using Domain.ValueObjects;

namespace ExpensesApi.Models.ViewModels
{
    public class UserExpensesViewModel
    {
        private UserExpensesViewModel(PagedList<UserExpenseResponseDto> items)
        {
            Items = items;
        }

        public PagedList<UserExpenseResponseDto> Items { get; set; }

        public static UserExpensesViewModel Create(PagedList<UserExpenseResponseDto> items)
        {
            return new(items);
        }
    }
}
