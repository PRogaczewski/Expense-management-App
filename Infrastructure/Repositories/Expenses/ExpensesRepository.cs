using Application.Exceptions;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.Expenses
{
    public class ExpensesRepository : IExpensesModule
    {
        private readonly ExpenseDbContext _context;

        private readonly IMapper _mapper;

        public ExpensesRepository(ExpenseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public UserExpense GetExpensesList(int id)
        //{
        //    var model = _context.Expenses
        //        .Include(e => e.UserExpensesList)
        //        .FirstOrDefault(m => m.Id == id);

        //    if (model is null)
        //        throw new NotFoundException("User expenses list not found.");

        //    return model;
        //}

        public void CreateExpense(UserExpense model)
        {
            var result = _mapper.Map<UserExpense>(model);

            result.CreatedDate = DateTime.Now;

            _context.Expenses.Add(result);
            _context.SaveChanges();
        }

        public bool CreateExpensesGoal(UserExpenseGoal model)
        {
            var result = _mapper.Map<UserExpenseGoal>(model);

            bool IsAnySameCategory = false;

            foreach (var userGoal in result.UserCategoryGoals)
            {
                var test = _context.UserExpensesGoals.Where(u => u.MonthChosenForGoal == result.MonthChosenForGoal && u.UserExpensesListId == result.UserExpensesListId).ToList();

                IsAnySameCategory = test.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category));
            }

            if (!IsAnySameCategory)
            {
                result.CreatedDate = DateTime.Now;

                _context.UserExpensesGoals.Add(result);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public UserIncome GetMonthlyIncome(int id, string year, string month)
        {
            var model = _context.UserIncomes.FirstOrDefault(m => m.UserExpensesListId == id 
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            return model;
        }

        public void AddMonthlyIncome(UserIncome income)
        {
            var currentMonthIncomes = _context.UserIncomes.FirstOrDefault(u => u.UserExpensesListId == income.UserExpensesListId && u.CreatedDate.Month==DateTime.Now.Month);

            if (currentMonthIncomes != null)
            {
                currentMonthIncomes.Income += income.Income;
                currentMonthIncomes.UpdateDate = DateTime.Now;
            }
            else
            {
                income.CreatedDate = DateTime.Now;

                _context.UserIncomes.Add(income);
            }

            _context.SaveChanges();
        }
    }
}
