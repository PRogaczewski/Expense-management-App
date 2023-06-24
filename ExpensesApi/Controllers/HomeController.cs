using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.ExpensesList.Commands;
using Application.IServices.ExpensesList.Queries;
using AutoMapper;
using ExpensesApi.Models.ErrorHandlers;
using ExpensesApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IExpensesListServiceQuery _expensesListServiceQuery;

        private readonly IExpensesListServiceCommand _expensesListServiceCommand;

        private readonly IMapper _mapper;

        public HomeController(IExpensesListServiceQuery expensesListService, IMapper mapper, IExpensesListServiceCommand expensesListServiceCommand)
        {
            _expensesListServiceQuery = expensesListService;
            _mapper = mapper;
            _expensesListServiceCommand = expensesListServiceCommand;
        }

        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<string>> GetEnum()
        {
            var enums = _expensesListServiceQuery.GetCategories();

            return Ok(enums);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserExpensesListViewModel>>> Home()
        {
            try
            {
                var expensesLists = await _expensesListServiceQuery.GetExpensesLists();

                var result = new UserExpensesListViewModel();
                result.UserLists = _mapper.Map<List<UserExpensesListModelViewModel>>(expensesLists);
                result.Success = true;

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetList(int id)
        {
            try
            {
                var expensesList = await _expensesListServiceQuery.GetExpensesList(id);

                var result = _mapper.Map<UserExpensesListModelViewModel>(expensesList);

                result.Success = true;

                return Ok(expensesList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserExpensesListModel model)
        {
            try
            {
                await _expensesListServiceCommand.CreateExpensesList(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return Conflict(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UserExpensesListModel model, int id)
        {
            try
            {
                await _expensesListServiceCommand.UpdateExpensesList(model, id);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return Conflict(new ErrorHandlerResponse(ex.Message));
            }
            catch (NotFoundException ex) 
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _expensesListServiceCommand.DeleteExpensesList(id);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }
    }
}