using AutoMapper;
using DatabaseProj.Database.Models;
using DatabaseProj.DatabaseEntities.Models;
using Domain.DomainCore;
using Microsoft.EntityFrameworkCore;
using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.DbService.Expenses
{
    public class ExpensesService : IExpensesService
    {
        private readonly ExpensesApiDb _context;

        private readonly IMapper _mapper;

        public ExpensesService(ExpensesApiDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserExpensesDto GetExpensesList(int id)
        {
            var model = _context.Expenses
                .Include(e => e.UserExpensesList)
                .FirstOrDefault(m => m.Id == id);

            var result = _mapper.Map<UserExpensesDto>(model);

            return result;
        }

        public void CreateExpensesList(UserExpensesModel model)
        {
            var result = _mapper.Map<UserExpense>(model);

            result.CreatedDate = DateTime.Now;

            _context.Expenses.Add(result);
            _context.SaveChanges();
        }

        public bool CreateExpensesGoal(UserExpenseGoalDto model)
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
                result.CreateDate = DateTime.Now;

                _context.UserExpensesGoals.Add(result);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month)
        {
            var model = _context.UserIncomes.FirstOrDefault(m => m.UserExpensesListId == id 
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            var result = _mapper.Map<UserIncomeDto>(model);

            return result;
        }

        public void AddMonthlyIncome(UserIncomeModel income)
        {
            var currentMonthIncomes = _context.UserIncomes.FirstOrDefault(u => u.UserExpensesListId == income.UserExpensesListId && u.CreatedDate.Month==DateTime.Now.Month);

            if (currentMonthIncomes != null)
            {
                currentMonthIncomes.Income += income.Income;
                currentMonthIncomes.UpdateDate = DateTime.Now;
            }
            else
            {
                var newIncome = _mapper.Map<UserIncome>(income);
                newIncome.CreatedDate = DateTime.Now;

                _context.UserIncomes.Add(newIncome);
            }

            _context.SaveChanges();
        }
    }
}
