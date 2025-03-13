using AutoMapper;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Model;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMessageRepo _messageRepo;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;
        public MessageController(ApplicationDbContext applicationDbContext,IMessageRepo messageRepo,UserManager<CustomUser> userManager,IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _messageRepo = messageRepo;
            _userManager = userManager;
            _mapper = mapper;   
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody]CreateMessageDto createMessageDto)
        {
            var CurrentUserName=User.GetCurrentUserName();
            if (createMessageDto.ReciverName == CurrentUserName)
            {
                return BadRequest("You Cannot Send Message to Yourself");
            }
            var ReciverUser=await _userManager.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName==createMessageDto.ReciverName);
            var SenderUser=await  _userManager.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName==User.GetCurrentUserName());
            if (ReciverUser == null)
            {
                return BadRequest("There Was A problem The Reciver User Could not Be Found ");
            }
            var message = new Message()
            {
                Reciver = ReciverUser,
                Sender = SenderUser,
                content = createMessageDto.Content,
                ReciverId = ReciverUser.Id,
                SenderId = SenderUser.Id,
                MessageRecivedDate = null,
              MessageSentDate = DateTime.Now,
                ReciverName = createMessageDto.ReciverName,
                SenderNamer = SenderUser.UserName,

            };
            if(message != null)
            {
             await  _applicationDbContext.messages.AddAsync(message);
                await _messageRepo.SaveChangesAsync();
               var resultAfterMapping=_mapper.Map<MessageDto>(message);
                
                return Ok(resultAfterMapping);

            }
            return BadRequest("There Was A problem While Sending The Message");
            
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessages([FromQuery]MessageParams messageParams)
        {
            var UserName=User.GetCurrentUserName();
           var messages=await  _messageRepo.GetMessageforUser(messageParams,UserName);
            Response.AddHttpResponseOfPaginated(messages.CurrentPage, messages.TotalCount, messages.TotalPages, messages.PageSize);
            return Ok(messages);

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Route("GetMessageThread/{SecondUserName}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread([FromRoute]string SecondUserName)
        {
            var UserName = User.GetCurrentUserName();
            var messagesThread=await   _messageRepo.GetMessageThread(UserName,SecondUserName);
            if (messagesThread != null)
            {
                return Ok(messagesThread);
            }
            return BadRequest("Something Went Wrong While Fetching the Message Thread");

        }
    }
}
