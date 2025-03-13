using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Api.Repos.Implementation
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration _configuration;
        public TokenRepo(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async  Task<string> GenerateToken(IList<string> Roles, CustomUser user)
        {
           List<Claim>claimss=new List<Claim>();
            var userNameClaim=user.UserName;
            claimss.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
            claimss.Add( new Claim(ClaimTypes.Name, userNameClaim));
            claimss.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in Roles)
            {
                var roleClaim=new Claim(ClaimTypes.Role, role);
                claimss.Add(roleClaim); 
            }
            var key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:audience"],
               signingCredentials:creds,
               expires:DateTime.Now.AddMinutes(40),
               claims:claimss


                );
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
