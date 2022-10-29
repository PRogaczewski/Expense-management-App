using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.Models.Model.ExpensesList
{
    public class UserExpensesListModel
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
