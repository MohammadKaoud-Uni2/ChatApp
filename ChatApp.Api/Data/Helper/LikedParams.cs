using System.ComponentModel.DataAnnotations;

namespace ChatApp.Api.Data.Helper
{
    public class LikedParams:PaginatedParams
    {
       
        public string predicate { get; set; } = "liked";

    }
}
