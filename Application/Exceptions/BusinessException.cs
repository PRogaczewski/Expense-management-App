namespace Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, int status) : base(message)
        {
            StatusCode = status;
        }

        public int StatusCode { get; set; }
    }
}
