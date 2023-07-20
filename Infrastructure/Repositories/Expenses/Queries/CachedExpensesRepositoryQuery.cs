using Domain.Entities.Base;
using Domain.Entities.Models;
using Domain.Modules.Queries;
using Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.EF.Repositories.Expenses.Queries
{
    public class CachedExpensesRepositoryQuery : IExpensesModuleQuery
    {
        private readonly ExpensesRepositoryQuery _queryRepo;

        private readonly IMemoryCache _memoryCache;

        public CachedExpensesRepositoryQuery(ExpensesRepositoryQuery queryRepo, IMemoryCache memoryCache)
        {
            _queryRepo = queryRepo;
            _memoryCache = memoryCache;
        }

        public async Task<UserIncome> GetMonthlyIncome(int id, string year, string month)
        {
            string key = $"income-{id}&{year}/{month}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return await _queryRepo.GetMonthlyIncome(id, year, month);
                });
        }

        public async Task<UserExpense> GetExpense(int id)
        {
            string key = $"expense-{id}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return await _queryRepo.GetExpense(id);
                });
        }

        public async Task<PagedList<UserExpenseResponseDto>> GetExpenses(int id, int? page, int? pagesize, string? searchTerm, bool allRecords, CancellationToken token)
        {
            string key = $"ExpensesPagedList-{id}&{page}&{pagesize}&{searchTerm}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return await _queryRepo.GetExpenses(id, page, pagesize, searchTerm, allRecords, token);
                });
        }
    }
}
