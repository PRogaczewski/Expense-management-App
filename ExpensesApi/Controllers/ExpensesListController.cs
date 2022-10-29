using ExpensesApi.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using ServiceProj.AplicationService.Expenses;
using ServiceProj.AplicationService.ExpensesList;
using ServiceProj.Models.AnalysisModels;
using ServiceProj.Models.Model.Expenses;

namespace ExpensesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesListController : Controller
    {
        private readonly IUserExpensesService _service;

        private readonly IUserExpensesListService _expensesListService;

        private readonly IUserExpensesAnalysisService _analysisService;

        public ExpensesListController(IUserExpensesService service, IUserExpensesAnalysisService analysisService, IUserExpensesListService expensesListService)
        {
            _service = service;
            _analysisService = analysisService;
            _expensesListService = expensesListService;
        }

        [HttpGet("{id}")]
        public ActionResult<HomeResultModel> Home(int id)
        {
            var incomes = _analysisService.TotalIncomesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            var outgoings = _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            var totalByCategories = _analysisService.ExpensesByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString()).ToDictionary(k => k.Key, v => v.Value);
            var currentWeekByCategories = _analysisService.ExpensesByCategoryCurrentWeek(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());    

            var result = new HomeResultModel();

            if (DateTime.Now.Day >= 25)
            {
                var compareToLastMonth = _analysisService.CompareByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), (DateTime.Now.Month - 1).ToString());

                result = new HomeResultModelCompare()
                {
                    CompareLastMonthByCategories = compareToLastMonth
                };
            }

            if (_expensesListService.GetExpensesList(id).UserGoals.Exists(u=>u.MonthChosenForGoal.Month.ToString() + u.MonthChosenForGoal.Year.ToString()==DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()))
            {
                var userGoals = _analysisService.MonthlyGoals(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

                result = new HomeResultModelGoals()
                {
                    UserGoals = userGoals[0],
                    UserExpenses = userGoals[1],
                    Result = userGoals[2],
                };
            }


            result.Incomes = incomes;
            result.Outgoings = outgoings;
            result.MonthlyResult = incomes - outgoings;
            result.TotalMonthByCategories = totalByCategories;
            result.CurrentWeekByCategories = currentWeekByCategories;

            return Ok(result);
        }

        [HttpGet("TotalInMonth/{id}")]
        public ActionResult GetMonthTotal(int id, string? year, string month)
        {
            if (string.IsNullOrEmpty(year))
                year = DateTime.Now.Year.ToString();

            var total = _analysisService.TotalExpensesMonth(id, year, month);

            return Ok(total);
        }

        [HttpGet("TotalInYear/{id}")]
        public ActionResult GetMonthTotal(int id, string year)
        {
            var total = _analysisService.TotalExpensesYear(id, year);

            return Ok(total);
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
            _service.CreateExpensesList(model);

            return Ok();
        }

        [HttpPost("ExpensesMonthlyGoal")]
        public ActionResult CreateMonthlyGoal(UserExpenseGoalDto model)
        {
            _service.CreateExpensesGoal(model);

            return Ok();
        }

        [HttpGet("ExpensesMonthlyGoal/{id}")]
        public ActionResult GetMonthlyGoal(int id, string year, string month)
        {
            if (!_expensesListService.GetExpensesList(id).UserGoals.Any())
                return NotFound();

            var total = _analysisService.MonthlyGoals(id, year, month).ToList();

            return Ok(total);
        }

        [HttpPost("UserIncome")]
        public ActionResult AddIncome(UserIncomeModel model)
        {
            _service.AddMonthlyIncome(model);

            return Ok();
        }
    }
}
