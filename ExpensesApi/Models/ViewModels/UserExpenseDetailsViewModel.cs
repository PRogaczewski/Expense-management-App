namespace ExpensesApi.Models.ViewModels
{
    public class UserExpenseDetailsViewModel
    {
        private UserExpenseDetailsViewModel() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
