using Application.Dto.Models.ExpensesList;

namespace ExpensesApi.Models.ViewModels
{
    public class UserExpensesListViewModel : ViewModelAbstract
    {
        //public int Id { get; set; }

        //public string Name { get; set; }

        //public int UserApplicationId { get; set; }

        //public int Count { get; set; }

        public IEnumerable<UserExpensesListDtoList> UserLists { get; set; }

        public UserExpensesListViewModel()
        {
            UserLists = new List<UserExpensesListDtoList>();
        }
    }
}
