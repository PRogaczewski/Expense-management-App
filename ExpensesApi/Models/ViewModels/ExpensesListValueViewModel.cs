namespace ExpensesApi.Models.ViewModels
{
    public record ExpensesListValueViewModel
    {
        private ExpensesListValueViewModel()
        { }

        public decimal Value { get; private set; }

        public static ExpensesListValueViewModel CreateViewModel(decimal value)
        {
            var result = new ExpensesListValueViewModel()
            {
                Value = value
            };

            return result;
        }
    }
}
