using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Data.Helper
{
    public class MessageParams :PaginatedParams
    {
       
        public string Container { get; set; } = "Unread";

    }

}
