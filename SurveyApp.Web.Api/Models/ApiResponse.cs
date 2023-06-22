using System.Net;

namespace SurveyApp.Web.Api.Models
{
    public class ApiResponse : ApiResponse<object> // For default generic type as object
    {

    }
    public class ApiResponse<T> // Generic can be used for swagger documentation for now
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool success
        {
            get
            {
                return (int)StatusCode / 100 == 2;
            }
        }
        public List<string> ErrorMessages { get; set; } = new();
        public T? Result { get; set; }
    }
}
