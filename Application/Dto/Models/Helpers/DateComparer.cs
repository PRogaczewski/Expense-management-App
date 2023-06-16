using Application.Dto.Models.Expenses;

namespace Application.Dto.Models.Helpers
{
    public class DateComparer
    {
        public Comparer Case { get; set; }

        public IEnumerable<UserExpensesDto> Expenses { get; set; }
    }

    public enum Comparer
    {
        First,
        Second,
        //Special type 
        None
    }
}
