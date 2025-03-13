using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace ChatApp.Api.Data.Helper
{
    public class UserPaginatedParams:PaginatedParams
    {
       
        public string Gender { get; set; }
        
        public string? Email { get; set; }
        public string ?OrderBy { get; set; }


       
        

    }
}
