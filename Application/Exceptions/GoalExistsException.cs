using System.Text;

namespace Application.Exceptions
{
    public class GoalExistsException : BusinessException
    {
        public GoalExistsException(ICollection<string> categories) : base(GenerateErrorMessage(categories), 409)
        {

        }

        private static string GenerateErrorMessage(ICollection<string> categories)
        {
            var sb = new StringBuilder();
            sb.Append("You currently have goal for the ");

            var info = string.Join(", ", categories);

            sb.Append(info);

            if(categories.Count() == 1)
            {
                sb.Append(" category.");
            }
            else
            {
                sb.Append(" categories.");
            }         

            return sb.ToString();
        }
    }
}
