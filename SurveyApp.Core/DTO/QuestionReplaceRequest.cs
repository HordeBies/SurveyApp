using SurveyApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class QuestionReplaceRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SurveyId { get; set; }

        [DefaultValue(1)] // Used for swagger info
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than or equal to {1}.")]
        public int Position { get; set; } = 1;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public QuestionTypes Type { get; set; }

        public ICollection<ChoiceCreateRequest> Choices { get; set; }
    }
}
