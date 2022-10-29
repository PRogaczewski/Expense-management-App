using ExpensesApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.AnalysisModels
{
    public class HomeResultModelCompare : HomeResultModel
    {
        public IDictionary<ExpenseCategories, decimal> CompareLastMonthByCategories { get; set; }
    }
}
