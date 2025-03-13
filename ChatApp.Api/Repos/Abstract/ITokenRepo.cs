using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Api.Repos.Abstract
{
    public interface ITokenRepo
    {
        public Task<string> GenerateToken(IList<string>Roles,CustomUser user);
    }
}
