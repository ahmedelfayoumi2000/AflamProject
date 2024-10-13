using Movies.API.Errors;

namespace Movies.API.Errors
{
    public class ApiExceptionResponce : ApiResponse
    {
        public string Details { get; }
        public ApiExceptionResponce(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

    }
}
