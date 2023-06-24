using Application.Dto.Models.Expenses;
using Application.Exceptions;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses.Commands;
using Application.IServices.Expenses.Queries;
using Application.IServices.ExpensesList.Queries;
using AutoMapper;
using Domain.ValueObjects;
using ExpensesApi.Models.ErrorHandlers;
using ExpensesApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExpensesListController : Controller
    {
        private readonly IExpensesServiceCommand _serviceCommand;

        private readonly IExpensesListServiceQuery _expensesListService;

        private readonly IUserExpensesAnalysisService _analysisService;

        private readonly IUserInitialData _userInitialData;

        private readonly IMapper _mapper;

        public ExpensesListController(IUserExpensesAnalysisService analysisService, IUserInitialData userInitialData, IMapper mapper, IExpensesServiceCommand serviceCommand, IExpensesListServiceQuery expensesListService)
        {
            _analysisService = analysisService;
            _userInitialData = userInitialData;
            _mapper = mapper;
            _serviceCommand = serviceCommand;
            _expensesListService = expensesListService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserExpensesSummaryViewModel>> Home(int id)
        {
            try
            {
                var result = await _userInitialData.GetUserInitialData(id);

                return Ok(_mapper.Map<UserExpensesSummaryViewModel>(result));
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
           
        }

        [HttpGet("TotalInMonth/{id}")]
        public async Task<ActionResult<ExpensesListValueViewModel>> GetMonthTotal(int id, string? year, string month)
        {
            try
            {
                if (string.IsNullOrEmpty(year))
                    year = DateTime.Now.Year.ToString();

                var total = await _analysisService.TotalExpensesMonth(id, year, month);

                return Ok(ExpensesListValueViewModel.CreateViewModel(total));
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
           
        }

        [HttpGet("TotalInYear/{id}")]
        public async Task<ActionResult<ExpensesListValueViewModel>> GetYearTotal(int id, string year)
        {
            try
            {
                var total = await _analysisService.TotalExpensesYear(id, year);

                return Ok(ExpensesListValueViewModel.CreateViewModel(total));
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            } 
        }

        [HttpGet("TotalInMonthByCategories/{id}")]
        public async Task<ActionResult<MonthSummaryViewModel>> GetMonthTotalByCategories(int id, string? year, string? month)
        {
            if (string.IsNullOrEmpty(year))
                year = DateTime.Now.Year.ToString();

            if (string.IsNullOrEmpty(month))
                month = DateTime.Now.Month.ToString();

            try
            {
                var total = (await _analysisService.ExpensesByCategoryMonth(id, year, month)).ToDictionary(k => k.Key, v => v.Value);
                var prevMonth = await _analysisService.TotalExpensesMonth(id, year, (int.Parse(month) - 1).ToString());

                var result = MonthSummaryViewModel.CreateViewModel(prevMonth, total);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpGet("TotalInYearByCategories/{id}")]
        public async Task<IActionResult> GetYearTotalByCategories(int id, string? year)
        {
            if (string.IsNullOrEmpty(year))
                year = DateTime.Now.Year.ToString();

            try
            {
                var total = (await _analysisService.ExpensesByCategoryYear(id, year)).ToDictionary(k => k.Key, v => v.Value);

                return Ok(total);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpGet("CompareMonths/{id}")]
        public async Task<ActionResult> GetCompareMonths(int id, string firstYear, string secondYear, string firstMonth, string secondMonth)
        {
            try
            {
                var total = (await _analysisService.CompareByCategoryMonth(id, firstYear, secondYear, firstMonth, secondMonth)).ToDictionary(k => k.Key, v => v.Value);

                return Ok(total);
            }
            catch (Exception ex)    
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpGet("CompareYears/{id}")]
        public async Task<ActionResult> GetCompareYears(int id, string firstYear, string secondYear)
        {
            try
            {
                var total = (await _analysisService.CompareByCategoryYear(id, firstYear, secondYear)).ToDictionary(k => k.Key, v => v.Value);

                return Ok(total);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message)); ;
            }
        }

        [HttpGet("Expenses")]
        public async Task<ActionResult<IEnumerable<UserExpensesViewModel>>> GetExpenses(int id, int? page, int? pagesize, CancellationToken token)
        {
            try
            {
                var model = await _expensesListService.GetExpenses(id, page, pagesize, token);

                var result = _mapper.Map<IEnumerable<UserExpensesViewModel>>(model).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message)); ;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserExpensesModel model)
        {
            try
            {
                await _serviceCommand.CreateExpense(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
            
        }

        [HttpPost("ExpensesMonthlyGoal")]
        public async Task<ActionResult> CreateMonthlyGoal(UserExpenseGoalDto model)
        {
            try
            {
                await _serviceCommand.CreateExpensesGoal(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
           
        }

        [HttpGet("ExpensesMonthlyGoal/{id}")]
        public async Task<ActionResult> GetMonthlyGoal(int id, string year, string month)
        {
            try
            {
                var total = (await _analysisService.MonthlyGoals(id, year, month)).ToDictionary(k => k.Keys, v => v.Values);

                if (!total.Any())
                    return NotFound(new ErrorHandlerResponse("User goals for current month not found."));

                return Ok(total);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }

        [HttpPost("UserIncome")]
        public async Task<ActionResult> AddIncome(UserIncomeModel model)
        {
            try
            {
                await _serviceCommand.AddMonthlyIncome(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandlerResponse(ex.Message));
            }
        }
    }
}
