using Domain.Entities.Base;

namespace Domain.Entities.Models
{
    public class UserIncome : ExpensesListBaseEntity
    {
        public decimal Income { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
