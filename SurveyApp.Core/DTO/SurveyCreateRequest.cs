using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class SurveyCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public ICollection<QuestionCreateRequest> Questions { get; set; }
    }
}
