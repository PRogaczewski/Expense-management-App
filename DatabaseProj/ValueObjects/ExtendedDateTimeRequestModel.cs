namespace Domain.ValueObjects
{
    public class ExtendedDateTimeRequestModel : DateTimeWithIdRequestModel
    {
        protected ExtendedDateTimeRequestModel() { }

        public ExtendedDateTimeRequestModel(int id, string firstYear, string firstMonth, string secondYear, string secondMonth):base(id, firstYear, firstMonth)
        {
            SecondMonth = secondMonth;
            SecondYear = secondYear;
        }

        public string SecondYear { get; protected set; }

        public string SecondMonth { get; protected set; }
    }
}
