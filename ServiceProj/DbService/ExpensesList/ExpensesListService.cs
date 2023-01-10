using AutoMapper;
using DatabaseProj.DatabaseEntities.Models;
using Domain.DomainCore;
using Microsoft.EntityFrameworkCore;
using SeederProj;
using ServiceProj.Models.Model.ExpensesList;
using ServiceProj.ValidationService.Exceptions;

namespace ServiceProj.DbService.ExpensesList
{
    public class ExpensesListService : IExpensesListService
    {
        private readonly ExpensesApiDb _context;

        private readonly IMapper _mapper;

        private readonly IExpensesSeeder _seeder;

        public ExpensesListService(ExpensesApiDb context, IMapper mapper, IExpensesSeeder seeder)
        {
            _context = context;
            _mapper = mapper;
            _seeder = seeder;
        }

        public IEnumerable<UserExpensesListDtoList> GetExpensesLists()
        {
            var models = _context.ExpensesLists
                .Include(e => e.Expenses
                .OrderByDescending(o => o.CreatedDate.Year)
                .ThenByDescending(o => o.CreatedDate.Month))
                .Include(e=>e.UserGoals)
                .ThenInclude(u => u.UserCategoryGoals)
                .OrderBy(e => e.CreateDate);

            var result = _mapper.Map<List<UserExpensesListDtoList>>(models);

            return result.ToList();
        }

        public UserExpensesListDtoModel GetExpensesList(int id)
        {
            var model = _context.ExpensesLists
                .Include(e => e.Expenses
                .OrderByDescending(o => o.CreatedDate.Year)
                .ThenByDescending(o => o.CreatedDate.Month))
                .Include(e => e.UserGoals)
                .ThenInclude(u=>u.UserCategoryGoals)
                .FirstOrDefault(e => e.Id == id);

            var result = _mapper.Map<UserExpensesListDtoModel>(model);

            return result;
        }

        public void CreateExpensesList(UserExpensesListModel model)
        {
            if (model.Name.ToLower().Contains("seeder"))
            {
                if (!_context.ExpensesLists.Any(e => e.Name == model.Name))
                {
                    var seederModel = _mapper.Map<UserExpensesListSeederModel>(model);

                    model = _seeder.Seed(seederModel);
                }      
            }

            var result = _mapper.Map<UserExpensesList>(model);

            result.CreateDate = DateTime.Now;

            _context.ExpensesLists.Add(result);
            _context.SaveChanges();
        }

        public void UpdateExpensesList(UserExpensesListModel model, int id)
        {
            var editModel = _context.ExpensesLists
                .Include(e => e.Expenses)
                .FirstOrDefault(m => m.Id == id);

            if (editModel is null)
                throw new NotFoundException("User list not found.");

            editModel.Name = model.Name;
            editModel.UpdateDate = DateTime.Now;

            _context.SaveChanges();
        }

        public void DeleteExpensesList(int id)
        {
            var result = _context.ExpensesLists.First(e => e.Id == id);

            _context.Remove(result);
            _context.SaveChanges();
        }
    }
}
