using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SurveyApp.Core.DTO;

namespace SurveyApp.Web.UI.Models
{
    public class SurveyViewModel
    {
        [ValidateNever]
        public SurveyResponse? Survey { get; set; }
        public SubmissionCreateRequest Request { get; set; }
    }
}
