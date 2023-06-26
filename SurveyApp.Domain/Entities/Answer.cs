using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int? QuestionId { get; set; }
        // If the question is updated after the submission, we need to keep the original question text here to preserve the integrity of the submission.
        public string QuestionText { get; set; }
        // Same goes for the answer value (choice).
        public string Value { get; set; }
        public Question? Question { get; set; }
    }
}
