using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses;
using Application.IServices.ExpensesList;
using ExpensesApi.Models.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesListController : Controller
    {
        private readonly IExpensesService _service;

        private readonly IExpensesListService _expensesListService;

        private readonly IUserExpensesAnalysisService _analysisService;

        private readonly IUserInitialData _userInitialData;

        public ExpensesListController(IExpensesService service, IUserExpensesAnalysisService analysisService, IExpensesListService expensesListService, IUserInitialData userInitialData)
        {
            _service = service;
            _analysisService = analysisService;
            _expensesListService = expensesListService;
            _userInitialData = userInitialData;
        }

        [HttpGet("{id}")]
        public ActionResult<UserExpensesListResponse> Home(int id)
        {
            //var incomes = _analysisService.TotalIncomesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            //var outgoings = _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            //var totalByCategories = _analysisService.ExpensesByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString()).ToDictionary(k => k.Key, v => v.Value);
            //var currentWeekByCategories = _analysisService.ExpensesByCategoryCurrentWeek(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());    

            //var result = new MainExpensesViewModel();

            //if (DateTime.Now.Day >= 25)
            //{
            //    var compareToLastMonth = _analysisService.CompareByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), (DateTime.Now.Month - 1).ToString());

            //    result = new ComparedDateExpensesViewModel()
            //    {
            //        CompareLastMonthByCategories = compareToLastMonth
            //    };
            //}

            //if (_expensesListService.GetExpensesList(id).UserGoals.Exists(u=>u.MonthChosenForGoal.Month.ToString() + u.MonthChosenForGoal.Year.ToString()==DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()))
            //{
            //    var userGoals = _analysisService.MonthlyGoals(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            //    result = new UserGoalsExpensesViewModel()
            //    {
            //        UserGoals = userGoals[0],
            //        UserExpenses = userGoals[1],
            //        Result = userGoals[2],
            //    };
            //}


            //result.Incomes = incomes;
            //result.Outgoings = outgoings;
            //result.MonthlyResult = incomes - outgoings;
            //result.TotalMonthByCategories = totalByCategories;
            //result.CurrentWeekByCategories = currentWeekByCategories;
            try
            {
                var result = _userInitialData.GetUserInitialData(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
           
        }

        [HttpGet("TotalInMonth/{id}")]
        public ActionResult GetMonthTotal(int id, string? year, string month)
        {
            try
            {
                if (string.IsNullOrEmpty(year))
                    year = DateTime.Now.Year.ToString();

                var total = _analysisService.TotalExpensesMonth(id, year, month);

                return Ok(total);
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
           
        }

        [HttpGet("TotalInYear/{id}")]
        public ActionResult GetMonthTotal(int id, string year)
        {
            try
            {
                var total = _analysisService.TotalExpensesYear(id, year);

                return Ok(total);
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            } 
        }

        [HttpGet("TotalInMonthByCategories/{id}")]
        public ActionResult GetMonthTotalByCategories(int id, string? year, string? month)
        {
            if (string.IsNullOrEmpty(year))
                year = DateTime.Now.Year.ToString();

            if (string.IsNullOrEmpty(month))
                month = DateTime.Now.Year.ToString();

            var total = _analysisService.ExpensesByCategoryMonth(id, year, month).ToDictionary(k => k.Key, v => v.Value);

            return Ok(total);
        }

        [HttpGet("TotalInYearByCategories/{id}")]
        public ActionResult GetYearTotalByCategories(int id, string? year)
        {
            if (string.IsNullOrEmpty(year))
                year = DateTime.Now.Year.ToString();

            var total = _analysisService.ExpensesByCategoryYear(id, year).ToDictionary(k => k.Key, v => v.Value);

            return Ok(total);
        }

        [HttpGet("CompareMonths/{id}")]
        public ActionResult GetCompareMonths(int id, string firstYear, string secondYear, string firstMonth, string secondMonth)
        {
            var total = _analysisService.CompareByCategoryMonth(id, firstYear, secondYear, firstMonth, secondMonth).ToDictionary(k => k.Key, v => v.Value);

            return Ok(total);
        }

        [HttpGet("CompareYears/{id}")]
        public ActionResult GetCompareYears(int id, string firstYear, string secondYear)
        {
            var total = _analysisService.CompareByCategoryYear(id, firstYear, secondYear).ToDictionary(k => k.Key, v => v.Value);

            return Ok(total);
        }

        [HttpPost]
        public ActionResult Create(UserExpensesModel model)
        {
            try
            {
                _service.CreateExpense(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
            
        }

        [HttpPost("ExpensesMonthlyGoal")]
        public ActionResult CreateMonthlyGoal(UserExpenseGoalDto model)
        {
            try
            {
                bool isSuccessfully = _service.CreateExpensesGoal(model);

                return Ok(isSuccessfully);
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
           
        }

        [HttpGet("ExpensesMonthlyGoal/{id}")]
        public ActionResult GetMonthlyGoal(int id, string year, string month)
        {
            var total = _analysisService.MonthlyGoals(id, year, month).ToList();

            if (!total.Any())
                return NotFound(new ErrorHandler("User goals for current month not found."));

            return Ok(total);
        }

        [HttpPost("UserIncome")]
        public ActionResult AddIncome(UserIncomeModel model)
        {
            try
            {
                _service.AddMonthlyIncome(model);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return NotFound(new ErrorHandler(ex.Message));
            }
        }
    }
}
