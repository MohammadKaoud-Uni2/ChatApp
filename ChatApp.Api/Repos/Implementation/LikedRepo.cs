using AutoMapper;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ChatApp.Api.Repos.Implementation
{
    public class LikedRepo : ILikedRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper mapper;
        public LikedRepo(ApplicationDbContext context,UserManager<CustomUser>userManager,IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            this.mapper = mapper;
        }


        public async Task<PagedList<GetUserDto>> getUserLikes(LikedParams likedParams, string UserId)
        {
            IQueryable<CustomUser> usersQuery;
           

            if (likedParams.predicate == "liked")
            {
                var user = await getUserWithLikes(UserId);

                usersQuery = _context.Users
                    .Where(u => user.LikedUsers.Select(l => l.Id).Contains(u.Id)) 
                    .Include(u => u.Photos); 
            }
            else if (likedParams.predicate == "likedBy")
            {
                var user = await getUserWithLikes(UserId);
            
                usersQuery = _context.Users
                    .Where(u => user.LikedByUser.Select(l => l.Id).Contains(u.Id)) 
                    .Include(u => u.Photos); 
            }
            else
            {
                return null;
            }

            
            var usersList = await usersQuery.ToListAsync();
            var resultAfterMapping=mapper.Map<List<GetUserDto>>(usersList);
          
            return  PagedList<GetUserDto>.ToPagedList(resultAfterMapping.AsQueryable(), likedParams.PageNumber, likedParams.PageSize);
        }

        public async Task<CustomUser> getUserWithLikes(string SourceUserId)
        {
               var user=await _userManager.Users.Include(x=>x.LikedUsers).Include(x=>x.LikedByUser).Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.Id==SourceUserId);
            return user;
        }
    }
}
