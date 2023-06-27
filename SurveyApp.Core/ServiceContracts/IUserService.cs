using SurveyApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<bool> IsUniqueUser(string username);
        Task<TokenResponse> GetToken(LoginRequest loginRequest);
        Task<UserResponse> Register(RegisterRequest registerRequest);
    }
}
