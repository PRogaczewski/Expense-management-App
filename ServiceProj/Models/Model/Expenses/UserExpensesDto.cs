using ExpensesApi.Models.Categories;
using ServiceProj.Models.Model.ExpensesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.Expenses
{
    public class UserExpensesDto
    {
        public int Id { get; set; }

        public ExpenseCategories Category { get; set; }

        public decimal Price { get; set; }

        public int UserExpensesListId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
