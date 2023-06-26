using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class AnswerCreateRequest
    {
        [Required]
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        [Required(ErrorMessage ="You need to answer this question.")]
        public string Value { get; set; }
    }
}
