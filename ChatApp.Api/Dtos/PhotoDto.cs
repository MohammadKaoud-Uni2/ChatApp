namespace ChatApp.Api.Dtos
{
    public class PhotoDto
    {
        public string Url { get; set; }
        public string? PublicId { get; set; }
        public bool IsMain { get; set; }
    }
}
