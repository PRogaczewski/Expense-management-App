using ExpensesApi.Models.ViewModels;

namespace ExpensesApi.Models.ErrorHandlers
{
    public class ErrorHandlerResponse : ViewModelAbstract
    {
        /// <summary>
        /// Initializing Message field
        /// </summary>
        /// <param name="message"></param>
        public ErrorHandlerResponse(string message)
        {
            Message = message;
            Success = false;
        }

    }
}
