using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Web.Api.Models;
using System.Net;

namespace SurveyApp.Web.Api.Filters.ExceptionFilters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        protected ApiResponse response;

        public ApiExceptionFilter(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            response = new ApiResponse();
        }

        public void OnException(ExceptionContext context)
        {
            // Don't display exception details unless running in Development.
            if (_hostEnvironment.IsDevelopment())
            {
                response.ErrorMessages.Add(context.Exception.ToString());
            }
            else
            {
                response.ErrorMessages.Add("An Internal Error occurred");
            }
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Result = new OkObjectResult(response);
        }
    }
}
