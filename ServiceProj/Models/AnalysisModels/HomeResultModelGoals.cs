using ExpensesApi.Models.Categories;
using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.AnalysisModels
{
    public class HomeResultModelGoals : HomeResultModelCompare
    {
        public IDictionary<ExpenseCategories, decimal> UserGoals { get; set; }

        public IDictionary<ExpenseCategories, decimal> UserExpenses { get; set; }

        public IDictionary<ExpenseCategories, decimal> Result { get; set; }
    }
}
