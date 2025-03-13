
using ChatApp.Api.Data.Identity;
using Microsoft.Identity.Client;

namespace ChatApp.Api.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public CustomUser Sender { get; set; }
        public string SenderId { get; set; }
        public string SenderNamer { get; set; }
        public CustomUser Reciver { get; set; }
        public string ReciverId { get; set; }
        public string ReciverName { get; set; }
        public DateTime MessageSentDate { get; set; } = DateTime.Now;
        public DateTime ?MessageRecivedDate {  get; set; }

        public string content {  get; set; }
        public bool IsSenderDelete { get; set; }
        public bool IsReciverDelete { get; set; }



    }
}
