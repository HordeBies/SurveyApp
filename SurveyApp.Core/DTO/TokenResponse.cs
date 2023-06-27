using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.DTO
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public UserResponse? User { get; set; }
    }
}
