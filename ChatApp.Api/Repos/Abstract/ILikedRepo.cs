using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;

namespace ChatApp.Api.Repos.Abstract
{
    public interface ILikedRepo
    {
        
        public Task<CustomUser> getUserWithLikes(string SourceUserId);
        public Task<PagedList<GetUserDto>> getUserLikes(LikedParams likedParams,string UserId);
    }
}
