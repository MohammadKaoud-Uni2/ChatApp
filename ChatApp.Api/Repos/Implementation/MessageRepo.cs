using AutoMapper;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Model;
using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Repos.Implementation
{
    public class MessageRepo : IMessageRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper Mapper;
        public MessageRepo(ApplicationDbContext context,UserManager<CustomUser> userManager,IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
            _userManager= userManager;  
        }
        public void AddMessage(Message message)
        {
            _context.messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
           _context.messages.Remove(message);
        }

        public async Task<Message> GetMessageAsync(Guid id)
        {
         return await _context.messages.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PagedList<MessageDto>> GetMessageforUser(MessageParams messageParams,string UserName)
        {
            var query = _context.messages.Include(x=>x.Sender).ThenInclude(x=>x.Photos).Include(x=>x.Reciver).ThenInclude(x=>x.Photos).OrderByDescending(x => x.MessageSentDate).AsQueryable();
            query = messageParams.Container switch
            {
                "Inbox"=>query.Where(x=>x.ReciverName==UserName),
                "Outbox"=>query.Where(x=>x.SenderNamer==UserName),
                _=>query.Where(x=>x.ReciverName==UserName &&x.MessageRecivedDate==null),
            };
            List <Message> resultList= query.ToList();
            var queryAfterMapping=Mapper.Map<List<MessageDto>>(resultList);
            return  PagedList<MessageDto>.ToPagedList(queryAfterMapping.AsQueryable(), messageParams.PageNumber, messageParams.PageSize);

        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string CurrentUserName, string SecondUserName)
        {
            var messages = await _context.messages.Include(x => x.Sender).ThenInclude(x => x.Photos)
                .Include(x => x.Reciver).ThenInclude(x => x.Photos)
                .Where(
                x => x.SenderNamer == CurrentUserName
                && x.ReciverName == SecondUserName
                || x.SenderNamer == SecondUserName
                && x.ReciverName == CurrentUserName
                ).OrderBy(x => x.MessageSentDate).ToListAsync();
            var UnreadMessages = messages.Where(x => x.ReciverName == CurrentUserName && x.MessageRecivedDate == null);
            if (UnreadMessages.Any())
            {
                foreach (var message in UnreadMessages)
                {
                    message.MessageRecivedDate = DateTime.Now;
                }
              await   _context.SaveChangesAsync();
            }
            var AfterMapping=Mapper.Map<IEnumerable<MessageDto>>(messages);
            return AfterMapping;
        }

        public async  Task<bool> SaveChangesAsync()
        {
            return  await _context.SaveChangesAsync()>0;
        }
    }
}
