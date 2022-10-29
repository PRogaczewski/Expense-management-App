using ServiceProj.DbService.ExpensesList;
using ServiceProj.Models.Model.ExpensesList;

namespace ServiceProj.ValidationService.ExpensesList
{
    public class ExpensesListValidation : IExpensesListValidation
    {
        private readonly IExpensesListService _expensesListService;

        public ExpensesListValidation(IExpensesListService expensesListService)
        {
            _expensesListService = expensesListService;
        }

        public IEnumerable<UserExpensesListDtoList> GetExpensesLists()
        {
            var models = _expensesListService.GetExpensesLists();

            if (models == null)
            {
                throw new Exception();
            }

            return models;
        }

        public UserExpensesListDtoModel GetExpensesList(int id)
        {
            var model = _expensesListService.GetExpensesList(id);

            if (model == null)
            {
                throw new Exception();
            }

            return model;
        }

        public void CreateExpensesList(UserExpensesListModel model)
        {
            var checkName = GetExpensesLists().FirstOrDefault(x => x.Name == model.Name);

            if (checkName != null)
                throw new Exception();

            _expensesListService.CreateExpensesList(model);
        }

        public void UpdateExpensesList(UserExpensesListModel model, int id)
        {
            if (model is null)
                throw new Exception();

            if (GetExpensesLists().FirstOrDefault(x => x.Id == id) is null || GetExpensesLists().FirstOrDefault(x => x.Name == model.Name) != null)
                throw new Exception();

            _expensesListService.UpdateExpensesList(model, id);
        }

        public void DeleteExpensesList(int id)
        {
            if (_expensesListService.GetExpensesList(id) is null)
                throw new Exception();

            _expensesListService.DeleteExpensesList(id);
        }
    }
}
