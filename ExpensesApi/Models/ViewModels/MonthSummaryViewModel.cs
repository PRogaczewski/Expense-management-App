namespace ExpensesApi.Models.ViewModels
{
    public record MonthSummaryViewModel
    { 
        public decimal PreviousMonthOutgoings { get; private set; }

        public IDictionary<string, decimal> MonthSummary { get; private set; }

        private MonthSummaryViewModel()
        {
            MonthSummary = new Dictionary<string, decimal>();
        }

        public static MonthSummaryViewModel CreateViewModel(decimal value, IDictionary<string, decimal> dict)
        {
            var result = new MonthSummaryViewModel()
            {
                PreviousMonthOutgoings = value,
                MonthSummary = dict,
            };

            return result;
        }
    }
}
