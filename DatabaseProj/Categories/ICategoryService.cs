namespace Domain.Categories
{
    public interface ICategoryService
    {
        public IEnumerable<string> GetExpenseCategories()
        {
            var categories = new List<string>();

            foreach (Enum item in Enum.GetValues(typeof(ExpenseCategories)))
            {
                categories.Add(item.GetEnumDisplayName());
            }

            return categories;
        }
    }
}