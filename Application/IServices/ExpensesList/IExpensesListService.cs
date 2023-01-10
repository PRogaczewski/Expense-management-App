using Application.Dto.Models.ExpensesList;

namespace Application.IServices.ExpensesList
{ 
    public interface IExpensesListService
    {
        public IEnumerable<string> GetCategories();

        public IEnumerable<UserExpensesListDtoList> GetExpensesLists();

        public UserExpensesListDtoModel GetExpensesList(int id);

        public void CreateExpensesList(UserExpensesListModel model);

        public void UpdateExpensesList(UserExpensesListModel model, int id);

        public void DeleteExpensesList(int id);
    }
}
