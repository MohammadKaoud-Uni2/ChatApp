using AutoMapper;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Repos.Abstract;
using ChatApp.Api.Repos.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Reflection.Metadata.Ecma335;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILikedRepo _likedRepo ;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;
        public LikesController(ApplicationDbContext context,ILikedRepo likedRepo, UserManager<CustomUser> userManager,IMapper mapper)
        {
            _context = context;
            _likedRepo = likedRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("{UserName}")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> AddLike([FromRoute]string UserName)
        {
            var sourceUserId = User.GetCurrentUserById();
            var sourceUserName=User.GetCurrentUserName();
            var userToLike=await _userManager.Users.Include(x=>x.LikedByUser).FirstOrDefaultAsync(x=>x.UserName==UserName);
          var user= await  _likedRepo.getUserWithLikes(sourceUserId);
            
            if (UserName == user.UserName)
            {
                return BadRequest("You Cannot Like Yourself");
            }
            var likedUser=user.LikedUsers.FirstOrDefault(x=>x.UserName==UserName);
            if (likedUser != null)
            {
                return BadRequest("You Add Like to this Before");
            }
            var UserToAddLike=await _userManager.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName == UserName);
            user.LikedUsers.Add(UserToAddLike);
           await  _context.SaveChangesAsync();

            return Ok(new { message = "You have liked this user" });

        }
        [HttpGet]
        [Route("UserLikes")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult>getUserlikes([FromQuery]LikedParams likedParams)
        {
            var userId = User.GetCurrentUserById();
           var users= await _likedRepo.getUserLikes(likedParams,userId);
            Response.AddHttpResponseOfPaginated(users.CurrentPage, users.TotalCount, users.TotalPages, users.PageSize);
           
            if (users!=null)
            {
                return Ok(users);
            }
            return BadRequest("There Was A problem While Fetching your Request ");
        }
        
    }
}
