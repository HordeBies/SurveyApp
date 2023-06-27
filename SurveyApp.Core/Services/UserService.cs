using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SurveyApp.Core.DTO;
using SurveyApp.Core.ServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SurveyApp.Core.Services
{
    public class UserService(UserManager<IdentityUser> userManager, IConfiguration configuration) : IUserService
    {
        public async Task<bool> IsUniqueUser(string username)
        {
            return (await userManager.FindByNameAsync(username)) == null;
        }

        public async Task<TokenResponse> GetToken(LoginRequest loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.UserName);
            bool isValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (user == null || isValid == false)
            {
                return new TokenResponse()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["ApiSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse response = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = new UserResponse()
                {
                    Id = user.Id,
                    UserName = user.UserName
                }
            };
            return response;
        }
        public async Task<UserResponse> Register(RegisterRequest registerRequest)
        {
            IdentityUser user = new()
            {
                UserName = registerRequest.Email,
                NormalizedUserName = registerRequest.Email.ToUpper(),
                Email = registerRequest.Email,
                NormalizedEmail = registerRequest.Email.ToUpper(),
            };
            
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
            {
                var userToReturn = await userManager.FindByNameAsync(user.UserName);
                return new()
                {
                    Id = userToReturn.Id,
                    UserName = userToReturn.UserName
                };
            }
            else
            {
                //throw new AggregateException("Multiple Errors Occured", result.Errors.Select(e => new Exception(e.Description)));
            }

            return new();
        }
    }
}
