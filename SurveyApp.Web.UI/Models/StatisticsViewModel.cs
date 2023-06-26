using SurveyApp.Core.DTO;

namespace SurveyApp.Web.UI.Models
{
    public class StatisticsViewModel
    {
        public SurveyResponse Survey { get; set; }
        public IEnumerable<SubmissionResponse> Submissions { get; set; }
        public IDictionary<QuestionResponse,IEnumerable<AnswerResponse>> StatsDict { get; set; }
    }
}
