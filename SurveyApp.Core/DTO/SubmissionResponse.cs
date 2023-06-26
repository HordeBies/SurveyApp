using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class SubmissionResponse
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<AnswerResponse> Answers { get; set; }
    }
}
