using ChatApp.Api.Data.Helper;
using ChatApp.Api.Dtos;
using ChatApp.Api.Model;

namespace ChatApp.Api.Repos.Abstract
{
    public interface IMessageRepo
    {
        public void AddMessage(Message message);
        public void DeleteMessage(Message message);
        public Task<Message> GetMessageAsync(Guid  id);
        public Task<PagedList<MessageDto>> GetMessageforUser(MessageParams messageParams,string UserName);
        public Task<IEnumerable<MessageDto>> GetMessageThread(string CurrentUserName, string SecondUserName);
        public Task<bool> SaveChangesAsync();
    }
}
