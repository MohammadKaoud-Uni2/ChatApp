using ChatApp.Api.Data.Identity;

namespace ChatApp.Api.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string SenderNamer { get; set; }
        public string ReciverId { get; set; }
        public string ReciverName { get; set; }
        public DateTime MessageSentDate { get; set; }
        public DateTime MessageRecivedDate { get; set; }
        public  string SenderPhotoUrl { get; set; }
        public string ReciverPhotoUrl { get; set; }

        public string content { get; set; }
    }
}
