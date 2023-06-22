using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public ICollection<ChoiceResponse> Choices { get; set; }
    }
}
