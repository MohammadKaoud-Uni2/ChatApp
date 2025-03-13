using System.Runtime.CompilerServices;
using System.Text.Json;
namespace ChatApp.Api.Data.Helper
{
    public static  class HttpHeaderExtension
    {
        public static  void  AddHttpResponseOfPaginated( this HttpResponse response,int CurrentPage,int TotalItems,int TotalPages,int ItemsPerPage)
        {
            var PageinatedHeader=new PagedHeader(CurrentPage, TotalPages,ItemsPerPage, TotalItems);
            var Options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(PageinatedHeader,Options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}
