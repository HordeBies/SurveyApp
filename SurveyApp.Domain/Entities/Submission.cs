using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Submission
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
