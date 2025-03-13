using Microsoft.Identity.Client;

namespace ChatApp.Api.Dtos
{
    public class LikedUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl {  get; set; }
        public string City { get; set; }

    }
}
