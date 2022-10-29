using ServiceProj.Models.Model.ExpensesList;
using ServiceProj.ValidationService.ExpensesList;

namespace ServiceProj.AplicationService.ExpensesList
{
    public class UserExpensesListService : IUserExpensesListService
    {
        private readonly IExpensesListValidation _validation;

        public UserExpensesListService(IExpensesListValidation validation)
        {
            _validation = validation;
        }

        public IEnumerable<UserExpensesListDtoList> GetExpensesLists()
        {
            try
            {
                var result = _validation.GetExpensesLists().ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserExpensesListDtoModel GetExpensesList(int id)
        {
            try
            {
                var result = _validation.GetExpensesList(id);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateExpensesList(UserExpensesListModel model)
        {
            try
            {
                _validation.CreateExpensesList(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateExpensesList(UserExpensesListModel model, int id)
        {
            try
            {
                _validation.UpdateExpensesList(model, id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteExpensesList(int id)
        {
            try
            {
                _validation.DeleteExpensesList(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
