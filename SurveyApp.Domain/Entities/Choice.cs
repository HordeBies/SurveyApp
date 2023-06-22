using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        // Optional, default to 1
        public int Position { get; set; }
        // Required
        public string Text { get; set; }
    }
}
