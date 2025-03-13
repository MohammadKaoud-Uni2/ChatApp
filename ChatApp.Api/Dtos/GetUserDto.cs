using ChatApp.Api.Model;

namespace ChatApp.Api.Dtos
{
    public class GetUserDto
    {
        public string Email { get; set; }
        public string PhonNumber { get; set; }
        public string UserName { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastActive { get; set; }

        public string? KnownAs { get; set; }

        public string? Gender { get; set; }

        public string? LookingFor { get; set; }
 
        public string? Interests { get; set; }
        public string CurrentProfilePictureUrl { get; set; }

        public  ICollection<PhotoDto> Photos { get; set; }
        public string? Introduction { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }
      
        
    }
}
