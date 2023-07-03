namespace Infrastructure.EF.Database
{
    public class DatabaseModule
    {
        protected DatabaseModule(ExpenseDbContext context)
        {
            _context = context;
        }

        protected ExpenseDbContext _context { get; set; }
    }
}
