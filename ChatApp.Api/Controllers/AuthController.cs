using AutoMapper;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter (typeof(LoginUserActionFilter))] 
    public class AuthController : ControllerBase
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenRepo _tokenRepo ;

        public AuthController(UserManager<CustomUser> userManager,RoleManager<IdentityRole>roleManager,ITokenRepo tokenRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenRepo = tokenRepo;
        }
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var users = await _userManager.Users.Include(x => x.Photos).ToListAsync();
            var user=users.FirstOrDefault(x=>x.Email == loginRequestDto.Email);
            if (user != null)
            {
               var checkpassword=await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkpassword)
                {
                    var roles=await _userManager.GetRolesAsync(user);
                     var token=await _tokenRepo.GenerateToken(roles, user);
                    var result = new LoginResponse
                    {
                        Roles = roles,
                        Token = token,
                        Email = loginRequestDto.Email,
                        Gender = user.Gender

                    };
                    return Ok(result);
                   
                }

                return BadRequest(new { message = "The Password is Incorrect!" });                
            }
            return BadRequest(new { message = "The Email Doesnot exist !" });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userEmailChecking = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userEmailChecking != null)
            {
                return BadRequest("Email is Exist From Another User!");
            } 
            if (registerDto != null)
            {
                var user = new CustomUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    PhoneNumber = registerDto.PhoneNumber,
                    EmailConfirmed = false,
                    Country = registerDto.Country,
                    City = registerDto.City,
                    KnownAs = registerDto.KnownAs,
                    Gender = registerDto.Gender,
                    Created = DateTime.Now,
                   DateBirth = registerDto.DateBirth,
                   LastActive = DateTime.Now,
                    NormalizedEmail = registerDto.Email.ToUpper(),

                };
               var resultofCreatingNewUser=await  _userManager.CreateAsync(user,registerDto.Password);
                if (resultofCreatingNewUser.Succeeded)
                {
                    IList<string> roles = new  List<string>()
                    {
                        "Reader"
                    };

                   var addToRole=await  _userManager.AddToRolesAsync(user, roles);
                    if (addToRole.Succeeded)
                    {
                     
                       var token=  await _tokenRepo.GenerateToken(roles, user);
                        var result = new LoginResponse()
                        {
                            Roles = roles,
                            Email = registerDto.Email,
                            Token = token
                        };
                        return Ok(result);
                    }
                }
                return BadRequest("There was Something Wrong While Creating The New user");
            }
            return BadRequest("Fill the fields");
        }

    }
}
