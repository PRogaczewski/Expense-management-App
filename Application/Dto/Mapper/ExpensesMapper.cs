using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using AutoMapper;
using Domain.Entities.Models;

namespace Application.Dto.Mapper
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
