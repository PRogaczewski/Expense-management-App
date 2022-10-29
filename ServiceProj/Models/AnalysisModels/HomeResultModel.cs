using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.AnalysisModels
{
    public class HomeResultModel
    {
        public decimal Incomes { get; set; }

        public decimal Outgoings { get; set; }

        public decimal MonthlyResult { get; set; }

        public IDictionary<ExpenseCategories, decimal> TotalMonthByCategories { get; set; }

        public IDictionary<ExpenseCategories, decimal> CurrentWeekByCategories { get; set; }
    }
}
