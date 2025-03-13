using AutoMapper;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Model;
using ChatApp.Api.Repos.Abstract;
using ChatApp.Api.Repos.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UserController(UserManager<CustomUser> userManager, IUserRepo userRepo, IMapper mapper, IPhotoService photoService)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _mapper = mapper;
            _photoService = photoService;
        }
        [HttpGet]
        [Route("AllUsers")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserPaginatedParams userPaginatedParams)
        {

            //var users = await _userRepo.GetUsersAsync();
            //var resultafterMapping = _mapper.Map<List<GetUserDto>>(users);
            //if (resultafterMapping != null)
            //{
            //    return Ok(resultafterMapping);
            //}
            
            var userAsPaginated =  _userRepo.GetUsersAsPaginated(userPaginatedParams);
            if (userAsPaginated != null)
            {
                Response.AddHttpResponseOfPaginated(userAsPaginated.CurrentPage, userAsPaginated.TotalCount, userAsPaginated.TotalPages, userAsPaginated.Count);
                 return Ok(userAsPaginated);
            }


            return BadRequest("There was A problem While fetching the User from the Server");


        }
        [HttpGet]
        [Route("GetUserByName/{UserName}")]
        public async Task<IActionResult> GetUserByUserName([FromRoute] string UserName)
        {
            var result = await _userRepo.GetUserByName(UserName);
            if (result != null)
            {
                var resultAfterMapping = _mapper.Map<GetUserDto>(result);
                return Ok(resultAfterMapping);
            }
            return BadRequest("There Was Something wrong !");
        }
        [HttpGet]
        [Route("GetUserByEmail/{Email}")]
     
        public async Task<IActionResult> GetUserByEmail([FromRoute] string Email)
        {
            var result = await _userRepo.GetUserByEmail(Email);
            if (result != null)
            {
                var resultAfterMapping = _mapper.Map<GetUserDto>(result);
                return Ok(resultAfterMapping);
            }
            return BadRequest("There Was Something wrong !");
        }
        [HttpPut]
        [Route("UpdateUser")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var email = User.GetCurrentUserEmail();
            var user = await _userRepo.GetUserByEmail(email);
            _mapper.Map(updateUserDto, user);
            await _userRepo.UpdateAsync(user);

            if (user != null)
            {
               var resultAfterMapping=_mapper.Map<GetUserDto>(user);
                return Ok(resultAfterMapping);
            }
            return BadRequest("There Was A problem while Updating the User ");
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Route("AddUserImage")]
        public async Task<IActionResult>AddPhotoToUser([FromForm]IFormFile file)
        {
            var emailofCurrentUser =  User.GetCurrentUserEmail();
            var user=await _userRepo.GetUserByEmail(emailofCurrentUser);
            if (user != null)
            {
              var resultofUploadingThroughCloudinary= await _photoService.UploadImage(file);
                var photoToUpload = new Photo()
                {
                    Url=resultofUploadingThroughCloudinary.SecureUrl.AbsoluteUri,
                    PublicId=resultofUploadingThroughCloudinary.PublicId,

                };
                if (photoToUpload != null)
                {
                    if(user.Photos.Count == 0)
                    {
                        photoToUpload.IsMain = true;
                    }
                   
                    photoToUpload.Id = Guid.NewGuid();
                    photoToUpload.CustomUserId = user.Id;
                   await   _photoService.AddAsync(photoToUpload);
                   await    _photoService.SaveChangesAsync();
                    var resultofMapping=_mapper.Map<PhotoDto>(photoToUpload);
                    return Ok(resultofMapping);
                }
                return BadRequest("There was A problem While Uploading The Image or While Mapping to Dto ");
            }
            return BadRequest("Something Went Wrong!");
        }
        [HttpPut]
        [Route("SetMainPhoto/{photoId}")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> SetMainPhoto([FromRoute]string photoId)
        {
            var userEmail = User.GetCurrentUserEmail();
            var user=await _userRepo.GetUserByEmail(userEmail);
            if (user != null)
            {
               var photo= user.Photos.FirstOrDefault(x => x.PublicId==photoId);
                if (photo.IsMain == true)
                {
                    return BadRequest("This Image Already Set As Main Photo Previously ");
                }
           var userphotos=user.Photos.Where(x => x.PublicId != photoId);
                foreach (var userphoto in userphotos)
                {
                    userphoto.IsMain = false;
                }
                photo.IsMain = true;
                 await _userRepo.SaveChangesAsync();
                return NoContent();
               
            }
            return BadRequest("There Was a Problem While getting the User Gmail Mybe The Gmail is InCorrect or Doesnot Exist !");
        }
        [HttpDelete]
        [Route("DeletePhoto/{PhotoId}")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> DeleteUserPhoto([FromRoute] string PhotoId)
        {
            var userEmail = User.GetCurrentUserEmail();
            var users = await _userRepo.GetTableAsTracking().Include(X => X.Photos).ToListAsync();
            var user= users.FirstOrDefault(x=>x.Email == userEmail);
            if (user != null)
            {
                var photo =  user.Photos.FirstOrDefault(x=> x.PublicId == PhotoId);
                if (photo != null && photo.IsMain==true)
                {
                    return BadRequest("Cannot Delete your Main photo");
                }
               var resultofDeleting=await  _photoService.DeletePhoto(PhotoId);

                if (resultofDeleting.Error != null)
                {
                    return BadRequest("There Was Something Wrong While Deleting the Photo");

                }
                user.Photos.Remove(photo);
              await   _userRepo.SaveChangesAsync();
                return Ok(photo);
            }
            return BadRequest("Cannot Load Or Recognized the user ");

        }
    }
}
