using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Core.DTO;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Web.Api.Filters.ExceptionFilters;
using SurveyApp.Web.Api.Models;

namespace SurveyApp.Web.Api.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class ApiController : ControllerBase
    {
    }
}
