using ExpensesApi.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using ServiceProj.AplicationService.ExpensesList;
using ServiceProj.Models.Model.ExpensesList;

namespace ExpensesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserExpensesListService _expensesListService;

        public HomeController(IUserExpensesListService expensesListService)
        {
            _expensesListService = expensesListService;
        }

        [HttpGet("enum")]
        public ActionResult<IEnumerable<string>> GetEnum()
        {
            var enums = Enum.GetNames(typeof(ExpenseCategories));

            return Ok(enums);
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserExpensesListDtoList>> Home()
        {
           var expensesLists = _expensesListService.GetExpensesLists();

            return Ok(expensesLists);
        }

        [HttpGet("{id}")]
        public ActionResult GetList(int id)
        {
            var expensesList = _expensesListService.GetExpensesList(id);

            return Ok(expensesList);
        }

        [HttpPost]
        public ActionResult Create(UserExpensesListModel model)
        {
            _expensesListService.CreateExpensesList(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Update(UserExpensesListModel model, int id)
        {
            _expensesListService.UpdateExpensesList(model, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _expensesListService.DeleteExpensesList(id);

            return Ok();
        }

    }
}