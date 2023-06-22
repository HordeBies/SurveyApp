using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class ChoiceCreateRequest
    {
        [DefaultValue(1)] // Used for swagger info
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than or equal to {1}.")]
        public int Position { get; set; } = 1;

        [Required]
        public string Text { get; set; }
    }
}
