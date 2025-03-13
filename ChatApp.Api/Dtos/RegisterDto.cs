using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Dtos
{
    public class RegisterDto
    {
       
        public string Email { get; set; }
    
        public string UserName { get; set; }
        public string KnownAs { get; set; }

       
        public string Password { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Country { get; set; }
        
        public DateTime DateBirth { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
   
      public string Gender { get; set; }

       

    }
}
