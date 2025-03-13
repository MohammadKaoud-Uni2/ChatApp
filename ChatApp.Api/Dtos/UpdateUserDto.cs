using ChatApp.Api.Model;

namespace ChatApp.Api.Dtos
{
    public class UpdateUserDto
    {
      

        

        public string? LookingFor { get; set; }

        public string? Interests { get; set; }
        public string? Introduction { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

    }
}
