using ServiceProj.DbService.ExpensesList;
using ServiceProj.Models.Model.ExpensesList;
using ServiceProj.ValidationService.Exceptions;

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
                throw new NotFoundException("Expenses lists not found.");

            return models;
        }

        public UserExpensesListDtoModel GetExpensesList(int id)
        {
            var model = _expensesListService.GetExpensesList(id);

            if (model == null)
                throw new NotFoundException("Expenses list not found.");

            return model;
        }

        public void CreateExpensesList(UserExpensesListModel model)
        {
            var checkName = GetExpensesLists().FirstOrDefault(x => x.Name == model.Name);

            if (checkName != null)
                throw new BusinessException("List with this name exists.", 409);

            _expensesListService.CreateExpensesList(model);
        }

        public void UpdateExpensesList(UserExpensesListModel model, int id)
        {
            if (model is null)
                throw new BusinessException("Expenses list cannot be empty.", 404);

            if (GetExpensesLists().FirstOrDefault(x => x.Id == id) is null || GetExpensesLists().FirstOrDefault(x => x.Name == model.Name) != null)
                throw new BusinessException("Current list already exists.", 409);

            _expensesListService.UpdateExpensesList(model, id);
        }

        public void DeleteExpensesList(int id)
        {
            if (_expensesListService.GetExpensesList(id) is null)
                throw new NotFoundException("Current expenses list can not be found.");

            _expensesListService.DeleteExpensesList(id);
        }
    }
}
