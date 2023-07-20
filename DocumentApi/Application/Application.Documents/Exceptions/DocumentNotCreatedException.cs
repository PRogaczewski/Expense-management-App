namespace Application.Exceptions
{
    public class DocumentNotCreatedException : Exception
    {
        public DocumentNotCreatedException(string message, int status) : base(message)
        {
            StatusCode = status;
        }

        public int StatusCode { get; set; }
    }
}
