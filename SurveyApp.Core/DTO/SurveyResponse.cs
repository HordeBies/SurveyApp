using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParticipantUrl { get; set; }
        public string StatisticsUrl { get; set; }
        public ICollection<QuestionResponse> Questions { get; set; }
    }
}
