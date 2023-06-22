using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        // Optional, default to 1
        public int Position { get; set; }
        public string Name { get; set; } // For survey creator to see
        public string Text { get; set; } // For survey taker to see
        public string Type { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
