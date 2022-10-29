using AutoMapper;
using DatabaseProj.Database.Models;
using DatabaseProj.DatabaseEntities.Models;
using ServiceProj.Models.Model.Expenses;
using ServiceProj.Models.Model.ExpensesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Mapper
{
    public class ExpensesMapper : Profile
    {
        public ExpensesMapper()
        {
            #region Expenses
            CreateMap<UserExpense, UserExpensesDto>();

            CreateMap<UserExpensesModel, UserExpense>();

            CreateMap<UserExpensesDto, UserExpense>();

            CreateMap<UserExpense, UserExpensesDto>();

            CreateMap<UserExpenseGoalDto, UserExpenseGoal>();
            #endregion

            #region ExpensesList
            CreateMap<UserExpensesList, UserExpensesListDtoList>()
               .ForMember(u => u.Count, e => e.MapFrom(o => o.Expenses.Count()));

            CreateMap<UserExpensesList, UserExpensesListDtoModel>();

            CreateMap<UserExpensesListDto, UserExpensesList>();

            CreateMap<UserExpensesListModel, UserExpensesList>();

            CreateMap<UserExpensesListDtoModel, UserExpensesList>();

            CreateMap<UserExpensesListModel, UserExpensesListSeederModel>();

            CreateMap<UserExpensesListSeederModel, UserExpensesList>();
            #endregion

            #region ExpensesMonthlyGoals
            CreateMap<UserExpenseGoalDto, UserExpenseGoal>();
            
            CreateMap<UserExpenseGoal, UserExpenseGoalDto>();

            CreateMap<UserGoal, UserGoalDto>();

            CreateMap<UserGoalDto, UserGoal>();
            #endregion

            #region Incomes
            CreateMap<UserIncomeModel, UserIncome>();

            CreateMap<UserIncome, UserIncomeDto>();
            #endregion
        }
    }
}
