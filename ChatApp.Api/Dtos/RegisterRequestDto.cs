using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
       

        public string Address { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        
    }
}
