using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class SubmissionCreateRequest
    {
        [Required]
        public int SurveyId { get; set; }

        [Required]
        public List<AnswerCreateRequest> Answers { get; set; }
    }
}
