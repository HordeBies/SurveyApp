using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class AnswerResponse
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Value { get; set; }
    }
}
