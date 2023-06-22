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
        [AllowedValues(QuestionTypes.SingleChoice, QuestionTypes.SingleOpenEnded, QuestionTypes.MultiOpenEnded, QuestionTypes.Rating, ErrorMessage = $"Allowed values are: {QuestionTypes.SingleChoice},{QuestionTypes.SingleOpenEnded},{QuestionTypes.MultiOpenEnded},{QuestionTypes.Rating}")] // For more complex validation custom validator can be used but for now this will do just fine
        public string Type { get; set; }

        public ICollection<ChoiceCreateRequest> Choices { get; set; }
    }
}
