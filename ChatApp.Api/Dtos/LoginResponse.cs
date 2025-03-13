namespace ChatApp.Api.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public IList<string>Roles { get; set; }
        public string Gender { get; set; }

    }
}
