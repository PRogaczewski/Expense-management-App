namespace Domain.ValueObjects
{
    public class DateTimeRequestModel
    {
        protected DateTimeRequestModel() { }

        public DateTimeRequestModel(string year, string month)
        {
            Year = year;
            Month = month;
        }

        public string Year { get; protected set; }

        public string Month { get; protected set; }
    }
}
