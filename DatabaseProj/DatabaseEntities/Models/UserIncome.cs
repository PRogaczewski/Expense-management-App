using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProj.DatabaseEntities.Models
{
    public class UserIncome
    {
        public int Id { get; set; }

        public int UserExpensesListId { get; set; }

        public UserExpensesList UserExpensesList { get; set; }

        public decimal Income { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
