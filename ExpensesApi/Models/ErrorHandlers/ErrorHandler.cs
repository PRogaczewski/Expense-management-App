namespace ExpensesApi.Models.ErrorHandlers
{
    public class ErrorHandler
    {
        /// <summary>
        /// Initializing Message field
        /// </summary>
        /// <param name="message"></param>
        public ErrorHandler(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
