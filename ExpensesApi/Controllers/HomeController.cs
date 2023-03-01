using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.ExpensesList;
using ExpensesApi.Models.ErrorHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IExpensesListService _expensesListService;

        public HomeController(IExpensesListService expensesListService)
        {
            _expensesListService = expensesListService;
        }

        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<string>> GetEnum()
        {
            var enums = _expensesListService.GetCategories();

            return Ok(enums);
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserExpensesListDtoList>> Home()
        {
            try
            {
                var expensesLists = _expensesListService.GetExpensesLists();
                return Ok(expensesLists);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetList(int id)
        {
            try
            {
                var expensesList = _expensesListService.GetExpensesList(id);

                return Ok(expensesList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
        }

        [HttpPost]
        public ActionResult Create(UserExpensesListModel model)
        {
            try
            {
                _expensesListService.CreateExpensesList(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return Conflict(new ErrorHandler(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(UserExpensesListModel model, int id)
        {
            try
            {
                _expensesListService.UpdateExpensesList(model, id);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return Conflict(new ErrorHandler(ex.Message));
            }
            catch (NotFoundException ex) 
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _expensesListService.DeleteExpensesList(id);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
        }
    }
}