namespace ChatApp.Api.Dtos
{
    public class ApiException
    {
        public int Statuecode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public ApiException(int statueCode,string message,string details=null)
        {
            Statuecode = statueCode;
            Message = message;
            Details = details;  
           
        }


    }
}
