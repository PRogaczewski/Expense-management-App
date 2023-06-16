namespace ExpensesApi.Models.ViewModels
{
    public class UserExpensesListViewModel : ViewModelAbstract
    {
        public IEnumerable<UserExpensesListModelViewModel> UserLists { get; set; }

        public UserExpensesListViewModel()
        {
            UserLists = new List<UserExpensesListModelViewModel>();
        }
    }
}
