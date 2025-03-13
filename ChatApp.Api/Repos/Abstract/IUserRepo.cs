using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;

namespace ChatApp.Api.Repos.Abstract
{
    public interface IUserRepo:IGenericRepo<CustomUser>
    {
        public  Task<CustomUser> GetUserByName(string UserName);
        public Task<List<CustomUser>> GetUsersAsync(); 
        public Task<CustomUser>GetUserByEmail(string Email);
        public PagedList<GetUserDto> GetUsersAsPaginated(UserPaginatedParams userPaginatedParams);
                
    }
}
