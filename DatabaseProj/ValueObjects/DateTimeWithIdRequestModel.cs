namespace Domain.ValueObjects
{
    public class DateTimeWithIdRequestModel : DateTimeRequestModel
    {
        protected DateTimeWithIdRequestModel() { }

        public DateTimeWithIdRequestModel(int id, string year, string month) : base(year, month)
        {
            Id = id;
        }

        public int Id { get; protected set; }
    }
}
