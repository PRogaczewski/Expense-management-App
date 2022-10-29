using DatabaseProj.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProj.DatabaseEntities.Models
{
    public class UserExpensesList
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<UserExpense> Expenses { get; set; } = new List<UserExpense>();

        public List<UserExpenseGoal> UserGoals { get; set; } = new List<UserExpenseGoal>();

        public List<UserIncome> UserIncomes { get; set; } = new List<UserIncome>();

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
