using AutoMapper;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ChatApp.Api.Repos.Implementation
{
    public class UserRepo : GenericRepo<CustomUser>, IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserRepo(ApplicationDbContext context,UserManager<CustomUser> userManager,IMapper mapper,IHttpContextAccessor httpContextAccessor):base(context)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<CustomUser> GetUserByEmail(string Email)
        {
            var users = await GetUsersAsync(); 
            var result = users.FirstOrDefault(x => x.Email == Email);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<CustomUser> GetUserByName(string UserName)
        {
            var users =  await GetUsersAsync();
            var result=users.FirstOrDefault(x=>x.UserName==UserName);
            if (result != null)
            {
                return result;
            }
            return null;

      
        }

        public  PagedList<GetUserDto> GetUsersAsPaginated(UserPaginatedParams userPaginatedParams)
        {
            var Users =  _userManager.Users.Include(x=>x.Photos).ToList();
            var userToExcludehisName = _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.GetCurrentUserEmail());
            userPaginatedParams.Email = userToExcludehisName.Result.Email;
            Users =Users.Where(x=>x.Email!=userPaginatedParams.Email).ToList();
         
            if (string.IsNullOrEmpty(userPaginatedParams.Gender))
            {
                userPaginatedParams.Gender = userToExcludehisName.Result.Gender == "male" ? "female" : "male";
            }
            if (userPaginatedParams.Gender != null)
            {
               
                Users = Users.Where(x => x.Gender.Equals(userPaginatedParams.Gender, StringComparison.OrdinalIgnoreCase)).ToList();
                if (userPaginatedParams.OrderBy == "lastActive")
                {
                    Users = Users.OrderBy(x => x.LastActive).ToList();
                }
                var resultAfterMapping = _mapper.Map<List<GetUserDto>>(Users);


                return PagedList<GetUserDto>.ToPagedList(resultAfterMapping.AsQueryable(), userPaginatedParams.PageNumber, userPaginatedParams.PageSize);
            }
            return null;
        }

        public async Task<List<CustomUser>> GetUsersAsync()
        {

            var users = await _userManager.Users.Include(x=>x.Photos).ToListAsync();    
            return users;
        }
    }
}
