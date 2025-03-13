using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }  

    }
}
